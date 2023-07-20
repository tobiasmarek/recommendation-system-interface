using Microsoft.Data.Analysis;
using RecommendationSystem.Interfaces;

namespace RecommendationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // var parser = new SisSubjectParser { Url = sis.cuni.uk };
            string csvFilePath = "path"; // parser.Parse();
            
            DataFrame dataFrame = DataFrame.LoadCsv(csvFilePath); // v try catch?

            var approach = new ContentBasedApproach()
            {
                Evaluator = new CosineSimilarityEvaluator(),
                Vectorizer = new UserAndRowVectorizer(),
                User = new SisUser(),
            };

            var viewer = new ConsoleViewer();

            var fw = new Framework
            {
                Data = dataFrame,
                Approach = approach,
                Viewer = viewer
            };
        }
    }
}