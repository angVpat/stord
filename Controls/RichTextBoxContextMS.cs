using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms_projet.Controls
{
    public class RichTextBoxContextMS : ContextMenuStrip
    {

        private RichTextBox _richtextBox;
        private const string NAME = "RtbContextMenuStrip";
        public RichTextBoxContextMS(RichTextBox richtextBox) // Constructeur
        {
            _richtextBox = richtextBox;

            //Instanciation et ajout des raccourcis claviers dans notre Rtb
            var Cut = new ToolStripMenuItem("Couper");
            var Copy = new ToolStripMenuItem("Copier");
            var Paste = new ToolStripMenuItem("Coller");
            var SelectAll = new ToolStripMenuItem("Selectionner tout");
            BackColor = Color.FromArgb(35, 37, 46);
            ForeColor = Color.FromArgb(255, 255, 255);
            Cut.Click += (s, e) => richtextBox.Cut();
            Copy.Click += (s, e) => richtextBox.Copy();
            Paste.Click += (s, e) => richtextBox.Paste();
            SelectAll.Click += (s, e) => richtextBox.SelectAll();
            Items.AddRange(new ToolStripItem[] { Cut, Copy, Paste, SelectAll });
        }
    }
}
