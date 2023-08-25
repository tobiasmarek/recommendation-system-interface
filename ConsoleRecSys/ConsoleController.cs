using System;
using RecommendationSystemInterface;

namespace ConsoleRecSys
{
    class ConsoleController : Controller
    {
        public ConsoleController(Session session) : base(session)
        {
            TakeInput();
        }

        public void TakeInput()
        {
            Console.WriteLine(@"            case ""loadfromcsv"":
            case ""loadfromdbs"":

            case ""selectapproach"":

            case ""recommend"":

            case ""savesession"":

            case ""loadsession"":

            case ""showsessions"":

            case ""deletesession""");

            string? line;
            while ((line = Console.ReadLine()) is not "" or null)
            {
                Crunch(line!.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries));
            }
        }

        public void Crunch(string[] cmd) // CHCI MIT MOZNOST DEFINOVAT SVOJE CASES
        {
            switch (cmd[0]) // NEBO ZPRACOVAVAT VZDY V INTERFACU?
            {
                case "loadfromcsv":
                    if (cmd.Length != 2) { break; }
                    Session.LoadFromCsv(cmd[1]);
                    break;
                case "loadfromdbs":
                    Session.LoadFromDbs();
                    break;
                case "selectapproach":
                    Session.SelectApproach();
                    break;
                case "recommend":
                    Session.GetRecommendations();
                    break;
                case "savesession":
                    Session.SaveSession(cmd[1]);
                    break;
                case "loadsession":
                    Session.LoadSession(cmd[1]);
                    break;
                case "showsessions":
                    Session.ShowSessions();
                    break;
                case "deletesession":
                    Session.DeleteSession(cmd[1]);
                    break;
                default:
                    return;
            }
        }
    }
}
