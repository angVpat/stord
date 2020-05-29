using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms_projet.Controls
{
    class MainMenuStrip : MenuStrip
    {
        public MainMenuStrip()
        {
            Name = "MainMenuStrip";
            Dock = DockStyle.Top;

            fileMenu();
            editMenu();
        }
        public void fileMenu() {
            var fileMenu = new ToolStripMenuItem("Fichier");

            var NewMenu = new ToolStripMenuItem("Nouveau", null, null, Keys.Control | Keys.N);
            var OpenMenu = new ToolStripMenuItem("Ouvrir", null, null, Keys.Control | Keys.O);
            var SaveMenu = new ToolStripMenuItem("Enregistrer", null, null, Keys.Control | Keys.S);
            var SaveAsMenu = new ToolStripMenuItem("Enregistrer sous", null, null, Keys.Control | Keys.Shift | Keys.N);
            var QuitMenu = new ToolStripMenuItem("Quitter", null, null, Keys.Alt | Keys.F4);

            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { NewMenu, OpenMenu, SaveMenu, SaveAsMenu, QuitMenu });
            Items.Add(fileMenu);
        }
        public void editMenu()
        {
            var editMenu = new ToolStripMenuItem("Edition");

            var CancelMenu = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            var RestoreMenu = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.Y);
           
            editMenu.DropDownItems.AddRange(new ToolStripItem[] { CancelMenu, RestoreMenu });
            Items.Add(editMenu);
        }
    }
}
