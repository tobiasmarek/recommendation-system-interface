using System;
using System.IO;
using RecommendationSystemInterface.Interfaces;
using System.Text.Json;

namespace RecommendationSystemInterface
{
    /// <summary>
    /// Implements basic concepts that a Session should have.
    /// Basically, the model and the glue of the program.
    /// Is controlled by Controller, commands Viewer.
    /// Lets us choose and define the basic characteristics of the Approach and save it for later.
    /// </summary>
    public class Session
    {
        private readonly Viewer _viewer;

        private IDisposableLineReader? RecordReader { get; set; } // nebo něco víc specific?
        private Approach? Approach { get; set; }

        protected Session(Viewer viewer)
        {
            _viewer = viewer;
        }


        public void GetRecommendations()
        {
            if (Approach is not null) { _viewer.ViewFile(Approach.Recommend()); }
        }

        public void SelectApproach(string name = "CFilter")
        {
            if (RecordReader is null) {return;}

            var preProcessor = new UserItemMatrixPreProcessor(); //TfIdf();
            var evaluator = new CosineSimilarityEvaluator();
            var predictor = new SimilarityAverageRatingsPredictor();
            var postProcessor = new UserItemMatrixPostProcessor(); //SimilarityVectorPostProcessor();

            Approach = new UserUserCfApproach() //StringSimilarityContentBasedApproach
            {
                Name = name,
                RecordReader = RecordReader,
                PreProcessor = preProcessor,
                Evaluator = evaluator,
                Predictor = predictor,
                PostProcessor = postProcessor,
                // User = new SisUser() // neměl bych ho dát do Session?
            };
        }

        public void LoadFromCsv(string filename)
        {
            IDisposableLineReader rr;

            string dataPath;
            if (File.Exists(filename))
            {
                dataPath = filename;
            }
            else
            {
                dataPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "Data", filename));
            }

            try
            {
                rr = new FileStreamLineReader(dataPath);
            }
            catch (Exception e)
            {
                _viewer.ViewString($"Failed to load csv file.\n\n{e}");
                return;
            }

            RecordReader = rr;

            _viewer.ViewFile(dataPath);
        }

        public void LoadFromDbs()
        {
            throw new NotImplementedException();
        }

        public void ShowSessions()
        {
            string[] metadataFiles = Directory.GetFiles(".", "*_metadata");

            FileStreamLineReader sr;
            foreach (var file in metadataFiles)
            {
                sr = new FileStreamLineReader(file);

                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    _viewer.ViewString(line);
                }

                sr.Dispose();
            }
        }

        public void DeleteSession(string filename)
        {
            if (!File.Exists($"{filename}_metadata"))
            {
                Console.WriteLine("Nothing to be deleted.");
                return;
            }

            File.Delete(filename);
            File.Delete($"{filename}_metadata");
        }

        public void SaveSession(string filename)
        {
            try
            {
                using (var fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    JsonSerializer.SerializeAsync(fileStream, this, new JsonSerializerOptions { WriteIndented = false });
                }

                using (StreamWriter sw = new StreamWriter($"{filename}_metadata"))
                {
                    sw.WriteLine($"Name: {filename}, Saved at: {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                _viewer.ViewString($"Failed to SaveSession. {e}");
            }
        }

        public void LoadSession(string filename)
        {
            Session? loadedSession;

            try
            {
                string jsonString = File.ReadAllText(filename);
                loadedSession = JsonSerializer.Deserialize<Session>(jsonString);
            }
            catch (Exception e)
            {
                _viewer.ViewString($"Loading session failed. {e}");
                return;
            }

            if (loadedSession is null) {return;}

            Approach = loadedSession.Approach; // ZOBRAZIT CO JE VYBRANO
            RecordReader = loadedSession.RecordReader;

            _viewer.ViewString("Loaded successfully!");
        }

        public void Parse()
        {
            // var parser = new SisSubjectParser { Url = sis.cuni.uk };
            // parser.Parse();
        }
    }




    /// <summary>
    /// A Session that is controlled and shown in the Console.
    /// </summary>
    sealed class ConsoleSession : Session
    {
        public ConsoleSession(Viewer viewer) : base(viewer)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// A Session that is controlled and shown in the web.
    /// </summary>
    sealed class WebSession : Session
    {
        public WebSession(Viewer viewer) : base(viewer)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// A Session that is controlled and shown in WinForms application.
    /// </summary>
    sealed class WinFormsSession : Session
    {
        public WinFormsSession(Viewer viewer) : base(viewer)
        {
            throw new NotImplementedException();
        }
    }
}