using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    abstract class Viewer // neni to přehrocený? nemam jen dát jednu class a předávat kam se to zobrazuje?
                          // nebo předávat interface? nebo dokonce pouze interface?
    {

    }


    class WebViewer : Viewer
    {

    }

    class ConsoleViewer : Viewer
    {

    }
}
