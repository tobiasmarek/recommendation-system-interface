using RecommendationSystemInterface;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using WinFormsRecSys.Interfaces;

namespace WinFormsRecSys
{
    public partial class Form1 : Form
    {
        private readonly WinFormsSession _session;
        private readonly WinFormsViewer _viewer;

        public Form1()
        {
            InitializeComponent();

            _viewer = new WinFormsViewer(this.OutputTextBox);
            _session = new WinFormsSession(_viewer);

            loadFromComboBox.SelectedIndex = 0; // Select LoadFromCSV option
            ApproachComboBox.Items.AddRange(_session.GetAvailableClassesOfAType("Approach")); // Fill combo box with instantiable Approach classes
            UserComboBox.Items.AddRange(_session.GetAvailableClassesOfAType("User")); // ..with instantiable User classes
        }

        /// <summary>
        /// Selects Approach given in combo box values, creates a user and starts recommending asynchronously.
        /// </summary>
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
        }

        private void Demo1Btn_Click(object sender, EventArgs e)
        {
            _session.LoadFromCsv("u.data");
            _viewer.SetTextBoxText(FileTextBox, "u.data");
            ApproachComboBox.SelectedItem = "UserUserCfApproach";
            _session.SetConvertorParams("u.item", 1);

            string[] approachParams = new[] {
                "FileStreamLineReader",
                "UserItemMatrixRatingsPreProcessor",
                "CosineSimilarityEvaluator",
                "UserItemMatrixPostProcessor",
                "UserSimilarityAverageRatingsPredictor",
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
            _session.SetConvertorParams("subjects_11310.csv");

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

        private void Demo3Btn_Click(object sender, EventArgs e)
        {
            _session.LoadFromCsv("u.data");
            _viewer.SetTextBoxText(FileTextBox, "u.data");
            ApproachComboBox.SelectedItem = "ItemItemCfApproach";
            _session.SetConvertorParams("u.item", 1);

            string[] approachParams = new[] {
                "FileStreamLineReader",
                "UserItemMatrixRatingsPreProcessor",
                "CosineSimilarityEvaluator",
                "UserItemMatrixPostProcessor",
                "ItemSimilarityAverageRatingsPredictor",
            };

            _session.FillBoxesInPanel(approachParams, approachParametersPnl);
            _session.SetUserDefinition("MovieDbsUser", userDefinitionTextBox);
            UserComboBox.SelectedItem = "MovieDbsUser";
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

        /// <summary>
        /// Writing down a file name or a path and then confirming with Enter key selects a file to load from.
        /// </summary>
        private void FileTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                _session.LoadFromCsv(((TextBox)sender).Text);
                _viewer.SetTextBoxText(FileTextBox, Path.GetFileName(((TextBox)sender).Text));
            }
        }

        /// <summary>
        /// When changed a new dialogue with a different Approach parameters appears.
        /// </summary>
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

        /// <summary>
        /// When changed loads a different User demo value as a template.
        /// </summary>
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