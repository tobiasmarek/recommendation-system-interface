using System;
using ConsoleRecSys.Interfaces;
using RecommendationSystemInterface;

namespace ConsoleRecSys
{
    public class ConsoleSession : Session
    {
        public IConsoleUserUtil? UserUtil { get; set; }
        public string[]? ApproachParameters { get; set; }
        
        private const string UserNotSelected = "User-type not selected";


        public ConsoleSession(Viewer viewer) : base(viewer)
        {
            
        }

        public void Recommend()
        {
            if (UserUtil is null) { Viewer.ViewString(UserNotSelected); return; }
            UserUtil.InitializeUser();
            User = (User)UserUtil;

            Viewer.ViewString($"{Environment.NewLine}This might take a while. (max 3 minutes){Environment.NewLine}");

            base.GetRecommendations();
        }

        public bool DataSourceKnown()
        {
            return !(string.IsNullOrEmpty(DataPath));
        }

        public void AddItemToUser(string where, string what)
        {
            if (UserUtil is null) { Viewer.ViewString(UserNotSelected); return; }

            if (!int.TryParse(where, out int index) || index < 0) { Viewer.ViewString("Wrong indexing"); return; }

            if (UserUtil.TryAdd(index, what))
            {
                Viewer.ViewString($"{what} successfully added.");
            }
            else
            {
                Viewer.ViewString("Something went wrong when adding!");
            }
        }

        public void ClearUser()
        {
            if (UserUtil is null) { Viewer.ViewString(UserNotSelected); return; }

            UserUtil.Clear();

            Viewer.ViewString("User variables successfully cleared!");
        }

        public void ShowUser()
        {
            if (UserUtil is null) { Viewer.ViewString(UserNotSelected); return; }

            Viewer.ViewString(UserUtil.Show());
        }

        public void UserDemo()
        {
            if (UserUtil is null) { Viewer.ViewString(UserNotSelected); return; }

            UserUtil.Demo();
            Viewer.ViewString("Demo successfully selected - here's how the user looks now:");
            Viewer.ViewString(UserUtil.Show());
        }

        public void ShowSummary()
        {
            Viewer.ViewString("");
            Viewer.ViewString("-------------------- SUMMARY --------------------");

            Viewer.ViewString("* DATA *");
            if (DataSourceKnown())
            {
                Viewer.ViewString($"The source of your data: {DataPath}");
            }
            else
            {
                Viewer.ViewString("Source unknown - not selected");
            }

            Viewer.ViewString("* USER *");
            ShowUser();

            Viewer.ViewString("* APPROACH *");
            if (Approach is null || ApproachParameters is null)
            {
                Viewer.ViewString("Approach unknown - not selected");
            }
            else
            {
                foreach (var approachParameter in ApproachParameters)
                {
                    Viewer.ViewString(approachParameter);
                }
            }

            Viewer.ViewString("-------------------------------------------------");
            Viewer.ViewString("");
        }
    }
}
