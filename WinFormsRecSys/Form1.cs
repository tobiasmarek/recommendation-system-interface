using RecommendationSystemInterface;

namespace WinFormsRecSys
{
    public partial class Form1 : Form // tady spis nejakej interface, kterej mi pomuze doplnit property "Session"
    {
        private readonly Session _session;

        public Form1()
        {
            InitializeComponent();

            var viewer = new WinFormsViewer(this.OutputTextBox);
            var session = new WinFormsSession(viewer);
            _session = session; // tohle by mìlo být nìkde jinde (mám v podstatì všechno v Controlleru (Form1 je Controller))
        }

        private void RecBtn_Click(object sender, System.EventArgs e)
        {
            _session.GetRecommendations();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _session.LoadFromCsv("u.data");

            // tady davat timer kterej dìlaáá waiting iluzi ...

            _session.SelectApproach();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _session.LoadFromCsv("subjects_11310.csv");

            _session.SelectApproach();
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