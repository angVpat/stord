using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms_projet.Controls
{
    public class RichTextBoxContextMS : ContextMenuStrip
    {
        
        private  RichTextBox _richtextBox; 
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
            Copy.Click += (s, e) => richtextBox.Copy();
            SelectAll.Click += (s, e) => richtextBox.Paste();
            SelectAll.Click += (s, e) => richtextBox.SelectAll();
            Items.AddRange(new ToolStripItem[] { Cut, Copy, Paste, SelectAll });
        }
    }
}
