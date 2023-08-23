using RecommendationSystemInterface;

namespace WinFormsRecSys
{
    public partial class Form1 : Form
    {
        private Session session { get; set; }

        public Form1()
        {
            InitializeComponent();
            this.session = new WinFormsSession(this.OutputLabel);
        }

        private void RecBtn_Click(object sender, System.EventArgs e)
        {
            session.Controller.TakeCommand(new string[] {"recommend"}); // nebo rovnou volat ty funkce
        }

        private void button1_Click(object sender, EventArgs e)
        {
            session.Controller.TakeCommand(new string[] {"loadfromcsv", "u.data" });

            session.Controller.TakeCommand(new string[] {"selectapproach"});
        }

        private void button2_Click(object sender, EventArgs e)
        {
            session.Controller.TakeCommand(new string[] { "loadfromcsv", "subjects_11310.csv" });

            session.Controller.TakeCommand(new string[] { "selectapproach" });
        }
    }




    public class WinFormsSession : Session
    {
        public WinFormsSession(Label outputLabel)
        {
            Controller = new WinFormsController {Session = this};
            Viewer = new WinFormsViewer {OutputLabel = outputLabel};
        }
    }

    class WinFormsController : Controller
    {
        public override void TakeInput()
        {
            throw new NotImplementedException();
        }
    }

    class WinFormsViewer : Viewer
    {
        public Label OutputLabel { get; set; }

        public override void View(string filePath)
        {
            OutputLabel.Text = string.Join('\n', ReadFirstKLines(filePath, 10));
        }
    }
}