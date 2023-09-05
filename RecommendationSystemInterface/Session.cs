using System;
using System.IO;
using System.Linq;
using System.Reflection;
using RecommendationSystemInterface.Interfaces;
using System.Text.Json;
using System.Collections.Generic;

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
        protected readonly Viewer Viewer;
        protected User? User { get; set; }
        protected Approach? Approach { get; set; }

        private Type? ApproachType { get; set; }
        private ParameterInfo[]? ApproachParams { get; set; } // Approach ctor parameters

        protected string? DataPath { get; set; }

        private IDisposableLineReader? RecordReader { get; set; } // The main reader of this program
        
        private IPageConvertor? Convertor { get; set; }
        protected object[]? ConvertorParamsWithoutResultFile { get; set; }


        protected Session(Viewer viewer)
        {
            Viewer = viewer;
        }


        /// <summary>
        /// The main function of the program.
        /// Checks if everything is set and calls Recommend on the selected Approach.
        /// Views the result file that Approach has made.
        /// If Convertor is present the results are converted in human readable form.
        /// </summary>
        public void GetRecommendations()
        {
            if (DataPath is null) { Viewer.ViewString("Source not selected!"); return; }
            if (Approach is null) { Viewer.ViewString("Approach is null!"); return; }
            if (User is null) { Viewer.ViewString("User is null!"); return; }

            Convertor = null;
            Approach.User = User;
            Approach.RecordReader.Reset();

            try
            {
                string resultFile = Approach.Recommend();
                Viewer.ViewFile(resultFile);
                SetUpFileConvertor(resultFile);
            }
            catch (LoggerException e)
            {
                Viewer.ViewString($"Error!{Environment.NewLine}{Environment.NewLine}{e}");
            }
            catch (Exception e)
            {
                Viewer.ViewString(
                    $"Something went wrong! Check your User and if it works with the logic of your Approach" +
                    $"{Environment.NewLine}{Environment.NewLine}{e}");
            }

            GetNextConvertedResultPage();
        }

        /// <summary>
        /// Creates an Approach instance when given constructor parameters.
        /// Approach type must be defined beforehand.
        /// The first constructor parameter must be a string of a type that implements IDisposableLineReader.
        /// </summary>
        public void SelectApproach(string[] parameters)
        {
            if (ApproachType == null || ApproachParams == null) { Viewer.ViewString("Fill in all information!"); return; }

            object[] constructorParameters = new object[parameters.Length];

            if (ApproachParams.Length != parameters.Length) { Viewer.ViewString("Insufficient number of parameters."); return; }
            if (DataPath is null) { Viewer.ViewString("Before selecting an approach, a source of content needs to be known. Load a csv file for example."); return; }

            var instance = GetInstance(parameters[0], new object[] { DataPath });
            if (instance is not IDisposableLineReader readerInstance) { Viewer.ViewString("Creating an instance of a reader has failed"); return; }
            
            constructorParameters[0] = readerInstance;

            for (int i = 1; i < parameters.Length; i++)
            {
                instance = GetInstance(parameters[i]);
                if (instance is null) { Viewer.ViewString($"Creating an instance of {parameters[i]} has failed"); return; }

                constructorParameters[i] = instance;
            }

            try
            {
                instance = Activator.CreateInstance(ApproachType, constructorParameters);
                if (instance is Approach approachInstance) { Approach = approachInstance; return; }
            }
            catch { }

            Viewer.ViewString("Creating approach failed!");
        }

        /// <summary>
        /// Calls a Convertor to get another page.
        /// </summary>
        public void GetNextConvertedResultPage()
        {
            if (Convertor is not null) { Viewer.ViewString(Convertor.ConvertPage()); }
        }

        /// <summary>
        /// Searches all Assemblies and gets a type that a class of a given name bears.
        /// </summary>
        public Type? GetClassType(string className)
        {
            className = className.Split('.')[^1];

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

        /// <summary>
        /// Fetches class names that are not abstract and are subclasses of a given class.
        /// </summary>
        public string[] GetAvailableClassesOfAType(string className)
        {
            Type? classType = GetClassType(className);
            if (classType is null) { Viewer.ViewString("Class type not found!"); return new []{""}; }

            var wantedAssembly = Assembly.GetAssembly(classType);
            if (wantedAssembly is null) return new[] { "" };

            var names = wantedAssembly
                .GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(classType))
                .Select(type => type.Name)
                .ToArray();

            return names;
        }

        public string[] GetClassesImplementingInterface(string interfaceName)
        {
            List<string> classNames = new();

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

        /// <summary>
        /// Gets all required Approach ctor parameter types in string format.
        /// Sets ApproachType and ApproachParams (ctor types).
        /// </summary>
        public string[] GetApproachCtorParameterTypes(string className)
        {
            Type? classType = GetClassType(className);
            if (classType is null) { Viewer.ViewString("Getting ctor parameters has failed!"); return new[] { "" }; }

            ApproachParams = (classType.GetConstructors())[0].GetParameters(); // Get the first ctor params
            ApproachType = classType;

            string[] parameterTypeNames = new string[ApproachParams.Length];

            for (int i = 0; i < ApproachParams.Length; i++)
            {
                parameterTypeNames[i] = ApproachParams[i].ParameterType.ToString();
            }

            return parameterTypeNames;
        }

        /// <summary>
        /// Takes a class name and its constructor parameter.
        /// Creates an instance of it and assigns it to User.
        /// </summary>
        public void CreateUser(string className, string parameters)
        {
            var instance = GetInstance(className, new object[] { parameters });

            if (instance is User userInstance) { User = userInstance; }
        }

        /// <summary>
        /// Tries to find a file by its path at first, if unsuccessful, tries to find a given file name in Data directory.
        /// The the path is then stored in DataPath variable.
        /// After that it tries create an instance of FileStreamLineReader with the found file, then buries it.
        /// </summary>
        public void LoadFromCsv(string filename)
        {
            if (File.Exists(filename)) { DataPath = filename; }
            else { DataPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "Data", filename)); }

            try { IDisposableLineReader rr = new FileStreamLineReader(DataPath); }
            catch (Exception e) { Viewer.ViewString($"Failed to load csv file.{Environment.NewLine}{e}"); return; }

            Viewer.ViewFile(DataPath);
        }

        public void LoadFromDbs()
        {
            Viewer.ViewString("Loading from a database is not implemented yet!");
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
                    Viewer.ViewString(line);
                }

                sr.Dispose();
            }
        }

        public void SaveSession(string filename)
        {
            try
            {
                using (var fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    JsonSerializer.SerializeAsync(fileStream, this, new JsonSerializerOptions { WriteIndented = false });
                }

                using (var sw = new StreamWriter($"{filename}_metadata"))
                {
                    sw.WriteLine($"Name: {filename}, Saved at: {DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                Viewer.ViewString($"Failed to SaveSession.{Environment.NewLine}{e}");
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
                Viewer.ViewString($"Loading session failed.{Environment.NewLine}{e}");
                return;
            }

            if (loadedSession is null) {return;}

            Approach = loadedSession.Approach;
            DataPath = loadedSession.DataPath;

            Viewer.ViewString("Loaded successfully!");
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

        /// <summary>
        /// This function represents extended Activator.CreateInstance() function.
        /// It implements a try catch statement so that the program does not crash.
        /// Saves a little space when creating instances all over the solution.
        /// </summary>
        protected object? GetInstance(string className, object[]? parameters = null)
        {
            Type? classType = GetClassType(className);
            if (classType is null) { Viewer.ViewString($"{className} type not found!"); return null; }

            object? instance = null;
            if (parameters is null) { parameters = Array.Empty<object>(); }

            try
            {
                instance = Activator.CreateInstance(classType, parameters);
            }
            catch (MissingMethodException e)
            {
                Viewer.ViewString($"User ctor with these parameters was not found!{Environment.NewLine}{e}");
            }
            catch (Exception e)
            {
                Viewer.ViewString($"Something went wrong!{Environment.NewLine}{e}");
            }

            return instance;
        }

        /// <summary>
        /// Tries to create an instance of FileConvertor
        /// </summary>
        private void SetUpFileConvertor(string resultFile)
        {
            if (ConvertorParamsWithoutResultFile is null) { return; }

            object[] ctorParameters = new object[ConvertorParamsWithoutResultFile.Length + 1];
            ctorParameters[0] = resultFile;

            for (int i = 0; i < ConvertorParamsWithoutResultFile.Length; i++) { ctorParameters[i+1] = ConvertorParamsWithoutResultFile[i]; }

            string resultTempFile = Path.GetTempFileName();
            File.Copy(resultFile, resultTempFile, true);

            try
            {
                var instance = Activator.CreateInstance(typeof(FileConvertor), ctorParameters);
                if (instance is FileConvertor fileConvertor) { Convertor = fileConvertor; }
            }
            catch
            {
                throw new LoggerException("Problem with creating the instance of Convertor!");
            }

            ConvertorParamsWithoutResultFile = null;
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
    /// A Session that is controlled and shown on the web.
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