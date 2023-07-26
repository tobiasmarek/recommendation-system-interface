using Microsoft.Data.Analysis;
using RecommendationSystem.Interfaces;

namespace RecommendationSystem
{
    abstract class Approach
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DataFrame dataFrame { get; set; }

        public ISimilarityEvaluator Evaluator { get; set; }

        public abstract void Recommend();
    }




    abstract class ContentBasedApproach : Approach
    {
        public User User { get; set; }
    }


    class NonModelContentBasedApproach : ContentBasedApproach
    {
        public IDataPreprocessor DataPreprocessor { get; set; }

        public override void Recommend()
        {
            var newColumn = new PrimitiveDataFrameColumn<float>($"{Name}", dataFrame.Rows.Count);

            float[][] numDataFrame = DataPreprocessor.Preprocess(dataFrame);

            float[] userVector = GetUserVector(numDataFrame);

            for (long rowNum = 0; rowNum < numDataFrame.LongLength; rowNum++)
            {
                float similarity = Evaluator.EvaluateSimilarity(numDataFrame[rowNum], userVector);

                newColumn[rowNum] = similarity;
            }

            dataFrame.Columns.Add(newColumn);
        }

        private float[] GetUserVector(float[][] numDataFrame)
        {
            float[] userVector = new float[numDataFrame[0].Length];

            if (User is not SisUser sisUser) { return userVector; }

            foreach (int index in sisUser.Favourites)
            {
                for (int j = 0; j < userVector.Length; j++)
                {
                    userVector[j] += numDataFrame[index][j];
                }
            }

            foreach (int index in sisUser.WishList)
            {
                for (int j = 0; j < userVector.Length; j++)
                {
                    userVector[j] += numDataFrame[index][j];
                }
            }

            int numOfRankedItems = sisUser.Favourites.Length + sisUser.WishList.Length;

            if (numOfRankedItems == 0) { return userVector; }

            for (int i = 0; i < userVector.Length; i++)
            {
                userVector[i] /= numOfRankedItems;
            }

            return userVector;
        }
    }




    abstract class CollaborativeFilteringApproach : Approach
    {
        public User[] Users { get; set; }
    }


    class ModelCollaborativeFilteringApproach : CollaborativeFilteringApproach
    {
        public IDataPreprocessor DataPreprocessor { get; set; }
        public IPredictor Predictor { get; set; }

        public override void Recommend()
        {
            DataPreprocessor.Preprocess(dataFrame);

            Predictor.Predict(dataFrame);

            // DataPostprocessor (sorting, filtering, similarity)

            // Evaluation (precision@K, @)
        }
    }




    abstract class HybridApproach : Approach
    {
        public User[] Users { get; set; }
    }
}
