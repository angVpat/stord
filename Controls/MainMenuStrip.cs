using System.Drawing;
using System.Windows.Forms;
using WindowsForms_projet.Objects;

namespace WindowsForms_projet.Controls
{
    public class MainMenuStrip : MenuStrip
    {
        private const string NAME = "MainMenuStrip";

        private MainForm _form;
        private FontDialog _fontDialog;
        public MainMenuStrip() //Constructeur
        {
            Name = NAME;
            Dock = DockStyle.Top;
            BackColor = Color.FromArgb(35, 37, 46);
            ForeColor = Color.FromArgb(255, 255, 255);
            _fontDialog = new FontDialog();

            //appel des fonctions de mon menu dans la classe MainMenuStrip
            FileMenu();
            EditMenu();
            FormatMenu();
            ViewMenu();

            HandleCreated += (s, e) =>
            {
                _form = FindForm() as MainForm;
            };
        }
        // Définition des différentes fonctions
        public void FileMenu()
        {

            var fileMenu = new ToolStripMenuItem("Fichier");
            fileMenu.BackColor = Color.FromArgb(35, 37, 46);
            var newFile = new ToolStripMenuItem("Nouveau", null, null, Keys.Control | Keys.N);
            var open = new ToolStripMenuItem("Ouvrir", null, null, Keys.Control | Keys.O);
            var save = new ToolStripMenuItem("Enregistrer", null, null, Keys.Control | Keys.S);
            var saveAs = new ToolStripMenuItem("Enregistrer sous", null, null, Keys.Control | Keys.Shift | Keys.N);
            var quit = new ToolStripMenuItem("Quitter", null, null, Keys.Alt | Keys.F4);

            newFile.Click += (s, e) =>
             {
                 var tabControl = _form.MainTabControl;
                 var tabPageCount = tabControl.TabPages.Count;

                 var fileName = $"Sans Titre {tabPageCount + 1}";
                 var file = new TextFile(fileName);
                 var rtb = new CustomRichTextBox();
                 tabControl.TabPages.Add(file.SafeFileName);

                 var newTabPages = tabControl.TabPages[tabPageCount];

                 newTabPages.Controls.Add(rtb);
                 tabControl.SelectedTab = newTabPages;
                 _form.Session.TextFiles.Add(file);
                 _form.CurrentFile = file;
                 _form.CurrentRtb = rtb;
             };
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { newFile, open, save, saveAs, quit });
            Items.Add(fileMenu);
        }
        public void EditMenu()
        {
            var editMenu = new ToolStripMenuItem("Edition");

            var Undo = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            var Redo = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.Y);
            
            Undo.Click += (s, e) => { if (_form.CurrentRtb.CanUndo) _form.CurrentRtb.Undo(); };
            //Ssi on peut annuler alors on annule. Cancel= Undo, Restore = Redo
            Redo.Click += (s, e) => { if (_form.CurrentRtb.CanRedo) _form.CurrentRtb.Redo(); };

            editMenu.DropDownItems.AddRange(new ToolStripItem[] { Undo, Redo });
            Items.Add(editMenu);
        }
        public void FormatMenu()
        {
            var formatMenu = new ToolStripMenuItem("Format");

            //var FormMenu = new ToolStripMenuItem("Format");
            var FontMenu = new ToolStripMenuItem("Police");
            FontMenu.Click += (s, e) =>
            {
                _fontDialog.Font = _form.CurrentRtb.Font;
                _fontDialog.ShowDialog(); //afficher les différentes polices
                _form.CurrentRtb.Font = _fontDialog.Font;
            };

            formatMenu.DropDownItems.AddRange(new ToolStripItem[] { FontMenu });
            Items.Add(formatMenu);
        }
        public void ViewMenu()
        {

            var viewMenu = new ToolStripMenuItem("Affichage");

            var alwaysMenu = new ToolStripMenuItem("Toujous devant");
            var zoomMenu = new ToolStripMenuItem("Zoom");

            var zoomStMenu = new ToolStripMenuItem("Zoom avant", null, null, Keys.Control | Keys.Add);
            var zoomDoMenu = new ToolStripMenuItem("Zoom arrière", null, null, Keys.Control | Keys.Subtract);
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
                    _form.TopMost = false;
                }
                else
                {
                    alwaysMenu.Checked = true;
                    _form.TopMost = true;
                }
            };

            zoomStMenu.Click += (s, e) =>
            {
                if (_form.CurrentRtb.ZoomFactor < 3F)
                {
                    _form.CurrentRtb.ZoomFactor += 0.3F;
                }
            };

            zoomDoMenu.Click += (s, e) =>
            {
                if (_form.CurrentRtb.ZoomFactor > 3F)
                {
                    _form.CurrentRtb.ZoomFactor -= 0.3F;
                }
            };

            zoomReMenu.Click += (s, e) => { _form.CurrentRtb.ZoomFactor = 1F; };
            Items.Add(viewMenu);
        }

    }
}
