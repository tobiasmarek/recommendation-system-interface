using RecommendationSystem.Interfaces;

namespace RecommendationSystem
{
    abstract class Approach
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IDisposableLineReader RecordReader { get; set; }
        public IPreProcessor PreProcessor { get; set; }
        public ISimilarityEvaluator Evaluator { get; set; }
        public IPostProcessor PostProcessor { get; set; }

        public abstract string Recommend();
    }




    abstract class ContentBasedApproach : Approach
    {
        public User User { get; set; }
    }


    class StringSimilarityContentBasedApproach : ContentBasedApproach
    {
        public override string Recommend()
        {
            float[][] dataMatrix = PreProcessor.Preprocess(RecordReader);

            float[] userVector = GetUserVector(dataMatrix);

            float[] similaritiesVector = new float[dataMatrix.GetLongLength(0)];

            for (long rowNum = 0; rowNum < similaritiesVector.LongLength; rowNum++)
            {
                similaritiesVector[rowNum] = Evaluator.EvaluateSimilarity(dataMatrix[rowNum], userVector);
            }
            // NEMÁM NASTAVENEJ POSTPROCESSOR
            string resultsFilePath = PostProcessor.Postprocess(new float[][] { similaritiesVector }); // tohle jinak
            // DataPostprocessor (sorting, filtering, similarity) z prediktly hodnoceni treba seradim a udelam nejakou filtraci

            // Evaluation (precision@K, @) vyhodim prvnich 10 idk a pak najdu podle indexu zaznamy predmetů a ty vyhodim
            return resultsFilePath;
        }

        private float[] GetUserVector(float[][] dataMatrix)
        {
            float[] userVector = new float[dataMatrix[0].Length];

            if (User is not SisUser sisUser) { return userVector; }

            foreach (int index in sisUser.Favourites)
            {
                for (int j = 0; j < userVector.Length; j++)
                {
                    userVector[j] += dataMatrix[index][j];
                }
            }

            foreach (int index in sisUser.WishList)
            {
                for (int j = 0; j < userVector.Length; j++)
                {
                    userVector[j] += dataMatrix[index][j];
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


    class UserUserCfApproach : CollaborativeFilteringApproach
    {
        public IPredictor Predictor { get; set; }

        public override string Recommend()
        {
            float[][] userItemMatrix = PreProcessor.Preprocess(RecordReader);  // vyrobit user-item matici

            float[][] userSimilarities = new float[userItemMatrix.LongLength][]; // čtvercová symetric matrix tvořená users

            for (int i = 0; i < userSimilarities.GetLength(0); i++)
            {
                userSimilarities[i] = new float[i];

                for (int j = 0; j < i; j++)
                {
                    if (i == j) { continue; }
                    userSimilarities[i][j] = Evaluator.EvaluateSimilarity(userItemMatrix[i], userItemMatrix[j]);
                }
            }

            Predictor.Predict(userItemMatrix, userSimilarities); // prediktnout kazdou chybejici value
            // tady by mělo probíhat i učení modelu tzn. tady by měla být evaluation measure for information retrieval
            // Evaluation (precision@K, @) vyhodim prvnich 10 idk a pak najdu podle indexu zaznamy predmetů a ty vyhodim

            string resultsFilePath = PostProcessor.Postprocess(userItemMatrix); // tzn potrebuju user-history (nebo aspoň jeho nepredicted ratings) a items (jejich popisy)
            // DataPostprocessor (sorting, filtering, similarity) z prediktly hodnoceni treba seradim a udelam nejakou filtraci

            return resultsFilePath;
        }
    }


    class ItemItemCfApproach : CollaborativeFilteringApproach
    {
        public IPredictor Predictor { get; set; }

        public override string Recommend()
        {
            throw new NotImplementedException();
        }
    }




    abstract class HybridApproach : Approach
    {
        public User[] Users { get; set; }
    }
}
