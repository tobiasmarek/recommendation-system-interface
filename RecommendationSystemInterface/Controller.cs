﻿using System;
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
        protected Session Session; // The Session it controls

        protected Controller(Session session)
        {
            Session = session;
        }

        // NEBO MISTO TOHO JEN INTERFACE KTEREJ TakeInput IMPLEMENTUJE A PARSUJE ODKUDKOLIV
    }




    /// <summary>
    /// Controller that takes input and parses it from the Console.
    /// </summary>
    class ConsoleController : Controller
    {
        public ConsoleController(Session session) : base(session)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Controller that takes and parses the input from the web.
    /// </summary>
    class WebController : Controller
    {
        public WebController(Session session) : base(session)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Controller that takes and parses input from Windows Forms interactions.
    /// </summary>
    class WinFormsController : Controller
    {
        public WinFormsController(Session session) : base(session)
        {
            throw new NotImplementedException();
        }
    }
}
