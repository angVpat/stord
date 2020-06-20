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
            
            Cut.Click += (s, e) => richtextBox.Cut();
            Cut.BackColor = MainForm.colorNoir;
            Cut.ForeColor = MainForm.colorBlanche;
            Copy.Click += (s, e) => richtextBox.Copy();
            Copy.BackColor = MainForm.colorNoir;
            Copy.ForeColor = MainForm.colorBlanche;
            Paste.Click += (s, e) => richtextBox.Paste();
            Paste.BackColor = MainForm.colorNoir;
            Paste.ForeColor = MainForm.colorBlanche;
            SelectAll.Click += (s, e) => richtextBox.SelectAll();
            SelectAll.ForeColor = MainForm.colorBlanche;
            SelectAll.BackColor = MainForm.colorNoir2;
            Items.AddRange(new ToolStripItem[] { Cut, Copy, Paste, SelectAll });
        }
    }
}
