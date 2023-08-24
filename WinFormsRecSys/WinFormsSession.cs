using RecommendationSystemInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsRecSys
{
    public class WinFormsSession : Session
    {
        public WinFormsSession(TextBox outputTextBox)
        {
            Controller = new WinFormsController { Session = this };
            Viewer = new WinFormsViewer { OutputTextBox = outputTextBox };
        }
    }
}
