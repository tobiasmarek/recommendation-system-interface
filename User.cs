using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    abstract class User
    {
        private string name { get; set; }
    }




    class SisUser : User
    {
        private Dictionary<string, string> interests { get; set; }
    }
}
