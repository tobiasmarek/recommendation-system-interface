﻿using RecommendationSystemInterface.Interfaces;
using System;

namespace RecommendationSystemInterface
{
    /// <summary>
    /// Defining class that contains the commonalities between all recommendation approaches.
    /// The defining functionality is Recommend function that lets us implement different techniques.
    /// </summary>
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




    /// <summary>
    /// Defining characteristics of Content-Based approaches.
    /// </summary>
    abstract class ContentBasedApproach : Approach
    {
        public User User { get; set; }
    }


    /// <summary>
    /// Content-Based approach centered around string input and the calculation of similarity between strings.
    /// The results are saved in a file.
    /// </summary>
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

            string resultsFilePath = PostProcessor.Postprocess(new float[][] { similaritiesVector }); // JINAK

            return resultsFilePath;
        }

        /// <summary>
        /// Takes subjects rated by user and averages them out into a single vector that should
        /// theoretically revolve around its preferences in the vector space.
        /// </summary>
        private float[] GetUserVector(float[][] dataMatrix)
        {
            float[] userVector = new float[dataMatrix[0].Length];

            if (User is not SisUser sisUser) { return userVector; } // TOHLE JE HODNĚ SPECIFIC - PŘESUNOU JINAM NEŽ U APPROACH

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




    /// <summary>
    /// Defining characteristics of Collaborative-Filtering approaches.
    /// </summary>
    abstract class CollaborativeFilteringApproach : Approach
    {
        public User[] Users { get; set; }
    }


    /// <summary>
    /// Calculates similarities between *users* and makes predictions of missing item ratings in userItemMatrix according to it.
    /// The results are then saved in a file.
    /// </summary>
    class UserUserCfApproach : CollaborativeFilteringApproach
    {
        public IPredictor Predictor { get; set; }

        public override string Recommend()
        {
            float[][] userItemMatrix = PreProcessor.Preprocess(RecordReader); // The creation of userItemMatrix

            float[][] userSimilarities = new float[userItemMatrix.LongLength][]; // Symmetric matrix of user similarities

            for (int i = 0; i < userSimilarities.GetLength(0); i++)
            {
                userSimilarities[i] = new float[i];

                for (int j = 0; j < i; j++)
                {
                    if (i == j) { continue; }
                    userSimilarities[i][j] = Evaluator.EvaluateSimilarity(userItemMatrix[i], userItemMatrix[j]);
                }
            }

            Predictor.Predict(userItemMatrix, userSimilarities); // Predicts missing values of userItemMatrix
            // TADY UČENÍ MODELU A PREDICTIONS TZN TAKY EVALUATION MEASURE FOR INFORMATION RETRIEVAL

            string resultsFilePath = PostProcessor.Postprocess(userItemMatrix); // USER-HISTORY NEEDED (NEBO VĚDĚT JAKY RATINGS BYLY NA ZAČATKU) A POPISY ITEMŮ

            return resultsFilePath;
        }
    }


    /// <summary>
    /// Calculates similarities between *items* and makes predictions of missing item ratings in userItemMatrix according to it.
    /// The results are then saved in a file.
    /// </summary>
    class ItemItemCfApproach : CollaborativeFilteringApproach
    {
        public IPredictor Predictor { get; set; }

        public override string Recommend()
        {
            throw new NotImplementedException();
        }
    }




    /// <summary>
    /// Defining characteristics of Hybrid approaches.
    /// </summary>
    abstract class HybridApproach : Approach
    {
        public User[] Users { get; set; }
    }
}
