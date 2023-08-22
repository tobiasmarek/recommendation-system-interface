using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystemInterface.Interfaces
{
    interface ISimilarityEvaluator
    {
        float EvaluateSimilarity(float[] u, float[] v); // nebo z toho udělat <T>, protože třeba NN může dostat jinej vstup
        // protože NN může mít svoje převádění na vektorovou reprezentaci, nebo ne?
        // tzn dám si pozor jestli vůbec chci dělat jinej interface pro převádění reprezentace do vektorový
        // protože ještě k tomu - similarity nemusim dělat jen z float[] reprezentace, ale třeba z množin
        // kdybych předělával měl bych Similarity předávat něco, co convertuje na vektorovou reprezentaci (IVectorConverter)
    }


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

            return 1 / (float)Math.Sqrt(sum);
        }
    }


    class CosineSimilarityEvaluator : ISimilarityEvaluator
    {
        private int k = 0;

        public float EvaluateSimilarity(float[] u, float[] v)
        {
            if (u.Length != v.Length) { return -1; }

            float numerator = 0;

            for (int i = 0; i < u.Length; i++)
            {
                numerator += u[i] * v[i];
            }

            float square_sum_u = 0;

            for (int i = 0; i < u.Length; i++)
            {
                square_sum_u += u[i] * u[i];
            }

            float square_sum_v = 0;

            for (int i = 0; i < v.Length; i++)
            {
                square_sum_v += v[i] * v[i];
            }

            float denominator = square_sum_u * square_sum_v;

            if (denominator == 0) { return -1; }

            return (float)(numerator / Math.Sqrt(denominator));
        }
    }
}
