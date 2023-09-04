using System;

namespace RecommendationSystemInterface.Interfaces
{
    /// <summary>
    /// Takes two vectors and evaluates its similarity.
    /// </summary>
    public interface ISimilarityEvaluator
    {
        float EvaluateSimilarity(float[] u, float[] v);
    }


    /// <summary>
    /// One divided by the euclidean distance between the two vector.
    /// If the result is higher than 1, returns 1 (the highest possible similarity)
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

            float distance = (float)Math.Sqrt(sum);

            if (distance < 1)
            {
                return 1;
            }

            return 1 / distance;
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
