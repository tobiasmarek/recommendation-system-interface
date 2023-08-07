﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    abstract class Controller // neni to přehrocený? nemam jen dát jednu class a předávat kam se to zobrazuje
                              // nebo předávat interface? nebo dokonce pouze interface?
    {
        public Session Session { get; set; }

        public abstract void TakeInput(); // nebo prostě interface dokáže naparsovat přikaz odkudkoliv

        public void TakeCommand(string[] cmd)
        {

            switch (cmd[0]) // nebo to nejak jinak zpracovat
            {
            case "loadfromcsv":
                Session.LoadFromCsv();
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
                if (loadedSession != null) { Session.Controller = this; } // takze bych mel spis ukladat jen approach, user a data, abych nemusel tohle
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


    class ConsoleController : Controller
    {
        public override void TakeInput()
        {
            Console.WriteLine(@"case ""loadcsv"":
    Session.LoadCsv();
    break;
case ""savecsv"":
    Session.SaveCsv();
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
    Session = Session.LoadSession(cmd[1]);
    Session.Controller = this;
    break;
case ""showsessions"":
    Session.ShowSessions();
    break;
case ""deletesession"":
    Session.DeleteSession(cmd[1]);
    break;
default:
    return;");

            string? line;
            while ((line = Console.ReadLine()) != null)
            {
                TakeCommand(line.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries));
            }
        }
    }


    class WebController : Controller
    {
        public override void TakeInput()
        {
            throw new NotImplementedException();
        }
    }
}
