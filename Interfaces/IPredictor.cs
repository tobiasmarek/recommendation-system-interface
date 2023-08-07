using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;

namespace RecommendationSystem.Interfaces
{
    internal interface IPredictor
    {
        void Predict(float[][] userItemMatrix, float[][] similarities);
    }




    class SimilarityAverageRankingsPredictor : IPredictor
    {
        public void Predict(float[][] userItemMatrix, float[][] similarities)
        {
            for (int userIndex = 0; userIndex < userItemMatrix.GetLength(0); userIndex++)
            {
                for (int itemIndex = 0; itemIndex < userItemMatrix.GetLength(0); itemIndex++)
                {
                    if (userItemMatrix[userIndex][itemIndex] != 0) { continue ;}

                    float[] similaritiesVector = GetSimilarityVector(userIndex, similarities);
                    float[] rankingsVector = GetRankingsVector(itemIndex, userItemMatrix);

                    userItemMatrix[userIndex][itemIndex] = GetWeightedRanking(similaritiesVector, rankingsVector);
                }
            }
        }

        private float GetWeightedRanking(float[] similaritiesVector, float[] rankingsVector)
        {
            float numerator = 0;
            float denominator = 0;
            for (int i = 0; i < similaritiesVector.Length; i++)
            {
                numerator += similaritiesVector[i] * rankingsVector[i];
                denominator += similaritiesVector[i];
            }

            return numerator / denominator;
        }

        private float[] GetSimilarityVector(int targetUser, float[][] similarities)
        {
            float[] similaritiesVector = new float[similarities.GetLength(0)];

            for (int i = 0; i < targetUser; i++)
            {
                similaritiesVector[i] = similarities[targetUser][i];
            }

            for (int i = targetUser + 1; i < similaritiesVector.Length; i++)
            {
                similaritiesVector[i] = similarities[i][targetUser];
            }

            return similaritiesVector;
        }

        private float[] GetRankingsVector(int targetItem, float[][] userItemMatrix)
        {
            float[] rankingsVector = new float[userItemMatrix.GetLength(0)];

            for (int i = 0; i < rankingsVector.Length; i++)
            {
                rankingsVector[i] = userItemMatrix[i][targetItem]; // snad to nebudou null hodnoty
            }

            return rankingsVector;
        }
    }
}
