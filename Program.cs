using Microsoft.Data.Analysis;

namespace RecommendationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // var parser = new SisSubjectParser { Url = sis.cuni.uk };
            string csvFilePath = "path"; // parser.Parse();
            
            DataFrame df = DataFrame.LoadCsv(csvFilePath);

            var approach = new ContentBasedApproach 
            {
                Similarity = new CosineSimilarity(),
                User = new SisUser()
            };

            var viewer = new ConsoleViewer();

            var fw = new Framework
            {
                Data = df,
                Approach = approach,
                Viewer = viewer
            };
        }
    }
}