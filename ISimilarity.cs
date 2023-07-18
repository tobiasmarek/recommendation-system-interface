using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    interface ISimilarity
    {
        public float Similarity(float[] u, float[] v); // nebo z toho udělat <T>, protože třeba NN může dostat jinej vstup
        // protože NN může mít svoje převádění na vektorovou reprezentaci, nebo ne?
        // tzn dám si pozor jestli vůbec chci dělat jinej interface pro převádění reprezentace do vektorový
        // protože ještě k tomu - similarity nemusim dělat jen z float[] reprezentace, ale třeba z množin
        // kdybych předělával měl bych Similarity předávat něco, co convertuje na vektorovou reprezentaci (IVectorConverter)
    }


    class EuclideanSimilarity : ISimilarity
    {
        public float Similarity(float[] u, float[] v)
        {
            if (u.Length != v.Length) { return -1; }

            float sum = 0;
            
            for (int i = 0; i < u.Length; i++)
            {
                sum += (u[i] - v[i]) * (u[i] - v[i]);
            }

            return 1/(float)Math.Sqrt(sum);
        }
    }


    class CosineSimilarity : ISimilarity
    {
        public float Similarity(float[] u, float[] v)
        {
            if (u.Length != v.Length) { return -1; }

            float numerator = 0;

            for (int i = 0; i < u.Length; i++)
            {
                numerator += u[i] * v[i];
            }

            float denominator = 0;

            for (int i = 0; i < u.Length; i++)
            {
                denominator += u[i] * u[i] * v[i] * v[i];
            }

            if (denominator == 0) { return -1; }

            return (float)(numerator / Math.Sqrt(denominator));
        }
    }
}
