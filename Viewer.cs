using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;

namespace RecommendationSystem
{
    abstract class Viewer // neni to přehrocený? nemam jen dát jednu class a předávat kam se to zobrazuje?
                          // nebo předávat interface? nebo dokonce pouze interface?
    {
        public abstract void View(DataFrame data);
    }


    class WebViewer : Viewer
    {
        public override void View(DataFrame data)
        {
            throw new NotImplementedException();
        }
    }

    class ConsoleViewer : Viewer
    {
        public override void View(DataFrame data)
        {
            Console.WriteLine(data);
        }
    }
}
