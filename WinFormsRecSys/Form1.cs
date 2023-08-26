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

            var viewer = new WinFormsViewer(this.OutputTextBox); // je k necemu to rozdelovat?
            var session = new WinFormsSession(viewer);
            _session = session; // tohle by mìlo být nìkde jinde (mám v podstatì všechno v Controlleru (Form1 je Controller))

            loadFromComboBox.SelectedIndex = 0;
            approachComboBox.Items.AddRange(_session.GetAvailableApproaches());
        }

        private async void RecBtn_Click(object sender, System.EventArgs e)
        {
            waitingTimer.Start();

            await Task.Run(() => _session.GetRecommendations());

            waitingTimer.Stop();
            waitingLbl.Text = "";
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

        private void approachComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var selectedApproach = (((ComboBox)sender).SelectedItem).ToString();

            if (selectedApproach is null) { return; }

            CreateApproachDialog(_session.GetConstructorParameterTypes(selectedApproach));
        }

        private void waitingTimer_Tick(object sender, EventArgs e)
        {
            waitingLbl.Text = "".PadLeft((waitingLbl.Text.Length + 1) % 4, '.');
        }

        // Helper functions
        private void CreateApproachDialog(string[] constructorParameters)
        {
            foreach (Control control in approachParametersPnl.Controls)
            {
                if (control.Name == "templateComboBox" || control.Name == "TemplatePropertyLabel") { continue; }

                approachParametersPnl.Controls.Remove(control);
            }

            int heightShift = templateComboBox.Size.Height + 20;
            approachParametersPnl.Size = new Size(approachParametersPnl.Size.Width, constructorParameters.Length * heightShift);

            for (int i = 0; i < constructorParameters.Length; i++)
            {
                Label newLabel = new Label();
                approachParametersPnl.Controls.Add(newLabel);
                ComboBox newCombo = new ComboBox();
                approachParametersPnl.Controls.Add(newCombo);

                newLabel.AutoSize = TemplatePropertyLabel.AutoSize;
                newLabel.BackColor = TemplatePropertyLabel.BackColor;
                newLabel.Font = TemplatePropertyLabel.Font;
                newLabel.ForeColor = TemplatePropertyLabel.ForeColor;

                newCombo.DropDownStyle = templateComboBox.DropDownStyle;
                newCombo.FlatStyle = templateComboBox.FlatStyle;
                newCombo.Font = templateComboBox.Font;
                newCombo.Size = new Size(templateComboBox.Size.Width, templateComboBox.Size.Height);
                newCombo.FormattingEnabled = templateComboBox.FormattingEnabled;

                newLabel.Text = (constructorParameters[i].Split('.'))[^1];
                newLabel.Location = new Point(TemplatePropertyLabel.Location.X, TemplatePropertyLabel.Location.Y + heightShift * i);


                newCombo.Location = new Point(templateComboBox.Location.X, templateComboBox.Location.Y + heightShift * i);
                newCombo.Items.AddRange(_session.GetClassesImplementing(constructorParameters[i]));
            }
        }
    }
}