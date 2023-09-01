using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleRecSys.Interfaces;
using RecommendationSystemInterface;

namespace ConsoleRecSys
{
    class ConsoleController : Controller
    {
        private readonly ConsoleViewer _viewer;

        public ConsoleController(ConsoleSession session, ConsoleViewer viewer) : base(session)
        {
            _viewer = viewer;
            TakeInput();
        }

        public void TakeInput()
        {
            _viewer.ViewString(@"This is a small scale recommendation system interface.
Choose Approach, User as you need and get your recommendations!
To view what you can do type 'help'.");

            string? line;
            while ((line = Console.ReadLine()) is not "" or null)
            {
                Crunch(line!.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries));
            }
        }

        private void Crunch(string[] cmd)
        {
            if (cmd.Length == 0) { return; }

            switch (cmd[0])
            {
                case "help":
                    _viewer.ViewString(@"
loadcsv - load a file you want to work with
loaddbs - connect to a database with which you want to work with
selectapproach - select recommendation approach
recommend - start the program (approach must be selected)
usertype - select the user's type
useradd - fill a user's variable of your need
userdemo - fill user's variables (demo)
userclear - reset user's variables
user - show your user
summary - shows info about everything - to check if everything went as expected
save - save your session (your current settings)
load - load a session
show - show saved sessions
delete - delete a session
demo - try one of the demos
");
                    break;
                case "loadcsv":
                    if (cmd.Length != 2) { _viewer.WrongCmdErrorMsg(new[] {"loadcsv", "filename / filepath without spaces"}); break; }
                    Session.LoadFromCsv(cmd[1]);
                    break;
                case "loaddbs":
                    Session.LoadFromDbs();
                    break;
                case "selectapproach":
                    if (!((ConsoleSession)Session).DataSourceKnown()) { _viewer.ViewString("Select data-source first. Load a csv for example."); break; }
                    string[] parameters = CreateApproachDialog();
                    if (parameters.Length == 0) { _viewer.ViewString("Approach dialog aborted!"); break; }
                    Session.SelectApproach(parameters);
                    ((ConsoleSession)Session).ApproachParameters = parameters;
                    _viewer.ViewString("Approach dialogue has ended!");
                    break;
                case "recommend":
                    ((ConsoleSession)Session).Recommend();
                    break;
                case "usertype":
                    CreateUserDialog();
                    break;
                case "useradd":
                    if (cmd.Length != 3) { _viewer.WrongCmdErrorMsg(new[] { "useradd", "index-of-field", "what-to-add" }); break; }
                    ((ConsoleSession)Session).AddItemToUser(cmd[1], cmd[2]);
                    break;
                case "userdemo":
                    ((ConsoleSession)Session).UserDemo();
                    break;
                case "userclear":
                    ((ConsoleSession)Session).ClearUser();
                    break;
                case "user":
                    ((ConsoleSession)Session).ShowUser();
                    break;
                case "summary":
                    ((ConsoleSession)Session).ShowSummary();
                    break;
                case "save":
                    if (cmd.Length != 2) { _viewer.WrongCmdErrorMsg(new[] { "save", "session-name" }); break; }
                    Session.SaveSession(cmd[1]);
                    break;
                case "load":
                    if (cmd.Length != 2) { _viewer.WrongCmdErrorMsg(new[] { "load", "session-name" }); break; }
                    Session.LoadSession(cmd[1]);
                    break;
                case "show":
                    Session.ShowSessions();
                    break;
                case "delete":
                    if (cmd.Length != 2) { _viewer.WrongCmdErrorMsg(new[] { "delete", "session-name" }); break; }
                    Session.DeleteSession(cmd[1]);
                    break;
                case "demo":
                    DemoDialog();
                    break;
                default:
                    _viewer.ViewString($"Command not found!{Environment.NewLine}");
                    return;
            }
        }

        private string[] CreateApproachDialog()
        {
            Queue<string> parameters = new Queue<string>();

            _viewer.ViewString("The approaches you can choose from are:");
            string[] approaches = Session.GetAvailableClassesOfAType("Approach");
            _viewer.ShowIndexedArray(approaches);
            _viewer.ViewString($"{Environment.NewLine}Type '[approach-name]' or '[index-of-approach]' to select one. Or press enter to leave the dialogue.");

            string? line = GetChosenValue(Console.ReadLine(), approaches);
            if (line is null) { return new string[0]; }

            string[] ctorParameters = Session.GetApproachCtorParameterTypes(line);
            if (ctorParameters.Length == 0) { return new string[0]; }

            _viewer.ViewString($"{Environment.NewLine}Now you have to fill in the constructor parameters:");
            _viewer.ShowIndexedArray(ctorParameters);

            foreach (var ctorParameter in ctorParameters)
            {
                _viewer.ViewString($"{Environment.NewLine}Choose one of the classes implementing {ctorParameter}:");

                string[] classesImplementing = Session.GetClassesImplementingInterface(ctorParameter);

                _viewer.ShowIndexedArray(classesImplementing);

                line = GetChosenValue(Console.ReadLine(), classesImplementing);
                if (line is null) { return new string[0]; }

                parameters.Enqueue(line);
            }

            return parameters.ToArray();
        }

        private void CreateUserDialog()
        {
            _viewer.ViewString("The user types you can choose from are:");
            string[] userTypes = Session.GetAvailableClassesOfAType("User");
            _viewer.ShowIndexedArray(userTypes);
            _viewer.ViewString($"{Environment.NewLine}Type '[user-type]' or '[index-of-user-type]' to select one. Or press enter to leave the dialogue.");

            string? line = GetChosenValue(Console.ReadLine(), userTypes);
            if (string.IsNullOrEmpty(line)) { _viewer.ViewString("Dialogue aborted!"); return; }

            Type? classtype = Session.GetClassType($"Console{line}");
            if (classtype is null) { _viewer.ViewString("Dialogue aborted!"); return; }

            IConsoleUserUtil? userInstance = (IConsoleUserUtil)Activator.CreateInstance(classtype);
            if (userInstance is null) { _viewer.ViewString("Problem with creating the user instance. Dialogue aborted!"); return; }
            ((ConsoleSession)Session).UserUtil = userInstance;

            _viewer.ViewString($"{Environment.NewLine}User-type successfully selected!");
            _viewer.ViewString($"{Environment.NewLine}Now you'll have to fill in user's fields - this is what you have so far: ");
            _viewer.ShowIndexedArray((userInstance.Show()).Split('\n'));
            _viewer.ViewString($"{Environment.NewLine}To fill in the variables, use 'useradd [index-of-field] [the-thing-you-want-to]'");
            _viewer.ViewString("To see how the user looks so far, type 'user'");
            _viewer.ViewString("To reset the user variables (not its type), use 'userclear'");
            _viewer.ViewString("Dialogue ended.");
        }

        private void DemoDialog()
        {
            var demos = new[] { "MovieDbsDemo", "SisSubjectsDemo" };

            _viewer.ViewString("You may select one of the demos: ");

            _viewer.ShowIndexedArray(demos);

            string? line = GetChosenValue(Console.ReadLine(), demos);
            if (string.IsNullOrEmpty(line)) { _viewer.ViewString("Dialogue aborted!"); return; }

            _viewer.ViewString(line);
            string[]? approachParams = null;

            switch (line)
            {
                case "MovieDbsDemo":
                    Session.LoadFromCsv("u.data");
                    ((ConsoleSession)Session).UserUtil = new ConsoleMovieDbsUser();
                    Session.GetApproachCtorParameterTypes("UserUserCfApproach");
                    approachParams = new[]
                    {
                        "FileStreamLineReader",
                        "UserItemMatrixRatingsPreProcessor",
                        "CosineSimilarityEvaluator",
                        "UserItemMatrixPostProcessor",
                        "SimilarityAverageRatingsPredictor"
                    };
                    break;
                case "SisSubjectsDemo":
                    Session.LoadFromCsv("subjects_11310.csv");
                    ((ConsoleSession)Session).UserUtil = new ConsoleSisUser();
                    Session.GetApproachCtorParameterTypes("StringSimilarityContentBasedApproach");
                    approachParams = new[]
                    {
                        "FileStreamLineReader",
                        "TfIdf",
                        "CosineSimilarityEvaluator",
                        "SimilarityVectorPostProcessor",
                    };
                    break;
                default:
                    _viewer.ViewString("Demo not found!");
                    break;
            }

            if (approachParams is not null)
            {
                Session.SelectApproach(approachParams);
            }
            
            ((ConsoleSession)Session).ApproachParameters = approachParams;
            
            ((ConsoleSession)Session).UserDemo();

            _viewer.ViewString("Demo dialogue has ended!");
            ((ConsoleSession)Session).ShowSummary();
        }

        private string? GetChosenValue(string? line, string[] array)
        {
            bool parsed = int.TryParse(line, out int index);

            if (string.IsNullOrEmpty(line) || (!parsed && !array.Contains(line)))
            {
                return null;
            }

            if (parsed)
            {
                if (index < 0 || array.Length <= index) { return null; }

                return array[index];
            }

            return line;
        }
    }
}
