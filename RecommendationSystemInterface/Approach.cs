﻿using RecommendationSystemInterface.Interfaces;
using System;
using System.Collections.Generic;

namespace RecommendationSystemInterface
{
    /// <summary>
    /// Defining class that contains the commonalities between all recommendation approaches.
    /// The defining functionality is Recommend function that lets us implement different techniques.
    /// </summary>
    public abstract class Approach
    {
        public User? User { get; set; }

        public IDisposableLineReader RecordReader { get; set; }
        public IPreProcessor PreProcessor { get; set; }
        public ISimilarityEvaluator Evaluator { get; set; }
        public IPostProcessor PostProcessor { get; set; }

        protected Approach(IDisposableLineReader recordReader, IPreProcessor preProcessor, ISimilarityEvaluator evaluator, IPostProcessor postProcessor)
        {
            RecordReader = recordReader;
            PreProcessor = preProcessor;
            Evaluator = evaluator;
            PostProcessor = postProcessor;
        }

        public abstract string Recommend();
    }




    /// <summary>
    /// Defining characteristics of Content-Based approaches.
    /// </summary>
    public abstract class ContentBasedApproach : Approach
    {
        protected ContentBasedApproach(IDisposableLineReader recordReader, IPreProcessor preProcessor, ISimilarityEvaluator evaluator, IPostProcessor postProcessor) : base(recordReader, preProcessor, evaluator, postProcessor) { }
    }


    /// <summary>
    /// Content-Based approach centered around string input and the calculation of similarity between strings.
    /// The results are saved in a file.
    /// </summary>
    class StringSimilarityContentBasedApproach : ContentBasedApproach
    {
        public StringSimilarityContentBasedApproach(IDisposableLineReader recordReader, IPreProcessor preProcessor, ISimilarityEvaluator evaluator, IPostProcessor postProcessor) : base(recordReader, preProcessor, evaluator, postProcessor) { }

        public override string Recommend()
        {
            float[][] dataMatrix = PreProcessor.Preprocess(RecordReader);

            float[] userVector = User.UserVectorizer.VectorizeUser(User, dataMatrix);

            float[] similaritiesVector = new float[dataMatrix.GetLongLength(0)];

            for (long rowNum = 0; rowNum < similaritiesVector.LongLength; rowNum++)
            {
                similaritiesVector[rowNum] = Evaluator.EvaluateSimilarity(dataMatrix[rowNum], userVector);
            }

            string resultsFilePath = PostProcessor.Postprocess(new float[][] { similaritiesVector }); // JINAK

            return resultsFilePath;
        }
    }




    /// <summary>
    /// Defining characteristics of Collaborative-Filtering approaches.
    /// </summary>
    public abstract class CollaborativeFilteringApproach : Approach
    {
        protected CollaborativeFilteringApproach(IDisposableLineReader recordReader, IPreProcessor preProcessor, ISimilarityEvaluator evaluator, IPostProcessor postProcessor) : base(recordReader, preProcessor, evaluator, postProcessor) { }
    }


    /// <summary>
    /// Calculates similarities between *users* and makes predictions of missing item ratings in userItemMatrix according to it.
    /// The results are then saved in a file.
    /// </summary>
    class UserUserCfApproach : CollaborativeFilteringApproach
    {
        public IPredictor Predictor { get; set; }

        public UserUserCfApproach(IDisposableLineReader recordReader, IPreProcessor preProcessor, ISimilarityEvaluator evaluator, IPostProcessor postProcessor, IPredictor predictor) : base(recordReader, preProcessor, evaluator, postProcessor)
        {
            Predictor = predictor;
        }

        public override string Recommend()
        {
            float[][] userItemMatrix = PreProcessor.Preprocess(RecordReader); // The creation of userItemMatrix

            float[] userVector = User.UserVectorizer.VectorizeUser(User, userItemMatrix);
            userItemMatrix[0] = userVector; // Change the user with index 0 to our userVector

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

        public ItemItemCfApproach(IDisposableLineReader recordReader, IPreProcessor preProcessor, ISimilarityEvaluator evaluator, IPostProcessor postProcessor, IPredictor predictor) : base(recordReader, preProcessor, evaluator, postProcessor)
        {
            Predictor = predictor;
        }

        public override string Recommend()
        {
            throw new NotImplementedException();
        }
    }




    /// <summary>
    /// Defining characteristics of Hybrid approaches.
    /// </summary>
    public abstract class HybridApproach : Approach
    {
        public User[]? Users { get; set; }

        protected HybridApproach(IDisposableLineReader recordReader, IPreProcessor preProcessor, ISimilarityEvaluator evaluator, IPostProcessor postProcessor) : base(recordReader, preProcessor, evaluator, postProcessor) { }
    }
}
