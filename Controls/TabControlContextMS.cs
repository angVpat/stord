using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms_projet.Controls
{
    class TabControlContextMS : ContextMenuStrip
    {
        private const string NAME = "TabControlContextMenuStrip";
        public TabControlContextMS() //Constructeur
        {
            Name = NAME;
            //Instanciation et ajout des éléments de notre TabControlContextMS
            var closeTab = new ToolStripMenuItem("Fermer");
            var AllTabExceptThis = new ToolStripMenuItem("Tout fermer sauf ce fichier");
            var OpenFileInEx = new ToolStripMenuItem("Ouvrir le répertoire du fichier en cours dans l'explorateur");
            BackColor = Color.FromArgb(35, 37, 46);
            ForeColor = Color.FromArgb(255, 255, 255);
            Items.AddRange(new ToolStripItem[] { closeTab, AllTabExceptThis, OpenFileInEx });

        }
    }
}
