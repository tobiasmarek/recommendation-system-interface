using System;

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
    class UserSimilarityAverageRatingsPredictor : IPredictor
    {
        public void Predict(float[][] userItemMatrix, float[][] similarities)
        {
            for (int userIndex = 0; userIndex < userItemMatrix.GetLongLength(0); userIndex++)
            {
                float[] similaritiesVector = GetSimilarityVector(userIndex, similarities);

                for (int itemIndex = 0; itemIndex < userItemMatrix.GetLongLength(0); itemIndex++)
                {
                    if (userItemMatrix[userIndex][itemIndex] != 0) { continue ;}

                    float[] ratingsVector = GetRatingsVector(itemIndex, userItemMatrix);

                    userItemMatrix[userIndex][itemIndex] = GetWeightedRating(similaritiesVector, ratingsVector);
                }
            }
        }

        /// <summary>
        /// Predicts the user's rating of unrated item.
        /// predicted_rating = (SUM sim(target_user, user_i) * user_i_rating) / (SUM |sim(target_user, user_i)|)
        /// </summary>
        private float GetWeightedRating(float[] similaritiesVector, float[] ratingsVector)
        {
            float numerator = 0;
            float denominator = 0;
            for (int i = 0; i < Math.Min(similaritiesVector.Length, ratingsVector.Length); i++)
            {
                numerator += similaritiesVector[i] * ratingsVector[i];
                denominator += Math.Abs(similaritiesVector[i]);
            }

            return numerator / denominator;
        }

        /// <summary>
        /// Since similarities matrix is a symmetric one and was calculated efficiently we must extract
        /// the real values of the user's similarity vector differently than just taking its row.
        /// </summary>
        private float[] GetSimilarityVector(int targetUser, float[][] similarities)
        {
            float[] similaritiesVector = new float[similarities.GetLongLength(0)];

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
            float[] ratingsVector = new float[userItemMatrix.GetLongLength(0)];

            for (int i = 0; i < ratingsVector.LongLength; i++)
            {
                if (targetItem >= userItemMatrix[i].LongLength) { continue; }
                ratingsVector[i] = userItemMatrix[i][targetItem];
            }

            return ratingsVector;
        }
    }


    /// <summary>
    /// Making an average rating prediction based on similarity between users.
    /// </summary>
    class ItemSimilarityAverageRatingsPredictor : IPredictor
    {
        public void Predict(float[][] userItemMatrix, float[][] similarities)
        {
            for (int userIndex = 0; userIndex < userItemMatrix.GetLongLength(0); userIndex++)
            {
                float[] ratingsVector = GetRatingsVector(userIndex, userItemMatrix);

                for (int itemIndex = 0; itemIndex < userItemMatrix.GetLongLength(0); itemIndex++)
                {
                    float[] similaritiesVector = GetSimilarityVector(itemIndex, similarities);

                    if (userItemMatrix[userIndex][itemIndex] != 0) { continue; }

                    userItemMatrix[userIndex][itemIndex] = GetWeightedRating(similaritiesVector, ratingsVector);
                }
            }
        }

        /// <summary>
        /// Predicts the user's rating of unrated item.
        /// predicted_rating = (SUM sim(target_item, item_i) * user_i_rating) / (SUM |sim(target_item, item_i)|)
        /// </summary>
        private float GetWeightedRating(float[] similaritiesVector, float[] ratingsVector)
        {
            float numerator = 0;
            float denominator = 0;
            for (int i = 0; i < similaritiesVector.Length; i++)
            {
                numerator += similaritiesVector[i] * ratingsVector[i];
                denominator += Math.Abs(similaritiesVector[i]);
            }

            return numerator / denominator;
        }

        /// <summary>
        /// Since similarities matrix is a symmetric one and was calculated efficiently we must extract
        /// the real values of the item's similarity vector differently than just taking its row.
        /// </summary>
        private float[] GetSimilarityVector(int targetItem, float[][] similarities)
        {
            float[] similaritiesVector = new float[similarities.GetLongLength(0)];

            for (int j = 0; j < targetItem; j++)
            {
                similaritiesVector[j] = similarities[targetItem][j];
            }

            for (int i = targetItem + 1; i < similaritiesVector.Length; i++)
            {
                similaritiesVector[i] = similarities[i][targetItem];
            }

            return similaritiesVector;
        }

        /// <summary>
        /// Extract the vector of user ratings of all items.
        /// </summary>
        private float[] GetRatingsVector(int targetUser, float[][] userItemMatrix)
        {
            float[] ratingsVector = new float[userItemMatrix[targetUser].LongLength];

            for (int i = 0; i < ratingsVector.LongLength; i++)
            {
                ratingsVector[i] = userItemMatrix[targetUser][i];
            }

            return ratingsVector;
        }
    }
}
