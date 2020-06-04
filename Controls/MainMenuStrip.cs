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
        private FontDialog _fontDialog;
        public MainMenuStrip() //Constructeur
        {
            Name = NAME;
            Dock = DockStyle.Top;

            _fontDialog = new FontDialog();

            //appel des fonctions de mon menu dans la classe MainMenuStrip
            FileMenu();
            EditMenu();
            FormatMenu();
            ViewMenu();
        }
        // Définition des différentes fonctions
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

            var Undo = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            var Redo = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.Y);

            Undo.Click += (s, e) => { if (MainForm.RichTextBox.CanUndo) MainForm.RichTextBox.Undo(); };
                //Ssi on peut annuler alors on annule. Cancel= Undo, Restore = Redo
            Redo.Click += (s, e) => { if (MainForm.RichTextBox.CanRedo) MainForm.RichTextBox.Redo(); };

            editMenu.DropDownItems.AddRange(new ToolStripItem[] { Undo, Redo });
            Items.Add(editMenu);
        }
        public void FormatMenu()
        {
            var formatMenu = new ToolStripMenuItem("Format");

            //var FormMenu = new ToolStripMenuItem("Format");
            var FontMenu = new ToolStripMenuItem("Police");
            FontMenu.Click += (s, e) => {
                _fontDialog.Font = MainForm.RichTextBox.Font;
                _fontDialog.ShowDialog(); //afficher les différentes polices
                MainForm.RichTextBox.Font = _fontDialog.Font;
            };

            formatMenu.DropDownItems.AddRange(new ToolStripItem[] {FontMenu });
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

            alwaysMenu.Click += (s, e) =>
            {
                if (alwaysMenu.Checked)
                {
                    alwaysMenu.Checked = false;
                    Program.MainForm.TopMost = false;
                }
                else
                {
                    alwaysMenu.Checked = true;
                    Program.MainForm.TopMost = true;
                }
            };

            zoomStMenu.Click += (s, e) =>
            {
                if (MainForm.RichTextBox.ZoomFactor < 3F)
                {
                    MainForm.RichTextBox.ZoomFactor += 0.3F;
                }
            };

            zoomDoMenu.Click += (s, e) =>
            {
                if (MainForm.RichTextBox.ZoomFactor > 3F)
                {
                    MainForm.RichTextBox.ZoomFactor -= 0.3F;
                }
            };

            zoomReMenu.Click += (s, e) => { MainForm.RichTextBox.ZoomFactor = 1F; };
            Items.Add(viewMenu);
        }

    }
}
