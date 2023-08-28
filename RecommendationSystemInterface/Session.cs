using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using RecommendationSystemInterface.Interfaces;
using System.Text.Json;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata;

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

        private IDisposableLineReader? RecordReader { get; set; }
        private Approach? Approach { get; set; }

        private Type? ApproachType { get; set; } 
        private ParameterInfo[]? ApproachParams { get; set; }

        private string DataPath { get; set; }
        private User? User { get; set; }

        protected Session(Viewer viewer)
        {
            _viewer = viewer;
        }


        public void GetRecommendations()
        {
            if (Approach is null) {_viewer.ViewString("Approach is null!"); return;}
            if (User is null) {_viewer.ViewString("User is null!"); return;}

            Approach.User = User;

            _viewer.ViewFile(Approach.Recommend());
        }

        public void SelectApproach(string[] parameters)
        {
            if (ApproachType == null || ApproachParams == null) { _viewer.ViewString("Fill in all information!"); return; }

            object[] constructorParameters = new object[parameters.Length];

            if (ApproachParams.Length != parameters.Length) { _viewer.ViewString("Insufficient number of parameters."); return; }

            Type? readerType = GetClassType(parameters[0]);
            if (readerType is null) { _viewer.ViewString("Loading reader has failed"); return; }

            constructorParameters[0] = Activator.CreateInstance(readerType, new object[] { DataPath });

            for (int i = 1; i < parameters.Length; i++)
            {
                Type? parameterType = GetClassType(parameters[i]);
                if (parameterType is null) { _viewer.ViewString("Loading parameter type has failed"); return; }

                constructorParameters[i] = Activator.CreateInstance(parameterType);
            }

            var resultingApproach = (Approach?)Activator.CreateInstance(ApproachType, constructorParameters);

            if (resultingApproach is null) { _viewer.ViewString("Creating approach failed!"); return; }

            Approach = resultingApproach;
        }

        public Type? GetClassType(string className)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type foundType in assembly.GetTypes())
                {
                    if (foundType.Name == className)
                    {
                        return foundType;
                    }
                }
            }

            return null;
        }

        public string[] GetAvailableClassesOfAType(string className)
        {
            Type? classType = GetClassType(className);
            if (classType is null) { _viewer.ViewString("Class type not found!"); return new []{""}; }

            var wantedAssembly = Assembly.GetAssembly(classType); // co kdyz to neni v tomhle assembly
            if (wantedAssembly is null) return new[] { "" };

            var names = wantedAssembly
                .GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(classType))
                .Select(type => type.FullName) // type.Name jenom abych nemusel [^1]
                .ToArray();

            if (names.Contains(null)) return new[] { "" };

            string[] resultingNames = new string[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                resultingNames[i] = (names[i]!.Split('.'))[^1];
            }

            return resultingNames;
        }

        public string[] GetClassesImplementing(string interfaceName) // udělat obecnější - ne jen interfaces
        {
            List<string> classNames = new List<string>();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type foundType in assembly.GetTypes())
                {
                    if (foundType.IsClass && !foundType.IsAbstract && foundType.GetInterface(interfaceName) != null)
                    {
                        classNames.Add(foundType.Name);
                    }
                }
            }

            return classNames.ToArray();
        }

        public string[] GetConstructorParameterTypes(string className)
        {
            Type? classType = GetClassType(className);
            if (classType is null) { _viewer.ViewString("Getting ctor parameters has failed!"); return new[] { "" }; }

            ApproachParams = (classType.GetConstructors())[0].GetParameters(); // Get the first ctor params
            ApproachType = classType;

            string[] parameterTypeNames = new string[ApproachParams.Length];

            for (int i = 0; i < ApproachParams.Length; i++)
            {
                parameterTypeNames[i] = ApproachParams[i].ParameterType.ToString();
            }

            return parameterTypeNames;
        }

        public void CreateUser(string className, string parameters)
        {
            Type? classType = GetClassType(className);
            if (classType is null) { _viewer.ViewString("User type not found!"); return; }
            
            User = (User)Activator.CreateInstance(classType, new object[] { parameters });
        }

        public void LoadFromCsv(string filename)
        {
            if (File.Exists(filename))
            {
                DataPath = filename;
            }
            else
            {
                DataPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "Data", filename));
            }

            try
            {
                IDisposableLineReader rr = new FileStreamLineReader(DataPath);
            }
            catch (Exception e)
            {
                _viewer.ViewString($"Failed to load csv file.\n\n{e}");
                return;
            }

            _viewer.ViewFile(DataPath);
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