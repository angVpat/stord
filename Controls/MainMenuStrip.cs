using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms_projet.Controls
{
    public class MainMenuStrip : MenuStrip
    {
        private const string NAME = "MainMenuStrip";
        public MainMenuStrip()
        {
            Name = NAME;
            Dock = DockStyle.Top;

            FileMenu();
            EditMenu();
            FormatMenu();
            ViewMenu();
        }
        public void FileMenu() {
            var fileMenu = new ToolStripMenuItem("Fichier");

            var NewMenu = new ToolStripMenuItem("Nouveau", null, null, Keys.Control | Keys.N);
            var OpenMenu = new ToolStripMenuItem("Ouvrir", null, null, Keys.Control | Keys.O);
            var SaveMenu = new ToolStripMenuItem("Enregistrer", null, null, Keys.Control | Keys.S);
            var SaveAsMenu = new ToolStripMenuItem("Enregistrer sous", null, null, Keys.Control | Keys.Shift | Keys.N);
            var QuitMenu = new ToolStripMenuItem("Quitter", null, null, Keys.Alt | Keys.F4);

            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { NewMenu, OpenMenu, SaveMenu, SaveAsMenu, QuitMenu });
            Items.Add(fileMenu);
        }
        public void EditMenu()
        {
            var editMenu = new ToolStripMenuItem("Edition");

            var CancelMenu = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            var RestoreMenu = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.Y);
           
            editMenu.DropDownItems.AddRange(new ToolStripItem[] { CancelMenu, RestoreMenu });
            Items.Add(editMenu);
        }
        public void FormatMenu()
        {
            var formatMenu = new ToolStripMenuItem("Format");

            var FormMenu = new ToolStripMenuItem("Format");
            var FontMenu = new ToolStripMenuItem("Police");

            formatMenu.DropDownItems.AddRange(new ToolStripItem[] { FormMenu, FontMenu });
            Items.Add(formatMenu);
        }
        public void ViewMenu() {

            var viewMenu = new ToolStripMenuItem("Affichage");

                var alwaysMenu = new ToolStripMenuItem("Toujous devant");
                var zoomMenu = new ToolStripMenuItem("Zoom");

                    var zoomStMenu = new ToolStripMenuItem("Zoom avant", null, null, Keys.Control|Keys.Add);
                    var zoomDoMenu = new ToolStripMenuItem("Zoom arrière", null,null, Keys.Control | Keys.Subtract);
                    var zoomReMenu = new ToolStripMenuItem("Zoom par défaut", null, null, Keys.Control | Keys.Divide);
                    zoomMenu.DropDownItems.AddRange(new ToolStripItem[] { zoomStMenu, zoomDoMenu, zoomReMenu });

            zoomStMenu.ShortcutKeyDisplayString = "Ctrl +";
            zoomDoMenu.ShortcutKeyDisplayString = "Ctrl -";
            zoomReMenu.ShortcutKeyDisplayString = "Ctrl /";
            viewMenu.DropDownItems.AddRange(new ToolStripItem[] { alwaysMenu, zoomMenu });

            Items.Add(viewMenu);
        }

    }
}
