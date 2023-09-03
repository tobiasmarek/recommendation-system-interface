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
        public WinFormsSession(Viewer viewer) : base(viewer)
        {


        }

        public void SetTemplateDataPath(string path)
        {
            if (path == "") { TemplateDataPath = null; return; }

            if (File.Exists(path))
            {
                TemplateDataPath = path;
            }
            else
            {
                var newPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "Data", path));

                if (File.Exists(newPath))
                {
                    TemplateDataPath = newPath;
                }
                else
                {
                    TemplateDataPath = null;
                    Viewer.ViewString("Template file does not exist!");
                }
            }
        }
    }
}
