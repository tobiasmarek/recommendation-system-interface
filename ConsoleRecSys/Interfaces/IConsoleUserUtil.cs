﻿using RecommendationSystemInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRecSys.Interfaces
{
    public interface IConsoleUserUtil
    {
        void InitializeUser();
        void Clear();
        string Show();
        void Demo();
        bool TryAdd(int index, string what);
    }




    class ConsoleSisUser : SisUser, IConsoleUserUtil
    {
        private List<int> _favouritesList;
        private List<int> _wishList;

        public ConsoleSisUser(int[] favourites, int[] wishList) : base(favourites, wishList) { }

        public ConsoleSisUser() : base(new int[0], new int[0])
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

    class ConsoleMovieDbsUser : MovieDbsUser, IConsoleUserUtil
    {
        public ConsoleMovieDbsUser() : base(new Dictionary<int, int>()) { }

        public void InitializeUser() { }

        public void Clear()
        {
            userItemRatings.Clear();
        }

        public string Show()
        {
            var sb = new StringBuilder();

            bool first = true;
            sb.Append("Item rating (from 1 to 5; [itemId-rating]): ");
            foreach (var userItemRating in userItemRatings)
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
            userItemRatings = new Dictionary<int, int>() { {8, 4}, {10, 3}, {11, 5}, {20, 2} };
        }

        public bool TryAdd(int index, string what)
        {
            if (index != 0) { return false; }

            var pair = what.Split('-');
            if (pair.Length != 2) { return false; }

            if (!int.TryParse(pair[0], out int itemIndex) || !int.TryParse(pair[1], out int rating)) { return false; }

            if (itemIndex < 0 || rating < 1 || rating > 5) { return false; }

            userItemRatings[itemIndex] = rating;

            return true;
        }
    }
}
