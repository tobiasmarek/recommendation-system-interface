using RecommendationSystemInterface;

namespace WinFormsRecSys
{
    public partial class Form1 : Form // tady spis nejakej interface, kterej mi pomuze doplnit property "Session"
    {
        private Session Session;

        public Form1()
        {
            InitializeComponent();

            var viewer = new WinFormsViewer(this.OutputTextBox);
            var session = new WinFormsSession(viewer);
            Session = session; // tohle by mìlo být nìkde jinde (mám v podstatì všechno v Controlleru (Form1 je Controller))
        }

        private void RecBtn_Click(object sender, System.EventArgs e)
        {
            Session.GetRecommendations();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Session.LoadFromCsv("u.data");

            // tady davat timer kterej dìlaáá waiting iluzi ...

            Session.SelectApproach();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Session.LoadFromCsv("subjects_11310.csv");

            Session.SelectApproach();
        }
    }

    // tohle je tady plonkový
    class WinFormsController : Controller
    {
        public WinFormsController(Session session) : base(session)
        {
            throw new NotImplementedException();
        }
    }
}