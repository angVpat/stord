using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms_projet.Controls
{
    class TabControlContextMS : ContextMenuStrip
    {
        private const string NAME ="TabControlContextMenuStrip";
        public TabControlContextMS()
        {
            Name = NAME;

            var closeTab = new ToolStripMenuItem("Fermer");
            var AllTabExceptThis = new ToolStripMenuItem("Tout fermer sauf ce fichier");
            var OpenFileInEx = new ToolStripMenuItem("Ouvrir le répertoire du fichier en cours dans l'explorateur");

            Items.AddRange(new ToolStripItem[] {closeTab, AllTabExceptThis, OpenFileInEx });
            
        }
    }
}
