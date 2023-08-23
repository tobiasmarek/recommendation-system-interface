using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecommendationSystemInterface
{
    /// <summary>
    /// Base Controller class that defines the most basic commands.
    /// Takes input and according to it, further actions are made with TakeCommand.
    /// </summary>
    public abstract class Controller
    {
        public Session Session { get; set; } // The Session it controls

        public abstract void TakeInput(); // NEBO MISTO TOHO JEN INTERFACE KTEREJ TakeInput IMPLEMENTUJE A PARSUJE ODKUDKOLIV

        public void TakeCommand(string[] cmd) // CHCI MIT MOZNOST DEFINOVAT SVOJE CASES
        {
            switch (cmd[0]) // NEBO ZPRACOVAVAT VZDY V INTERFACU?
            {
            case "loadfromcsv":
                if (cmd.Length == 2) {Session.LoadFromCsv(cmd[1]);}
                else {Session.LoadFromCsv();}
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
                Session? loadedSession = Session.LoadSession(cmd[1]);
                if (loadedSession != null) { Session.Controller = this; } // TAKZE BYCH MEL UKLADAT JEN APPROACH, USER A DATA, ABYCH TOHLE NEMUSEL DELAT
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




    /// <summary>
    /// Controller that takes input and parses it from the Console.
    /// </summary>
    class ConsoleController : Controller
    {
        public override void TakeInput()
        {
            Console.WriteLine(@"            case ""loadfromcsv"":
                Session.LoadFromCsv();
                break;
            case ""loadfromdbs"":
                Session.LoadFromDbs();
                break;
            case ""selectapproach"":
                Session.SelectApproach();
                break;
            case ""recommend"":
                Session.GetRecommendations();
                break;
            case ""savesession"":
                Session.SaveSession(cmd[1]);
                break;
            case ""loadsession"":
                Session? loadedSession = Session.LoadSession(cmd[1]);
                if (loadedSession != null) { Session.Controller = this; } // takze bych mel spis ukladat jen approach, user a data, abych nemusel tohle
                break;
            case ""showsessions"":
                Session.ShowSessions();
                break;
            case ""deletesession"":
                Session.DeleteSession(cmd[1]);");

            string? line;
            while ((line = Console.ReadLine()) != "")
            {
                TakeCommand(line.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries));
            }
        }
    }


    /// <summary>
    /// Controller that takes and parses the input from the web.
    /// </summary>
    class WebController : Controller
    {
        public override void TakeInput()
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Controller that takes and parses input from Windows Forms interactions.
    /// </summary>
    class WinFormsController : Controller
    {
        public override void TakeInput()
        {
            throw new NotImplementedException();
        }
    }
}
