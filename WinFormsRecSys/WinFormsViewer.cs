using RecommendationSystemInterface;

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
            _outputTextBox.Text = string.Join(Environment.NewLine, ReadFirstKLines(filePath, 10));
        }

        public override void ViewString(string str)
        {
            _outputTextBox.Text = str;
        }
    }
}
