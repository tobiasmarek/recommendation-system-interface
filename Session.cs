using Microsoft.Data.Analysis;
using RecommendationSystem.Interfaces;
using System.Text.Json;

namespace RecommendationSystem
{
    abstract class Session
    {
        public Viewer Viewer { get; set; }
        public Controller Controller { get; set; }

        private DataFrame dataFrame { get; set; } // nebo jen prostě class která implementuje víc interfaces (do ktery spada DataFrame)
        private Approach approach { get; set; }

        public void GetRecommendations()
        {
            if (approach is null) { return; }

            approach.Recommend();

            var subsetDF = new DataFrame();
            subsetDF.Columns.Add(dataFrame["NAME"]);
            subsetDF.Columns.Add(dataFrame["TfIdf"]);
            Viewer.View(subsetDF.Head(5));
        }

        public void SelectApproach(string name = "TfIdf")
        {
            var preProcessor = new TfIdf();
            var evaluator = new CosineSimilarityEvaluator();

            approach = new NonModelContentBasedApproach
            {
                Name = name,
                dataFrame = dataFrame,
                PreProcessor = preProcessor,
                Evaluator = evaluator,
                User = new SisUser() // neměl bych ho dát do Framework?
            };
        }

        public void LoadCsv(string csvFilePath = "subjects_11310.csv", char separator = '|')
        {
            try { dataFrame = DataFrame.LoadCsv(csvFilePath, separator); }
            catch (Exception e) { Console.WriteLine("Failed to LoadCsv"); return; }

            Viewer.View(dataFrame.Head(5));
        }

        public void SaveCsv(string csvResultFilePath = "subjects_11310_result.csv", char separator = '|')
        {
            try { DataFrame.SaveCsv(dataFrame, csvResultFilePath, separator); }
            catch (Exception e) { Console.WriteLine("Failed to SaveCsv"); }
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
                Console.WriteLine("Loading session failed");
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




    class ConsoleSession : Session
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




    class ConsoleSubjectSession : ConsoleSession
    {
        public void AddFavourite() // tady spis ne, nebo ne?
        {

        }
    } 
}