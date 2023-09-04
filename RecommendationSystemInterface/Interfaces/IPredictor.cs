namespace RecommendationSystemInterface.Interfaces
{
    /// <summary>
    /// Completing userItemMatrix.
    /// Predicting its missing values based on similarities.
    /// </summary>
    internal interface IPredictor
    {
        void Predict(float[][] userItemMatrix, float[][] similarities);
    }




    /// <summary>
    /// Making an average rating prediction based on similarity between users.
    /// </summary>
    class SimilarityAverageRatingsPredictor : IPredictor
    {
        public void Predict(float[][] userItemMatrix, float[][] similarities)
        {
            for (int userIndex = 0; userIndex < userItemMatrix.GetLength(0); userIndex++)
            {
                float[] similaritiesVector = GetSimilarityVector(userIndex, similarities);

                for (int itemIndex = 0; itemIndex < userItemMatrix.GetLength(0); itemIndex++)
                {
                    if (userItemMatrix[userIndex][itemIndex] != 0) { continue ;}

                    float[] ratingsVector = GetRatingsVector(itemIndex, userItemMatrix);

                    userItemMatrix[userIndex][itemIndex] = GetWeightedRating(similaritiesVector, ratingsVector);
                }
            }
        }

        /// <summary>
        /// Predicts the user's rating of unrated item.
        /// predicted_rating = (SUM sim(target_user, user_i) * user_i_rating) / (SUM sim(target_user, user_i))
        /// </summary>
        private float GetWeightedRating(float[] similaritiesVector, float[] ratingsVector)
        {
            float numerator = 0;
            float denominator = 0;
            for (int i = 0; i < similaritiesVector.Length; i++)
            {
                numerator += similaritiesVector[i] * ratingsVector[i];
                denominator += similaritiesVector[i];
            }

            return numerator / denominator;
        }

        /// <summary>
        /// Since similarities matrix is a symmetric one and was calculated efficiently we must extract
        /// the real values of the user's similarity vector differently than just taking its row.
        /// </summary>
        private float[] GetSimilarityVector(int targetUser, float[][] similarities)
        {
            float[] similaritiesVector = new float[similarities.GetLength(0)];

            for (int j = 0; j < targetUser; j++)
            {
                similaritiesVector[j] = similarities[targetUser][j];
            }

            for (int i = targetUser + 1; i < similaritiesVector.Length; i++)
            {
                similaritiesVector[i] = similarities[i][targetUser];
            }

            return similaritiesVector;
        }

        /// <summary>
        /// Extract the vector of item ratings from userItemMatrix from all users.
        /// </summary>
        private float[] GetRatingsVector(int targetItem, float[][] userItemMatrix)
        {
            float[] ratingsVector = new float[userItemMatrix.GetLength(0)];

            for (int i = 0; i < ratingsVector.Length; i++)
            {
                ratingsVector[i] = userItemMatrix[i][targetItem];
            }

            return ratingsVector;
        }
    }
}
