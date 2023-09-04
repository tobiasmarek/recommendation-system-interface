using RecommendationSystemInterface;

namespace WinFormsRecSys.Interfaces
{
    /// <summary>
    /// Session specific utility for users defined through Windows Forms.
    /// Implements a function that retrieves an example of how the user should look in string format
    /// so that the original User class would be derivable from it.
    /// </summary>
    interface IWinFormsUserUtil
    {
        string ShowExampleParamString(); // Shows user demo in a string
        void GetValuesFrom(string parameters); // Derives original User from a string
    }




    /// <summary>
    /// Windows Forms specific SIS User.
    /// </summary>
    class WinFormsSisUser : SisUser, IWinFormsUserUtil
    {
        public WinFormsSisUser(int[] favourites, int[] wishList) : base(favourites, wishList) { }

        public WinFormsSisUser(string parameters) : base(Array.Empty<int>(), Array.Empty<int>())
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
            return @"Favourites: 2, 5, 14, 11, 13, 21
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


    /// <summary>
    /// Windows Forms specific Movie DBS User.
    /// </summary>
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

            UserItemRatings = userItemRatingsResult;
        }

        public string ShowExampleParamString()
        {
            return "User-Item rating (1-5): 227 5, 228 4, 229 5, 230 3";
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
