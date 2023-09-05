using System;
using System.Collections.Generic;
using System.Text;

namespace RecommendationSystemInterface.Interfaces
{
    /// <summary>
    /// Final processing of userItemMatrix.
    /// Results are stored in a file with the returned path.
    /// </summary>
    public interface IPostProcessor
    {
        string Postprocess(float[][] userItemMatrix);
    }




    /// <summary>
    /// Sorts user-item values for each user in the matrix.
    /// </summary>
    class UserItemMatrixPostProcessor : IPostProcessor
    {
        private readonly int _min; // Minimal value of all the matrix values
        private readonly int _max; // Maximal value
        private readonly int _conversion; // Conversion used for count sort

        public UserItemMatrixPostProcessor() : this(3, 0, 5) { }

        public UserItemMatrixPostProcessor(int decimalPlace, int min, int max)
        {
            _min = min;
            _max = max;
            _conversion = Convert.ToInt32(Math.Pow(10, decimalPlace)); // Number of decimal places taken into consideration for count sort
        }

        public string Postprocess(float[][] userItemMatrix)
        {
            string resultsFilePath = "userItemMatrixResults.csv";

            var sw = new FileStreamWriter(resultsFilePath, false);
            var sb = new StringBuilder();

            int negativeMinShift = 0; 
            if (_min < 0) { negativeMinShift = -_min * _conversion; } // Shift needed for count sort to start from 0

            foreach (var userItemRatings in userItemMatrix)
            {
                sb.Clear();
                List<int>[] sortedItemRatings = GetCountSortedRatings(userItemRatings, _min, _max, _conversion, negativeMinShift);

                // Iterates from the most positive rating and creates a line with all the item IDs with this rating
                for (int ratingIndex = sortedItemRatings.Length - 1; ratingIndex >= 0; ratingIndex--)
                {
                    if (sortedItemRatings[ratingIndex] is null) { continue; }

                    int numOfItems = sortedItemRatings[ratingIndex].Count;
                    int counter = 1;

                    foreach (var itemID in sortedItemRatings[ratingIndex])
                    {
                        sb.Append($"{itemID} {ratingIndex - negativeMinShift},");

                        counter++;
                    }
                }

                sb.Append('\n');
                sw.Write(sb.ToString());
            }

            sw.Dispose();

            return resultsFilePath;
        }

        /// <summary>
        /// Count sort algorithm implemented for sorting a row of a matrix (userItemRatings).
        /// Indices are not lost in the process.
        /// </summary>
        /// <returns>
        /// A matrix where the number of rows is fixed and equal to the number of possible rating values
        /// and where the columns are expandable and equal to item indices with the same item rating as the row index is equal to.
        /// </returns>
        List<int>[] GetCountSortedRatings(float[] userItemRatings, int min, int max, int conversion, int negativeMinShift)
        {
            List<int>[] countingList = new List<int>[(max - min) * conversion + 1];

            for (int i = 0; i < userItemRatings.Length; i++)
            {
                int ratingIndex = (int)Math.Round(userItemRatings[i] * conversion + negativeMinShift);

                if (ratingIndex < 0 || ratingIndex >= countingList.Length) { continue; }
                if (countingList[ratingIndex] is null) { countingList[ratingIndex] = new List<int>(); }

                countingList[ratingIndex].Add(i);
            }

            return countingList;
        }
    }


    /// <summary>
    /// Sorts similarity values of one user to all items.
    /// </summary>
    class SimilarityVectorPostProcessor : IPostProcessor
    {
        private readonly int _min; // Minimal value of all the matrix values
        private readonly int _max; // Maximal value
        private readonly int _conversion; // Conversion used for count sort

        public SimilarityVectorPostProcessor() : this(3, -1, 1) { }

        public SimilarityVectorPostProcessor(int decimalPlace, int min, int max)
        {
            _min = min;
            _max = max;
            _conversion = Convert.ToInt32(Math.Pow(10, decimalPlace)); // Number of decimal places taken into consideration for count sort
        }

        public string Postprocess(float[][] userItemMatrix)
        {
            string resultsFilePath = "userItemSimilarityResults.csv";

            float[] similaritiesVector = userItemMatrix[0];
            var sw = new FileStreamWriter(resultsFilePath, false);

            int negativeMinShift = 0;
            if (_min < 0) { negativeMinShift = -_min * _conversion; } // Shift needed for count sort to start from 0

            bool[,] countSortedSimilarities = GetCountSortedSimilaritiesVector(similaritiesVector, _min, _max, _conversion, negativeMinShift);

            // Iterates from the best similarity to the worst and writes a line with all the item indices that have the same similarity value
            for (int similarityIndex = countSortedSimilarities.GetLength(0) - 1; similarityIndex >= 0; similarityIndex--)
            {
                for (int itemIndex = 0; itemIndex < similaritiesVector.Length; itemIndex++)
                {
                    if (countSortedSimilarities[similarityIndex, itemIndex])
                    {
                        sw.Write($"{itemIndex} {similarityIndex - negativeMinShift},");
                    }
                }
            }

            sw.Dispose();

            return resultsFilePath;
        }

        /// <summary>
        /// Count sort algorithm implemented for sorting a similarity (user-items) vector.
        /// Indices are not lost in the process.
        /// </summary>
        /// <returns>
        /// A boolean matrix where the number of rows is equal to the number of possible rating values
        /// and where the number of columns is equal to the number of item indices.
        /// If countingMatrix[ratingIndex, i] == true then there is an item with index i that has the similarity of ratingIndex.
        /// </returns>
        bool[,] GetCountSortedSimilaritiesVector(float[] similaritiesVector, int min, int max, int conversion, int negativeMinShift)
        {
            bool[,] countingMatrix = new bool[(max - min) * conversion + 1, similaritiesVector.Length];

            for (int i = 0; i < similaritiesVector.Length; i++)
            {
                int ratingIndex = (int)Math.Round(similaritiesVector[i] * conversion + negativeMinShift);
                if (ratingIndex < 0 || ratingIndex >= countingMatrix.GetLength(0)) { continue; }
                countingMatrix[ratingIndex, i] = true;
            }

            return countingMatrix;
        }
    }
}
