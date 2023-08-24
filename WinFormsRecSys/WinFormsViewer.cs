using RecommendationSystemInterface;

namespace WinFormsRecSys
{
    class WinFormsViewer : Viewer
    {
        public TextBox OutputTextBox;

        public WinFormsViewer(TextBox outputTextBox)
        {
            OutputTextBox = outputTextBox;
        }

        public override void ViewFile(string filePath)
        {
            OutputTextBox.Text = string.Join(Environment.NewLine, ReadFirstKLines(filePath, 10));
        }

        public override void ViewString(string str)
        {
            throw new NotImplementedException();
        }
    }
}
