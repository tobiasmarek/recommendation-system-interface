using System;
using System.Collections.Generic;
using System.Text;

namespace RecommendationSystemInterface.Interfaces
{
    internal interface IPostProcessor
    {
        string Postprocess(float[][] userItemMatrix);
    }




    class UserItemMatrixPostProcessor : IPostProcessor
    {
        public string Postprocess(float[][] userItemMatrix)
        {
            string resultsFilePath = "userItemMatrixResults.csv";

            var sw = new FileStreamWriter(resultsFilePath, false);
            var sb = new StringBuilder();

            int decimalPlace = 1;
            int min = 0; // parametrizovat
            int max = 5;
            int conversion = Convert.ToInt32(Math.Pow(10, decimalPlace));

            int negativeMin = 0;
            if (min < 0) { negativeMin = min * conversion; }

            foreach (var userItemRatings in userItemMatrix)
            {
                sb.Clear();
                List<int>[] sortedItemRatings = GetCountSortedRatings(userItemRatings, min, max, conversion);

                for (int ratingIndex = sortedItemRatings.Length - 1; ratingIndex >= 0; ratingIndex--) // sortedItemRatings.Length - 1?
                {
                    if (sortedItemRatings[ratingIndex] is null) { continue; }

                    int numOfItems = sortedItemRatings[ratingIndex].Count;
                    int counter = 1;

                    foreach (var itemID in sortedItemRatings[ratingIndex])
                    {
                        sb.Append($"{itemID} {ratingIndex + negativeMin}");

                        if (counter != numOfItems) { sb.Append(','); }
                        else { sb.Append('\n'); } // spis nikdy nedavat \n

                        counter++;
                    }
                }

                sw.Write(sb.ToString());
            }

            sw.Dispose();

            return resultsFilePath;
        }

        List<int>[] GetCountSortedRatings(float[] userItemRatings, int min, int max, int conversion)
        {
            List<int>[] countingList = new List<int>[(max - min) * conversion + 1];

            int negativeMin = 0;
            if (min < 0) { negativeMin = -min * conversion; }

            for (int i = 0; i < userItemRatings.Length; i++)
            {
                int ratingIndex = (int)Math.Round(userItemRatings[i] * conversion + negativeMin);

                if (countingList[ratingIndex] is null) { countingList[ratingIndex] = new List<int>(); }

                countingList[ratingIndex].Add(i); // +1?
            }

            return countingList;
        }
    }


    class SimilarityVectorPostProcessor : IPostProcessor
    {
        public string Postprocess(float[][] userItemMatrix)
        {
            string resultsFilePath = "userItemSimilarityResults.csv";

            float[] similaritiesVector = userItemMatrix[0];
            var sw = new FileStreamWriter(resultsFilePath);

            int decimalPlace = 2;
            int min = -1; // parametrizovat
            int max = 1;
            int conversion = Convert.ToInt32(Math.Pow(10, decimalPlace));

            int negativeMin = 0;
            if (min < 0) { negativeMin = min * conversion; }

            bool[,] countSortedSimilarities = GetCountSortedSimilaritiesVector(similaritiesVector, min, max, conversion);

            for (int similarityIndex = countSortedSimilarities.GetLength(0) - 1; similarityIndex >= 0; similarityIndex--)
            {
                for (int itemIndex = 0; itemIndex < similaritiesVector.Length; itemIndex++)
                {
                    if (countSortedSimilarities[similarityIndex, itemIndex]) { sw.Write($"{itemIndex} {similarityIndex + negativeMin}\n"); }
                }
            }

            sw.Dispose();

            return resultsFilePath;
        }

        bool[,] GetCountSortedSimilaritiesVector(float[] similaritiesVector, int min, int max, int conversion)
        {
            bool[,] countingArray = new bool[(max - min) * conversion + 1, similaritiesVector.Length];

            int negativeMin = 0;
            if (min < 0) { negativeMin = -min * conversion; }

            for (int i = 0; i < similaritiesVector.Length; i++)
            {
                int ratingIndex = (int)Math.Round(similaritiesVector[i] * conversion + negativeMin);
                countingArray[ratingIndex, i] = true;
            }

            return countingArray;
        }
    }
}
