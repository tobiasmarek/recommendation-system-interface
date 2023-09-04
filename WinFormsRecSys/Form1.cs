using RecommendationSystemInterface;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using WinFormsRecSys.Interfaces;

namespace WinFormsRecSys
{
    public partial class Form1 : Form // tady spis nejakej interface, kterej mi pomuze doplnit property "Session" a melo by byt : Controller
    {
        private readonly WinFormsSession _session;
        private readonly WinFormsViewer _viewer;

        public Form1()
        {
            InitializeComponent();

            _viewer = new WinFormsViewer(this.OutputTextBox);
            _session = new WinFormsSession(_viewer);

            loadFromComboBox.SelectedIndex = 0;
            ApproachComboBox.Items.AddRange(_session.GetAvailableClassesOfAType("Approach"));
            UserComboBox.Items.AddRange(_session.GetAvailableClassesOfAType("User"));
        }

        private async void RecBtn_Click(object sender, System.EventArgs e)
        {
            WaitingTimer.Start();
            _session.SelectApproach(_session.GetSelectedComboBoxValues(approachParametersPnl));
            leftSidePnl.Enabled = false;
            _session.CreateUser($"WinForms{UserComboBox.SelectedItem}", userDefinitionTextBox.Text);
            userPnl.Enabled = false;
            userDefinitionTextBox.Enabled = false;

            await Task.Run(() => _session.GetRecommendations());

            WaitingTimer.Stop();
            _viewer.SetLabelText(waitingLbl, "");
            leftSidePnl.Enabled = true;
            userPnl.Enabled = true;
            userDefinitionTextBox.Enabled = true;
            _session.SetTemplateDataPath("");
        }

        private void Demo1Btn_Click(object sender, EventArgs e)
        {
            _session.LoadFromCsv("u.data");
            _viewer.SetTextBoxText(FileTextBox, "u.data");
            ApproachComboBox.SelectedItem = "UserUserCfApproach";
            _session.SetTemplateDataPath("u.item");

            string[] approachParams = new[] {
                "FileStreamLineReader",
                "UserItemMatrixRatingsPreProcessor",
                "CosineSimilarityEvaluator",
                "UserItemMatrixPostProcessor",
                "SimilarityAverageRatingsPredictor",
            };

            _session.FillBoxesInPanel(approachParams, approachParametersPnl);
            _session.SetUserDefinition("MovieDbsUser", userDefinitionTextBox);
            UserComboBox.SelectedItem = "MovieDbsUser";
        }

        private void Demo2Btn_Click(object sender, EventArgs e)
        {
            _session.LoadFromCsv("subjects_11310.csv");
            _viewer.SetTextBoxText(FileTextBox, "subjects_11310.csv");
            ApproachComboBox.SelectedItem = "StringSimilarityContentBasedApproach";
            _session.SetTemplateDataPath("subjects_11310.csv");

            string[] approachParams = new[] {
                "FileStreamLineReader",
                "TfIdf",
                "CosineSimilarityEvaluator",
                "SimilarityVectorPostProcessor",
            };

            _session.FillBoxesInPanel(approachParams, approachParametersPnl);
            _session.SetUserDefinition("SisUser", userDefinitionTextBox);
            UserComboBox.SelectedItem = "SisUser";
        }

        private void DirectoryBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "Select a CSV file",
                InitialDirectory = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "Data"))
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _session.LoadFromCsv(openFileDialog.FileName);
                _viewer.SetTextBoxText(FileTextBox, Path.GetFileName(openFileDialog.FileName));
            }
        }

        private void FileTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                _session.LoadFromCsv(((TextBox)sender).Text);
                _viewer.SetTextBoxText(FileTextBox, Path.GetFileName(((TextBox)sender).Text));
            }
        }

        private void ApproachComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string? selectedApproach = ((ComboBox)sender).SelectedItem.ToString();

            if (selectedApproach is not null)
            {
                _session.CreateApproachDialogue(_session.GetApproachCtorParameterTypes(selectedApproach), approachParametersPnl, templateComboBox, templatePropertyLbl);
            }
        }

        private void WaitingTimer_Tick(object sender, EventArgs e)
        {
            _viewer.SetLabelText(waitingLbl, "".PadLeft((waitingLbl.Text.Length + 1) % 4, '.'));
        }

        private void UserComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string? selectedUser = ((ComboBox)sender).SelectedItem.ToString();

            if (selectedUser is not null)
            {
                _session.SetUserDefinition(selectedUser, userDefinitionTextBox);
            }
        }
    }
}