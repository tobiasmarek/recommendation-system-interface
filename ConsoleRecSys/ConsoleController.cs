﻿using System;
using RecommendationSystemInterface;

namespace ConsoleRecSys
{
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

        public void TakeCommand(string[] cmd) // CHCI MIT MOZNOST DEFINOVAT SVOJE CASES
        {
            switch (cmd[0]) // NEBO ZPRACOVAVAT VZDY V INTERFACU?
            {
                case "loadfromcsv":
                    if (cmd.Length == 2)
                    {
                        Session.LoadFromCsv(cmd[1]);
                        break;
                    }

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
                    RecommendationSystemInterface.Session? loadedSession = Session.LoadSession(cmd[1]);
                    if (loadedSession != null)
                    {
                        Session.Controller = this;
                    } // TAKZE BYCH MEL UKLADAT JEN APPROACH, USER A DATA, ABYCH TOHLE NEMUSEL DELAT

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