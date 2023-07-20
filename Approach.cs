using Microsoft.Data.Analysis;
using RecommendationSystem.Interfaces;

namespace RecommendationSystem
{
    abstract class Approach
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DataFrame dataFrame { get; set; }

        public ISimilarityEvaluator Evaluator { get; set; }

        public abstract void Recommend();
    }




    class ContentBasedApproach : Approach
    {
        public User User { get; set; }

        public IUserAndRowVectorizer Vectorizer { get; set; }

        public override void Recommend()
        {
            var newColumn = new PrimitiveDataFrameColumn<float>($"{Name}_results.txt", dataFrame.Rows.Count);

            float[] userVector = Vectorizer.VectorizeUser(User, dataFrame);

            long rowNum = 0;
            foreach (var row in dataFrame.Rows)
            {
                float[] rowVector = Vectorizer.VectorizeRow();

                float similarity = Evaluator.EvaluateSimilarity(rowVector, userVector);

                newColumn[rowNum] = similarity;

                rowNum++;
            }

            dataFrame.Columns.Add(newColumn);
        }
    }




    abstract class CollaborativeFilteringApproach : Approach
    {
        public User[] Users { get; set; }
    }




    abstract class HybridApproach : Approach
    {
        public User[] Users { get; set; }
    }
}
