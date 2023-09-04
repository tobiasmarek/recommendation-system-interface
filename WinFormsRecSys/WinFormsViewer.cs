using RecommendationSystemInterface;

namespace WinFormsRecSys
{
    /// <summary>
    /// A Viewer which result is shown in Windows Forms app.
    /// </summary>
    class WinFormsViewer : Viewer
    {
        private readonly TextBox _outputTextBox; // A united output window

        public WinFormsViewer(TextBox outputTextBox)
        {
            _outputTextBox = outputTextBox;
        }

        public override void ViewFile(string filePath)
        {
            if (_outputTextBox.InvokeRequired)
            {
                _outputTextBox.Invoke(new Action<string>(ViewFile), filePath);
            }
            else
            {
                _outputTextBox.Text = string.Join(Environment.NewLine, ReadFirstKLines(filePath, 10));
            }
        }

        public override void ViewString(string str)
        {
            if (_outputTextBox.InvokeRequired)
            {
                _outputTextBox.Invoke(new Action<string>(ViewString), str);
            }
            else
            {
                _outputTextBox.Text = str;
            }
        }

        public void SetLabelText(Label label, string text)
        {
            label.Text = text;
        }

        public void SetTextBoxText(TextBox textBox, string text)
        {
            textBox.Text = text;
        }
    }
}
