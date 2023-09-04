using RecommendationSystemInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRecSys.Interfaces
{
    /// <summary>
    /// Session specific utility for users defined through Console.
    /// Capable of deriving original User class through it, showing which items the user contains so far.
    /// And a demo user is defined.
    /// </summary>
    public interface IConsoleUserUtil
    {
        void InitializeUser(); // Recreates original User class from the object implementing this interface
        void Clear(); // Clears so far constructed fields / properties
        string Show(); // Shows what do those fields / properties contain
        void Demo(); // Defines or fills fields / properties that will define the original User
        bool TryAdd(int index, string what); // Adds an item to an index-specified field or a property
    }




    /// <summary>
    /// Console specific SIS User.
    /// </summary>
    class ConsoleSisUser : SisUser, IConsoleUserUtil
    {
        private List<int> _favouritesList;
        private List<int> _wishList;

        public ConsoleSisUser() : base(Array.Empty<int>(), Array.Empty<int>())
        {
            _favouritesList = new List<int>();
            _wishList = new List<int>();
        }

        public void InitializeUser()
        {
            base.Favourites = _favouritesList.ToArray();
            base.WishList = _wishList.ToArray();
        }

        public void Clear()
        {
            _favouritesList.Clear();
            _wishList.Clear();
        }

        public string Show()
        {
            var sb = new StringBuilder();

            sb.Append("Favourites: ");
            for (int i = 0; i < _favouritesList.Count; i++)
            {
                if (i == 0) { sb.Append(_favouritesList[i]); }
                else { sb.Append($", {_favouritesList[i]}"); }
            }

            sb.Append($"{Environment.NewLine}Wish-list: ");
            for (int i = 0; i < _wishList.Count; i++)
            {
                if (i == 0) { sb.Append(_wishList[i]); }
                else { sb.Append($", {_wishList[i]}"); }
            }

            return sb.ToString();
        }

        public void Demo()
        {
            _favouritesList = new List<int>() { 2, 5, 6, 9, 22 };
            _wishList = new List<int>() { 1, 3, 13 };
        }

        public bool TryAdd(int index, string what)
        {
            if (index > 1 || !int.TryParse(what, out int number)) { return false; }

            if (index == 0)
            {
                _favouritesList.Add(number);
            }
            else
            {
                _wishList.Add(number);
            }

            return true;
        }
    }


    /// <summary>
    /// Console specific Movie DBS User.
    /// </summary>
    class ConsoleMovieDbsUser : MovieDbsUser, IConsoleUserUtil
    {
        public ConsoleMovieDbsUser() : base(new Dictionary<int, int>()) { }

        public void InitializeUser() { }

        public void Clear()
        {
            UserItemRatings.Clear();
        }

        public string Show()
        {
            var sb = new StringBuilder();

            bool first = true;
            sb.Append("Item rating (from 1 to 5; [itemId-rating]): ");
            foreach (var userItemRating in UserItemRatings)
            {
                if (first)
                {
                    sb.Append($"{userItemRating.Key}-{userItemRating.Value}");
                    first = false;
                    continue;
                }

                sb.Append($", {userItemRating.Key}-{userItemRating.Value}");
            }

            return sb.ToString();
        }

        public void Demo()
        {
            UserItemRatings = new Dictionary<int, int>() { {8, 4}, {10, 3}, {11, 5}, {20, 2} };
        }

        public bool TryAdd(int index, string what)
        {
            if (index != 0) { return false; }

            var pair = what.Split('-');
            if (pair.Length != 2) { return false; }

            if (!int.TryParse(pair[0], out int itemIndex) || !int.TryParse(pair[1], out int rating)) { return false; }

            if (itemIndex < 0 || rating < 1 || rating > 5) { return false; }

            UserItemRatings[itemIndex] = rating;

            return true;
        }
    }
}
