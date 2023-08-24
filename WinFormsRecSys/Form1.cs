using RecommendationSystemInterface;

namespace WinFormsRecSys
{
    public partial class Form1 : Form
    {
        private Session session { get; set; }

        public Form1()
        {
            InitializeComponent();
            this.session = new WinFormsSession(this.OutputTextBox);
        }

        private void RecBtn_Click(object sender, System.EventArgs e)
        {
            session.GetRecommendations();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            session.LoadFromCsv("u.data");

            // tady davat timer kterej dìlaáá waiting iluzi ...

            session.SelectApproach();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            session.LoadFromCsv("subjects_11310.csv");

            session.SelectApproach();
        }
    }

    class WinFormsController : Controller
    {
        public override void TakeInput()
        {
            throw new NotImplementedException();
        }
    }
}