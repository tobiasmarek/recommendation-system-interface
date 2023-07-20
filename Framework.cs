using Microsoft.Data.Analysis;

namespace RecommendationSystem
{
    internal class Framework // udělat abstract a rozšířit? Session
    {
        public DataFrame Data { get; set; } // nebo jen prostě class která implementuje víc interfaces (do ktery spada DataFrame)
        public Approach Approach { get; set; }
        public Viewer Viewer { get; set; }

        public void GetRecommendations()
        {
            Approach.Recommend();
            // Viewer.View();
        }
    }
}