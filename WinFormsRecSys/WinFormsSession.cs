using RecommendationSystemInterface;
using WinFormsRecSys.Interfaces;

namespace WinFormsRecSys
{
    /// <summary>
    /// A Session that is controlled and shown in WinForms application.
    /// </summary>
    public class WinFormsSession : Session
    {
        public WinFormsSession(Viewer viewer) : base(viewer) { }

        /// <summary>
        /// Sets TemplateDataPath to a template file by getting its path at first
        /// and then, if needed, searches for a file name in Data directory. 
        /// </summary>
        public void SetTemplateDataPath(string path)
        {
            if (path == "") { TemplateDataPath = null; return; }

            if (File.Exists(path))
            {
                TemplateDataPath = path;
            }
            else
            {
                var newPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "Data", path));

                if (File.Exists(newPath))
                {
                    TemplateDataPath = newPath;
                }
                else
                {
                    TemplateDataPath = null;
                    Viewer.ViewString("Template file does not exist!");
                }
            }
        }

        /// <summary>
        /// Gets a string of how the user should look and displays it in a given textBox.
        /// </summary>
        public void SetUserDefinition(string selectedUser, TextBox textBox)
        {
            var instance = GetInstance($"WinForms{selectedUser}", new object[] { " " });

            if (instance is IWinFormsUserUtil userExampleObj)
            {
                textBox.Text = userExampleObj.ShowExampleParamString();
            }
        }

        /// <summary>
        /// Recreates a panel and shows comboBoxes with appropriate labels and items.
        /// Serves for an easy selection of Approach.
        /// </summary>
        public void CreateApproachDialogue(string[] constructorParameters, Panel panel, ComboBox templateComboBox, Label templatePropertyLbl)
        {
            ClearPanel(panel, new Control[] { templateComboBox, templatePropertyLbl });

            int heightShift = templateComboBox.Size.Height + 20;
            panel.Size = new Size(panel.Size.Width, constructorParameters.Length * heightShift);

            for (int i = 0; i < constructorParameters.Length; i++)
            {
                Label newLabel = new();
                panel.Controls.Add(newLabel);
                ComboBox newCombo = new();
                panel.Controls.Add(newCombo);
                panel.Controls.SetChildIndex(newCombo, i); // So that they appear first when looping through Controls

                newLabel.AutoSize = templatePropertyLbl.AutoSize;
                newLabel.BackColor = templatePropertyLbl.BackColor;
                newLabel.Font = templatePropertyLbl.Font;
                newLabel.ForeColor = templatePropertyLbl.ForeColor;

                newCombo.DropDownStyle = templateComboBox.DropDownStyle;
                newCombo.FlatStyle = templateComboBox.FlatStyle;
                newCombo.Font = templateComboBox.Font;
                newCombo.Size = new Size(templateComboBox.Size.Width, templateComboBox.Size.Height);
                newCombo.FormattingEnabled = templateComboBox.FormattingEnabled;

                newLabel.Text = (constructorParameters[i].Split('.'))[^1];
                newLabel.Location = new Point(templatePropertyLbl.Location.X, templatePropertyLbl.Location.Y + heightShift * i);


                newCombo.Location = new Point(templateComboBox.Location.X, templateComboBox.Location.Y + heightShift * i);
                newCombo.Items.AddRange(GetClassesImplementingInterface(constructorParameters[i]));
            }
        }

        /// <summary>
        /// Gets selected values of comboBoxes in specified panel.
        /// </summary>
        /// <returns>ComboBoxes' selected items in string format</returns>
        public string[] GetSelectedComboBoxValues(Panel panel)
        {
            Queue<string> selectedApproachParams = new();

            foreach (Control control in panel.Controls)
            {
                if (control.Visible && control is ComboBox combo)
                {
                    if (combo.SelectedItem is null) { continue; }
                    string? selectedName = combo.SelectedItem.ToString();

                    if (selectedName is not null)
                    {
                        selectedApproachParams.Enqueue(selectedName);
                    }
                }
            }

            return selectedApproachParams.ToArray();
        }

        public void FillBoxesInPanel(string[] items, Panel panel)
        {
            int index = 0;
            foreach (Control control in panel.Controls)
            {
                if (control is ComboBox combo && control.Enabled)
                {
                    if (index >= items.Length) { break; }
                    combo.SelectedItem = items[index];
                    index++;
                }
            }
        }

        /// <summary>
        /// Removes everything but the solids (specified Controls that remain) from a panel.
        /// </summary>
        protected void ClearPanel(Panel panel, Control[] solids)
        {
            List<Control> controlsToRemove = new();

            foreach (Control control in panel.Controls)
            {
                if (!solids.Contains(control))
                {
                    controlsToRemove.Add(control);
                }
            }

            foreach (Control control in controlsToRemove)
            {
                panel.Controls.Remove(control);
            }
        }
    }
}
