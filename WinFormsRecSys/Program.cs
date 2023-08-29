using RecommendationSystemInterface;
using RecommendationSystemInterface.Interfaces;

namespace WinFormsRecSys
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }




    interface IWinFormsUserUtil
    {
        string ShowExampleParamString();
        void GetValuesFrom(string parameters);
    }


    class WinFormsSisUser : SisUser, IWinFormsUserUtil
    {
        public WinFormsSisUser(int[] favourites, int[] wishList) : base(favourites, wishList) { }

        public WinFormsSisUser(string parameters) : base(new int[0], new int[0])
        {
            GetValuesFrom(parameters);
        }

        public void GetValuesFrom(string parameters)
        {
            string[] twoParameters = parameters.Split(Environment.NewLine);
            if (twoParameters.Length != 2) { GetValuesFrom(ShowExampleParamString()); return; }

            int[]? favResult = GetParamOutOfLine(twoParameters[0]);
            if (favResult is null) { GetValuesFrom(ShowExampleParamString()); return; }

            Favourites = favResult;

            int[]? wishResult = GetParamOutOfLine(twoParameters[0]);
            if (wishResult is null) { GetValuesFrom(ShowExampleParamString()); return; }

            WishList = wishResult;
        }

        public string ShowExampleParamString()
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

    class WinFormsMovieDbsUser : MovieDbsUser, IWinFormsUserUtil
    {
        public WinFormsMovieDbsUser(Dictionary<int, int> userItemRatings) : base(userItemRatings) { }

        public WinFormsMovieDbsUser(string parameters) : base(new Dictionary<int, int>())
        {
            GetValuesFrom(parameters);
        }

        public void GetValuesFrom(string parameters)
        {
            var userItemRatingsResult = GetParamOutOfLine(parameters);
            if (userItemRatingsResult is null) { GetValuesFrom(ShowExampleParamString()); return; }

            userItemRatings = userItemRatingsResult;
        }

        public string ShowExampleParamString()
        {
            return "User-Item rating (1-5): 22 4, 11 5, 30 2, 11 1, 3 5, 4 5";
        }

        private Dictionary<int, int>? GetParamOutOfLine(string line)
        {
            string[] lineParams = line.Split(":");
            if (lineParams.Length != 2) return null;

            lineParams = lineParams[1].Split(",");

            int index; int rating;
            Dictionary<int, int> result = new();
            foreach (var pairStr in lineParams)
            {
                string[] pair = pairStr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (pair.Length != 2 || !int.TryParse(pair[0], out index) || !int.TryParse(pair[1], out rating)) return null;

                result[index] = rating;
            }

            return result;
        }
    }
}