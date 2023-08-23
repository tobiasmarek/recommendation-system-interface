using System;
using System.IO;
using RecommendationSystemInterface.Interfaces;
using System.Text.Json;

namespace RecommendationSystemInterface
{
    public abstract class Session
    {
        public Viewer Viewer { get; set; }
        public Controller Controller { get; set; }

        private IDisposableLineReader recordReader { get; set; } // nebo něco víc specific?
        private Approach approach { get; set; }

        public void GetRecommendations()
        {
            if (approach is not null) { Viewer.View(approach.Recommend()); }
        }

        public void SelectApproach(string name = "CFilter")
        {
            var preProcessor = new UserItemMatrixPreProcessor(); //TfIdf();
            var evaluator = new CosineSimilarityEvaluator();
            var predictor = new SimilarityAverageRatingsPredictor();
            var postProcessor = new UserItemMatrixPostProcessor(); //SimilarityVectorPostProcessor();

            approach = new UserUserCfApproach() //StringSimilarityContentBasedApproach
            {
                Name = name,
                RecordReader = recordReader,
                PreProcessor = preProcessor,
                Evaluator = evaluator,
                Predictor = predictor,
                PostProcessor = postProcessor,
                // User = new SisUser() // neměl bych ho dát do Session?
            };
        }

        public void LoadFromCsv(string filePath = "u.data")//"subjects_11310.csv", char separator = '|')
        {
            IDisposableLineReader rr;

            try
            {
                rr = new FileStreamLineReader(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to LoadCsv", e);
                return;
            }

            recordReader = rr;

            Viewer.View(filePath);
        }

        public void LoadFromDbs()
        {
            throw new NotImplementedException();
        }

        public void ShowSessions() // nebo nejak proste jenom view bude handlovat? bez tvorby takovejch funkci tim myslim
        {
            string[] metadataFiles = Directory.GetFiles(".", "*_metadata");

            FileStreamLineReader sr;
            foreach (var file in metadataFiles)
            {
                sr = new FileStreamLineReader(file);

                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    //Viewer.View(line);
                    Console.WriteLine(line);
                }

                sr.Dispose(); // lip nez porad disposovat
            }
        }

        public void DeleteSession(string filename)
        {
            File.Delete(filename);
            File.Delete($"{filename}_metadata");
        }

        public Session? LoadSession(string filename)
        {
            Session? loadedSession = null;

            try
            {
                string jsonString = File.ReadAllText(filename); // tohle je abominace
                loadedSession = JsonSerializer.Deserialize<Session>(jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine("Loading session failed", e);
            }

            return loadedSession;
        }

        public void SaveSession(string filename)
        {
            try
            {
                using (var fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write)) // udelat binary?
                {
                    JsonSerializer.SerializeAsync(fileStream, this, new JsonSerializerOptions { WriteIndented = false });
                }

                using (StreamWriter sw = new StreamWriter($"{filename}_metadata")) // svuj
                {
                    sw.WriteLine($"Name: {filename}, Saved at: {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Failed to SaveSession");
            }
        }

        public void Parse()
        {
            // var parser = new SisSubjectParser { Url = sis.cuni.uk };
            // parser.Parse();
        }
    }




    public class ConsoleSession : Session
    {
        public ConsoleSession()
        {
            Controller = new ConsoleController { Session = this };
            Viewer = new ConsoleViewer();

            Controller.TakeInput();
        }
    }


    class WebSession : Session
    {
        public WebSession()
        {
            Controller = new WebController { Session = this };
            Viewer = new WebViewer();

            Controller.TakeInput();
        }
    }


    class WinFormsSession : Session
    {
        public WinFormsSession()
        {
            Controller = new WinFormsController() { Session = this };
            Viewer = new WinFormsViewer();

            Controller.TakeInput();
        }
    }
}