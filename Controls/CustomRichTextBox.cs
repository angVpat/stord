using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms_projet.Controls
{
    public class CustomRichTextBox : RichTextBox
    {
        private const string NAME = "RtbTextFileContents";
        public CustomRichTextBox()
        {
            Name = NAME;
            AcceptsTab = true;
            Font = new Font("Arial", 12.0F, FontStyle.Regular);
            Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.Fixed3D;
            BackColor = Color.FromArgb(35, 37, 46);
            ForeColor = Color.FromArgb(255, 255, 255);
            ContextMenuStrip = new RichTextBoxContextMS(this);
        }
    }
}
