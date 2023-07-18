using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    abstract class Approach
    {
        public abstract void Recommend();
    }




    class ContentBasedApproach : Approach // nebylo by dobrý to udělat abstract?
    {
        public ISimilarity Similarity { get; set; }
        public User User { get; set; }

        public override void Recommend()
        {
            throw new NotImplementedException();
        }
    }




    abstract class CollaborativeFilteringApproach : Approach
    {
        public User[] Users { get; set; }
    }


    abstract class HybridApproach : Approach
    {

    }
}
