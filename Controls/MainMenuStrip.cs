using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WindowsForms_projet.Objects;
using System.Linq;
using WindowsForms_projet.Services;

namespace WindowsForms_projet.Controls
{
    public class MainMenuStrip : MenuStrip
    {
        private const string NAME = "MainMenuStrip";
        private CustomRichTextBox _customText;
        private MainForm _form;
        private FontDialog _fontDialog;
        private OpenFileDialog _openFileDialog;
        private SaveFileDialog _saveFileDialog;
        public MainMenuStrip() //Constructeur
        {
            Name = NAME;
            Dock = DockStyle.Top;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            ForeColor = Color.FromArgb(255, 255, 255);
            _fontDialog = new FontDialog();
            _openFileDialog = new OpenFileDialog();
            _saveFileDialog = new SaveFileDialog();
            _customText = new CustomRichTextBox();
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
            fileMenu.BackColor = MainForm.colorNoir;
            fileMenu.ForeColor = MainForm.colorBlanche;
            var newFile = new ToolStripMenuItem("Nouveau", null, null, Keys.Control | Keys.N);
            newFile.BackColor= MainForm.colorNoir;
            newFile.ForeColor = MainForm.colorBlanche;
            newFile.Image = System.Drawing.Image.FromFile(@"..\..\Icons\New-icon.png");
            var open = new ToolStripMenuItem("Ouvrir", null, null, Keys.Control | Keys.O);
            open.Image = System.Drawing.Image.FromFile(@"..\..\Icons\Open-icon.png");
            open.BackColor= MainForm.colorNoir;
            open.ForeColor = MainForm.colorBlanche;
            var save = new ToolStripMenuItem("Enregistrer", null, null, Keys.Control | Keys.S);
            save.BackColor= MainForm.colorNoir;
            save.ForeColor = MainForm.colorBlanche;
            save.Image = System.Drawing.Image.FromFile(@"..\..\Icons\Save-icon.png");
            var saveAs = new ToolStripMenuItem("Enregistrer sous", null, null, Keys.Control | Keys.Shift | Keys.S);
            saveAs.BackColor= MainForm.colorNoir;
            saveAs.ForeColor = MainForm.colorBlanche;
            var quit = new ToolStripMenuItem("Quitter", null, null, Keys.Alt | Keys.F4);
            quit.BackColor= MainForm.colorNoir;
            quit.ForeColor = MainForm.colorBlanche;
            newFile.Click += (s, e) =>
             {
                 var tabControl = _form.MainTabControl;
                 var tabCount = tabControl.TabCount;

                 var fileName = $"Sans Titre {tabCount + 1}";
                 var file = new TextFile(fileName);
                 var rtb = new CustomRichTextBox();
                 tabControl.TabPages.Add(file.SafeFileName);

                 var newTabPages = tabControl.TabPages[tabCount];

                 newTabPages.Controls.Add(rtb);
                 _form.Session.Files.Add(file);
                 tabControl.SelectedTab = newTabPages;
                 _form.CurrentFile = file;
                 _form.CurrentRtb = rtb;
             };

            open.Click += async(s, e) =>
            {
                if(_openFileDialog.ShowDialog()==DialogResult.OK)
                {
                    var tabControl = _form.MainTabControl;
                    var tabCount = tabControl.TabCount;
                    var file = new TextFile(_openFileDialog.FileName);
                    var rtb = new CustomRichTextBox();
                    _form.Text = $"{file.FileName} - Stord";
                    using(StreamReader reader=new StreamReader(file.FileName))
                    {
                        file.Contents =await reader.ReadToEndAsync();
                    }
                    rtb.Text = file.Contents;
                    tabControl.TabPages.Add(file.SafeFileName);
                    tabControl.TabPages[tabCount].Controls.Add(rtb);
                    _form.Session.Files.Add(file);
                    _form.CurrentRtb = rtb;
                    _form.CurrentFile= file;
                    tabControl.SelectedTab = tabControl.TabPages[tabCount];
                }
            };

            save.Click += async (s, e) =>
            {
                var CurrentFile = _form.CurrentFile;
                var CurrentRtbText = _form.CurrentRtb.Text;

                if(CurrentFile.Contents != CurrentRtbText)
                {
                    if (File.Exists(CurrentFile.FileName))
                    {
                        using (StreamWriter writer = File.CreateText(CurrentFile.FileName))
                        {
                            await writer.WriteAsync(CurrentFile.Contents);
                        }
                        CurrentFile.Contents = CurrentRtbText;

                        _form.MainTabControl.SelectedTab.Text = CurrentFile.SafeFileName;
                        _form.Text = CurrentFile.FileName;
                        _form.CurrentFile = CurrentFile;
                    }
                    else
                    {
                        saveAs.PerformClick();
                    }
                }
            };

            saveAs.Click += async(s, e) => 
            { 
                if(_saveFileDialog.ShowDialog() == DialogResult.OK) 
                {
                    var newFileName = _saveFileDialog.FileName;
                    var alreadyExists = false;

                    foreach (var file in _form.Session.Files)
                    {
                        if(file.FileName == newFileName)
                        {
                            MessageBox.Show("Ce fichier est déja ouvert dans Stord.NET", "ERREUR",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alreadyExists = true;
                            break;
                        }
                    }
                    //Si le fichier à enregistrer n'existe pas déja
                    if(! alreadyExists) 
                    //Créer ou ouvrir un fichier pour mettre du texte
                    {
                        var file = new TextFile(newFileName) { Contents = _form.CurrentRtb.Text };

                        var oldfile = _form.Session.Files.Where(x => x.FileName == _form.CurrentFile.FileName).First();
                       
                        _form.Session.Files.Replace(oldfile, file);

                        using (StreamWriter writer = File.CreateText(file.FileName))
                        {
                            await writer.WriteAsync(file.Contents);
                        }
                        _form.MainTabControl.SelectedTab.Text = file.SafeFileName;
                        _form.Text = file.FileName;
                        _form.CurrentFile = file;

                    }
                }
            };

            quit.Click += (s, e) =>
            {
                Application.Exit();
            };
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { newFile, open, save, saveAs, quit });
            Items.Add(fileMenu);
        }
        public void EditMenu()
        {
            var editMenu = new ToolStripMenuItem("Edition");
            editMenu.BackColor = MainForm.colorNoir;
            editMenu.ForeColor = MainForm.colorBlanche;
            var Undo = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            Undo.BackColor = MainForm.colorNoir;
            Undo.Image = System.Drawing.Image.FromFile(@"..\..\Icons\Undo-icon.png");
            Undo.ForeColor = MainForm.colorBlanche;
            var Redo = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.Y);
            Redo.BackColor = MainForm.colorNoir;
            Redo.ForeColor = MainForm.colorBlanche;
            Redo.Image = System.Drawing.Image.FromFile(@"..\..\Icons\Redo-icon.png");
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
            FontMenu.BackColor = MainForm.colorNoir;
            FontMenu.ForeColor = MainForm.colorBlanche;
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
            alwaysMenu.BackColor = MainForm.colorNoir;
            alwaysMenu.ForeColor = MainForm.colorBlanche;
            var zoomMenu = new ToolStripMenuItem("Zoom");
            zoomMenu.BackColor = MainForm.colorNoir;
            zoomMenu.ForeColor = MainForm.colorBlanche;
            var zoomStMenu = new ToolStripMenuItem("Zoom avant", null, null, Keys.Control | Keys.Add);
            zoomStMenu.BackColor = MainForm.colorNoir;
            zoomStMenu.ForeColor = MainForm.colorBlanche;
            var zoomDoMenu = new ToolStripMenuItem("Zoom arrière", null, null, Keys.Control | Keys.Subtract);
            zoomDoMenu.BackColor = MainForm.colorNoir;
            zoomDoMenu.ForeColor = MainForm.colorBlanche;
            var zoomReMenu = new ToolStripMenuItem("Zoom par défaut", null, null, Keys.Control | Keys.Divide);
            zoomReMenu.BackColor = MainForm.colorNoir;
            zoomReMenu.ForeColor = MainForm.colorBlanche;
            /*var dark = new ToolStripMenuItem("Mode Sombre");
            dark.BackColor = MainForm.colorNoir;
            dark.ForeColor = MainForm.colorBlanche;
            var clair = new ToolStripMenuItem("Mode Clair");
            clair.ForeColor = MainForm.colorBlanche;
            clair.BackColor = MainForm.colorNoir;*/
            zoomMenu.DropDownItems.AddRange(new ToolStripItem[] { zoomStMenu, zoomDoMenu, zoomReMenu });

            zoomStMenu.ShortcutKeyDisplayString = "Ctrl +";
            zoomDoMenu.ShortcutKeyDisplayString = "Ctrl -";
            zoomReMenu.ShortcutKeyDisplayString = "Ctrl /";
            viewMenu.DropDownItems.AddRange(new ToolStripItem[] { alwaysMenu, zoomMenu });

            /*clair.Click += (s, e) =>
            {
                _form.BackColor = MainForm.colorBlanche;
                _customText.BackColor = MainForm.colorBlanche;
                this.BackColor = MainForm.colorBlanche;
                this.ForeColor = MainForm.colorNoir;
                alwaysMenu.BackColor = MainForm.colorBlanche;
                alwaysMenu.ForeColor = MainForm.colorNoir;
                zoomStMenu.BackColor = MainForm.colorBlanche;
                zoomStMenu.ForeColor = MainForm.colorNoir;
                zoomDoMenu.BackColor = MainForm.colorBlanche;
                zoomDoMenu.ForeColor = MainForm.colorNoir;
                zoomReMenu.BackColor = MainForm.colorBlanche;
                zoomReMenu.ForeColor = MainForm.colorNoir;
                clair.ForeColor = MainForm.colorNoir;
                clair.BackColor = MainForm.colorBlanche;
                dark.BackColor = MainForm.colorBlanche;
                dark.ForeColor = MainForm.colorNoir;

            };
            dark.Click += (s, e) =>
            {
                alwaysMenu.BackColor = MainForm.colorNoir;
                alwaysMenu.ForeColor = MainForm.colorBlanche;
                zoomMenu.BackColor = MainForm.colorNoir;
                zoomMenu.ForeColor = MainForm.colorBlanche;
                zoomStMenu.BackColor = MainForm.colorNoir;
                zoomStMenu.ForeColor = MainForm.colorBlanche;
                zoomDoMenu.BackColor = MainForm.colorNoir;
                zoomDoMenu.ForeColor = MainForm.colorBlanche;
                zoomReMenu.BackColor = MainForm.colorNoir;
                zoomReMenu.ForeColor = MainForm.colorBlanche;
                dark.BackColor = MainForm.colorNoir;
                dark.ForeColor = MainForm.colorBlanche;
                clair.ForeColor = MainForm.colorBlanche;
                clair.BackColor = MainForm.colorNoir;
                
            };*/
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
