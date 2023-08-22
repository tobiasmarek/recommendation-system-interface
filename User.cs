using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    class User
    {
        private string userID { get; set; }
    }




    class SisUser : User
    {
        public int[] Favourites { get; set; } = new int[] {9,10,11,12};
        public int[] WishList { get; set; } = new int[0];
    }
}
