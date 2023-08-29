using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecommendationSystemInterface.Interfaces;

namespace RecommendationSystemInterface
{
    public abstract class User
    {
        public IUserVectorizer? UserVectorizer { get; set; }

        protected User(IUserVectorizer vectorizer)
        {
            UserVectorizer = vectorizer;
        }
    }




    public class SisUser : User
    {
        public int[] Favourites { get; set; }
        public int[] WishList { get; set; }

        public SisUser(int[] favourites, int[] wishList) : base(new SisUserVectorizer())
        {
            Favourites = favourites;
            WishList = wishList;
        }
    }

    public class MovieDbsUser : User
    {
        public Dictionary<int, int> userItemRatings { get; set; }

        public MovieDbsUser(Dictionary<int, int> userItemRatings) : base(new MovieDbsUserVectorizer())
        {
            this.userItemRatings = userItemRatings;
        }
    }
}
