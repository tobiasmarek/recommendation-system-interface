using RecommendationSystemInterface;
using static System.Net.Mime.MediaTypeNames;

namespace WinFormsRecSys
{
    class WinFormsViewer : Viewer
    {
        private readonly TextBox _outputTextBox;

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
    }
}
