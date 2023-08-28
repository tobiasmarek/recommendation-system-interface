using System;
using System.Collections.Generic;
using System.Text;

namespace RecommendationSystemInterface.Interfaces
{
    /// <summary>
    /// Final processing of userItemMatrix.
    /// Results are stored in a file with the returned path.
    /// </summary>
    internal interface IPostProcessor
    {
        string Postprocess(float[][] userItemMatrix);
    }




    /// <summary>
    /// Sorts user-item values for each user in the matrix.
    /// </summary>
    class UserItemMatrixPostProcessor : IPostProcessor
    {
        public string Postprocess(float[][] userItemMatrix)
        {
            string resultsFilePath = "userItemMatrixResults.csv";

            var sw = new FileStreamWriter(resultsFilePath, false);
            var sb = new StringBuilder();

            // PARAMETRIZOVAT
            int decimalPlace = 1; // Number of decimal places taken into consideration for count sort
            int min = 0; // Minimal value of all the matrix values
            int max = 5; // Maximal value
            int conversion = Convert.ToInt32(Math.Pow(10, decimalPlace)); // Conversion used for count sort

            int negativeMinShift = 0; 
            if (min < 0) { negativeMinShift = -min * conversion; } // Shift needed for count sort to start from 0

            foreach (var userItemRatings in userItemMatrix)
            {
                sb.Clear();
                List<int>[] sortedItemRatings = GetCountSortedRatings(userItemRatings, min, max, conversion, negativeMinShift);

                // Iterates from the most positive rating and creates a line with all the item IDs with this rating
                for (int ratingIndex = sortedItemRatings.Length - 1; ratingIndex >= 0; ratingIndex--) // SORTEDITEMRATINGS.LENGTH - 1?
                {
                    if (sortedItemRatings[ratingIndex] is null) { continue; }

                    int numOfItems = sortedItemRatings[ratingIndex].Count;
                    int counter = 1;

                    foreach (var itemID in sortedItemRatings[ratingIndex])
                    {
                        sb.Append($"{itemID} {ratingIndex - negativeMinShift}");

                        if (counter != numOfItems) { sb.Append(','); }
                        else { sb.Append('\n'); } // SPIS NIKDY NEDAVAT \n

                        counter++;
                    }
                }

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

                if (countingList[ratingIndex] is null) { countingList[ratingIndex] = new List<int>(); }

                countingList[ratingIndex].Add(i); // +1??
            }

            return countingList;
        }
    }


    /// <summary>
    /// Sorts similarity values of one user to all items.
    /// </summary>
    class SimilarityVectorPostProcessor : IPostProcessor
    {
        public string Postprocess(float[][] userItemMatrix)
        {
            string resultsFilePath = "userItemSimilarityResults.csv";

            float[] similaritiesVector = userItemMatrix[0];
            var sw = new FileStreamWriter(resultsFilePath, false);

            // PARAMETRIZOVAT
            int decimalPlace = 2; // Number of decimal places taken into consideration for count sort
            int min = -1; // Minimal value of all the matrix values
            int max = 1; // Maximal value
            int conversion = Convert.ToInt32(Math.Pow(10, decimalPlace)); // Conversion used for count sort

            int negativeMinShift = 0;
            if (min < 0) { negativeMinShift = -min * conversion; } // Shift needed for count sort to start from 0

            bool[,] countSortedSimilarities = GetCountSortedSimilaritiesVector(similaritiesVector, min, max, conversion, negativeMinShift);

            // Iterates from the best similarity to the worst and writes a line with all the item indices that have the same similarity value
            for (int similarityIndex = countSortedSimilarities.GetLength(0) - 1; similarityIndex >= 0; similarityIndex--)
            {
                for (int itemIndex = 0; itemIndex < similaritiesVector.Length; itemIndex++)
                {
                    if (countSortedSimilarities[similarityIndex, itemIndex])
                    {
                        sw.Write($"{itemIndex} {similarityIndex - negativeMinShift}\n");
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
                countingMatrix[ratingIndex, i] = true;
            }

            return countingMatrix;
        }
    }
}
