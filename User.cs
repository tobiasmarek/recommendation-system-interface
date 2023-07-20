using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;

namespace RecommendationSystem
{
    abstract class User
    {
        private string name { get; set; }
    }




    class SisUser : User
    {
        private DataFrameRow[] favourites { get; set; }
        private DataFrameRow[] wishList { get; set; }
    }
}
