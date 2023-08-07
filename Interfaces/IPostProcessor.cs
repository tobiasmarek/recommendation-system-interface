using System.Text;

namespace RecommendationSystem.Interfaces
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

            var sw = new FileStreamWriter(resultsFilePath);
            var sb = new StringBuilder();

            foreach (var userItemRatings in userItemMatrix)
            {
                sb.Clear();
                int decimalPlace = 1;
                List<int>[] sortedItemRatings = GetCountSortedRatings(userItemRatings, 0, 5, decimalPlace);

                for (int ratingIndex = sortedItemRatings.Length; ratingIndex >= 0; ratingIndex--) // sortedItemRatings.Length - 1?
                {
                    int numOfItems = sortedItemRatings[ratingIndex].Count;
                    int counter = 1;

                    foreach (var itemID in sortedItemRatings[ratingIndex])
                    {
                        sb.Append($"{itemID} {ratingIndex}");

                        if (counter != numOfItems) { sb.Append(','); }
                        else { sb.Append('\n'); }

                        counter++;
                    }
                }

                sw.Write(sb.ToString());
            }

            sw.Dispose();

            return resultsFilePath;
        }

        List<int>[] GetCountSortedRatings(float[] userItemRatings, int min, int max, int decimalPlace)
        {
            int conversion = Convert.ToInt32(Math.Pow(10, decimalPlace));
            List<int>[] countingList = new List<int>[(max - min) * conversion + 1];

            for (int i = 0; i < userItemRatings.Length; i++)
            {
                int ratingIndex = (int)Math.Round(userItemRatings[i] * conversion);
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

            int decimalPlace = 2;
            float[] similaritiesVector = userItemMatrix[0];
            var sw = new FileStreamWriter(resultsFilePath);
            bool[,] countSortedSimilarities = GetCountSortedSimilaritiesVector(similaritiesVector, -1, 1, decimalPlace);

            for (int similarityIndex = countSortedSimilarities.GetLength(0); similarityIndex >= 0; similarityIndex--) // dim 0? // -1?
            {
                for (int itemIndex = 0; itemIndex < similaritiesVector.Length; itemIndex++)
                {
                    if (countSortedSimilarities[similarityIndex, itemIndex]) { sw.Write($"{itemIndex} {similarityIndex}\n"); }
                }
            }

            return resultsFilePath;
        }

        bool[,] GetCountSortedSimilaritiesVector(float[] similaritiesVector, int min, int max, int decimalPlace)
        {
            int conversion = Convert.ToInt32(Math.Pow(10, decimalPlace));
            bool[,] countingArray = new bool[(max - min) * conversion + 1, similaritiesVector.Length];

            for (int i = 0; i < similaritiesVector.Length; i++)
            {
                int ratingIndex = (int)Math.Round(similaritiesVector[i] * conversion);
                countingArray[ratingIndex, i] = true;
            }

            return countingArray;
        }
    }
}
