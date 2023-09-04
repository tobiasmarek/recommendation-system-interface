using System;
using ConsoleRecSys.Interfaces;
using RecommendationSystemInterface;

namespace ConsoleRecSys
{
    /// <summary>
    /// A Session that is controlled and shown in the Console.
    /// </summary>
    public class ConsoleSession : Session
    {
        public IConsoleUserUtil? UserUtil { get; set; } // A ConsoleUser-creating utility
        public string[]? ApproachParameters { get; set; }
        
        private const string UserNotSelected = "User-type not selected";


        public ConsoleSession(ConsoleViewer viewer) : base(viewer) { }

        /// <summary>
        /// Makes sure that the User is created before calling the base Session's function GetRecommendations.
        /// </summary>
        public void Recommend()
        {
            if (UserUtil is null) { Viewer.ViewString(UserNotSelected); return; }
            UserUtil.InitializeUser();
            User = (User)UserUtil;

            Viewer.ViewString($"{Environment.NewLine}This might take a while. (max 3 minutes){Environment.NewLine}");

            base.GetRecommendations();
        }

        public bool DataSourceKnown()
        {
            return !(string.IsNullOrEmpty(DataPath));
        }

        /// <summary>
        /// Item adding handler. Adds item to a property / field of specified index.
        /// </summary>
        public void AddItemToUser(string where, string what)
        {
            if (UserUtil is null) { Viewer.ViewString(UserNotSelected); return; }

            if (!int.TryParse(where, out int index) || index < 0) { Viewer.ViewString("Wrong indexing"); return; }

            if (UserUtil.TryAdd(index, what))
            {
                Viewer.ViewString($"{what} successfully added.");
            }
            else
            {
                Viewer.ViewString("Something went wrong when adding!");
            }
        }

        /// <summary>
        /// Erases user's items.
        /// </summary>
        public void ClearUser()
        {
            if (UserUtil is null) { Viewer.ViewString(UserNotSelected); return; }

            UserUtil.Clear();

            Viewer.ViewString("User variables successfully cleared!");
        }

        public void ShowUser()
        {
            if (UserUtil is null) { Viewer.ViewString(UserNotSelected); return; }

            Viewer.ViewString(UserUtil.Show());
        }

        /// <summary>
        /// Loads user specific demo.
        /// </summary>
        public void UserDemo()
        {
            if (UserUtil is null) { Viewer.ViewString(UserNotSelected); return; }

            UserUtil.Demo();
            Viewer.ViewString($"{Environment.NewLine}Demo successfully selected - here's how the user looks now:");
            Viewer.ViewString(UserUtil.Show());
        }

        public void ShowSummary()
        {
            Viewer.ViewString("");
            Viewer.ViewString("-------------------- SUMMARY --------------------");

            Viewer.ViewString("* DATA *");
            if (DataSourceKnown())
            {
                Viewer.ViewString($"The source of your data: {DataPath}");
            }
            else
            {
                Viewer.ViewString("Source unknown - not selected");
            }

            Viewer.ViewString("* USER *");
            ShowUser();

            Viewer.ViewString("* APPROACH *");
            if (Approach is null || ApproachParameters is null)
            {
                Viewer.ViewString("Approach unknown - not selected");
            }
            else
            {
                foreach (var approachParameter in ApproachParameters)
                {
                    Viewer.ViewString(approachParameter);
                }
            }

            Viewer.ViewString("-------------------------------------------------");
            Viewer.ViewString("");
        }

        /// <summary>
        /// Selects specific user utility.
        /// </summary>
        public void SelectUserType(string className)
        {
            var instance = GetInstance($"Console{className}"); // For it to be Session-specific

            if (instance is IConsoleUserUtil userInstance)
            {
                UserUtil = userInstance;

                Viewer.ViewString($"{Environment.NewLine}User-type successfully selected!");
                Viewer.ViewString($"{Environment.NewLine}Now you'll have to fill in user's fields - this is what you have so far: ");
                ((ConsoleViewer)Viewer).ShowIndexedArray((userInstance.Show()).Split('\n'));
                Viewer.ViewString($"{Environment.NewLine}To fill in the variables, use 'useradd [index-of-field] [the-thing-you-want-to]'");
                Viewer.ViewString("To see how the user looks so far, type 'user'");
                Viewer.ViewString("To reset the user variables (not its type), use 'userclear'");
                Viewer.ViewString("Dialogue ended.");
            }
            else { Viewer.ViewString("Problem with creating the user instance. Dialogue aborted!"); }
        }

        /// <summary>
        /// Loads specific demo ready for recommending.
        /// Selects Approach and User as well.
        /// </summary>
        public void SelectDemo(string line)
        {
            Viewer.ViewString(line);
            string[]? approachParams = null;

            switch (line)
            {
                case "MovieDbsDemo":
                    LoadFromCsv("u.data");
                    UserUtil = new ConsoleMovieDbsUser();
                    GetApproachCtorParameterTypes("UserUserCfApproach");
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
                    LoadFromCsv("subjects_11310.csv");
                    UserUtil = new ConsoleSisUser();
                    GetApproachCtorParameterTypes("StringSimilarityContentBasedApproach");
                    approachParams = new[]
                    {
                        "FileStreamLineReader",
                        "TfIdf",
                        "CosineSimilarityEvaluator",
                        "SimilarityVectorPostProcessor",
                    };
                    break;
                default:
                    Viewer.ViewString("Demo not found!");
                    break;
            }

            if (approachParams is not null)
            {
                SelectApproach(approachParams);
            }

            ApproachParameters = approachParams;

            UserDemo();

            Viewer.ViewString("Demo dialogue has ended!");
            ShowSummary();
        }
    }
}
