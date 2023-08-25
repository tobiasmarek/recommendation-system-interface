using RecommendationSystemInterface;
using System.Windows.Forms;

namespace WinFormsRecSys
{
    public partial class Form1 : Form // tady spis nejakej interface, kterej mi pomuze doplnit property "Session" a melo by byt : Controller
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
            // tady davat timer kterej dìlaáá waiting iluzi ...

            _session.GetRecommendations();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _session.LoadFromCsv("u.data");

            _session.SelectApproach();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _session.LoadFromCsv("subjects_11310.csv");

            _session.SelectApproach();
        }

        private void MagGlassBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select a CSV file";
            openFileDialog.InitialDirectory = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "Data"));

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _session.LoadFromCsv(openFileDialog.FileName);

                FileTextBox.Text = Path.GetFileName(openFileDialog.FileName);
            }
        }

        private void FileTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                _session.LoadFromCsv(((TextBox)sender).Text);

                FileTextBox.Text = Path.GetFileName(((TextBox)sender).Text);
            }
        }
    }
}