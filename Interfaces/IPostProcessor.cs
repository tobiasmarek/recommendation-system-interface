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
                List<int>[] sortedItemRatings = CountingSortRatings(userItemRatings, 0, 5, decimalPlace);

                for (int ratingIndex = 0; ratingIndex < sortedItemRatings.Length; ratingIndex++)
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

        List<int>[] CountingSortRatings(float[] userItemRatings, int min, int max, int decimalPlace)
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

        }
    }
}
