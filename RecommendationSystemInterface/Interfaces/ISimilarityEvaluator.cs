using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecommendationSystemInterface.Interfaces
{
    /// <summary>
    /// Takes two vectors and evaluates its similarity.
    /// </summary>
    interface ISimilarityEvaluator
    {
        float EvaluateSimilarity(float[] u, float[] v); // nebo z toho udělat <T>, protože třeba NN může dostat jinej vstup
        // protože NN může mít svoje převádění na vektorovou reprezentaci, nebo ne?
        // tzn dám si pozor jestli vůbec chci dělat jinej interface pro převádění reprezentace do vektorový
        // protože ještě k tomu - similarity nemusim dělat jen z float[] reprezentace, ale třeba z množin
        // kdybych předělával měl bych Similarity předávat něco, co convertuje na vektorovou reprezentaci (IVectorConverter)
    }


    /// <summary>
    /// One divided by the euclidean distance between the two vector.
    /// </summary>
    class EuclideanSimilarityEvaluator : ISimilarityEvaluator
    {
        public float EvaluateSimilarity(float[] u, float[] v)
        {
            if (u.Length != v.Length) { return -1; }

            float sum = 0;

            for (int i = 0; i < u.Length; i++)
            {
                sum += (u[i] - v[i]) * (u[i] - v[i]);
            }

            return 1 / (float)Math.Sqrt(sum); // MĚL BYCH UDĚLAT UPPER A LOWER LIMIT
        }
    }


    /// <summary>
    /// Cosine of the angle between the two vectors.
    /// </summary>
    class CosineSimilarityEvaluator : ISimilarityEvaluator
    {
        public float EvaluateSimilarity(float[] u, float[] v)
        {
            if (u.Length != v.Length) { return -1; }

            float numerator = 0;

            for (int i = 0; i < u.Length; i++)
            {
                numerator += u[i] * v[i];
            }

            float squareSumU = 0;
            for (int i = 0; i < u.Length; i++)
            {
                squareSumU += u[i] * u[i];
            }

            float squareSumV = 0;
            for (int i = 0; i < v.Length; i++)
            {
                squareSumV += v[i] * v[i];
            }

            float denominator = squareSumU * squareSumV;

            if (denominator == 0) { return -1; }

            return (float)(numerator / Math.Sqrt(denominator));
        }
    }
}
