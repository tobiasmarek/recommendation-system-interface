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

        public abstract string ShowExampleParamString();
        public abstract void GetValuesFrom(string parameters);

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

        public SisUser(string parameters) : base(new SisUserVectorizer())
        {
            GetValuesFrom(parameters);
        }

        public sealed override void GetValuesFrom(string parameters)
        {
            string[] twoParameters = parameters.Split(Environment.NewLine);
            if (twoParameters.Length != 2) { return; }

            int[]? favResult = GetParamOutOfLine(twoParameters[0]);
            if (favResult is null) { GetValuesFrom(ShowExampleParamString()); return; }

            Favourites = favResult;

            int[]? wishResult = GetParamOutOfLine(twoParameters[0]);
            if (wishResult is null) { GetValuesFrom(ShowExampleParamString()); return; }

            WishList = wishResult;
        }

        public override string ShowExampleParamString()
        {
            return @"Favourites: 0, 5, 14, 11, 13, 21
Wish-list: 1, 10, 12";
        }

        private int[]? GetParamOutOfLine(string line)
        {
            string[] lineParams = line.Split(":");
            if (lineParams.Length != 2) return null;

            lineParams = lineParams[1].Split(",", StringSplitOptions.RemoveEmptyEntries);

            int num;
            int[] result = new int[lineParams.Length];
            for (int i = 0; i < lineParams.Length; i++)
            {
                if (!int.TryParse(lineParams[i], out num)) { return null; }
                result[i] = num;
            }

            return result;
        }
    }

    class MovieDbsUser : User
    {
        public Dictionary<int, int> userItemRatings { get; set; }

        public MovieDbsUser(Dictionary<int, int> userItemRatings) : base(new MovieDbsUserVectorizer())
        {
            this.userItemRatings = userItemRatings;
        }

        public MovieDbsUser(string parameters) : base(new MovieDbsUserVectorizer())
        {
            GetValuesFrom(parameters);
        }

        public sealed override void GetValuesFrom(string parameters)
        {

        }

        public override string ShowExampleParamString()
        {
            return "User-Item rating (1-5): 22 4, 11 5, 30 2, 11 1, 3 5, 4 5";
        }
    }
}
