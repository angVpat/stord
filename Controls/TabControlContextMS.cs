using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsForms_projet.Objects;

namespace WindowsForms_projet.Controls
{
    class TabControlContextMS : ContextMenuStrip
    {
        private const string NAME = "TabControlContextMenuStrip";
        private MainForm _form;
        public TabControlContextMS() //Constructeur
        {
            Name = NAME;
            //Instanciation et ajout des éléments de notre TabControlContextMS
            var closeTab = new ToolStripMenuItem("Fermer");
            closeTab.BackColor = MainForm.colorNoir;
            closeTab.ForeColor = MainForm.colorBlanche;
            var AllTabExceptThis = new ToolStripMenuItem("Tout fermer sauf ce fichier");
            AllTabExceptThis.BackColor = MainForm.colorNoir;
            AllTabExceptThis.ForeColor = MainForm.colorBlanche;
            var OpenFileInEx = new ToolStripMenuItem("Ouvrir le répertoire du fichier en cours dans l'explorateur");
            OpenFileInEx.BackColor = MainForm.colorNoir2;
            OpenFileInEx.ForeColor = MainForm.colorBlanche;
            Items.AddRange(new ToolStripItem[] { closeTab, AllTabExceptThis, OpenFileInEx });

            HandleCreated += (s, e) =>
            {
                _form =SourceControl.FindForm() as MainForm;
            };

            closeTab.Click += (s, e) => 
            {
                _form.Session.Files.Remove(_form.CurrentFile);
                var SelectedTab = _form.MainTabControl.SelectedTab;

                //Supprime l'onglet puis récupère le nouvel index(fichier de gauche) et l'active
                if(_form.MainTabControl.TabCount > 1) 
                {
                    _form.MainTabControl.TabPages.Remove(SelectedTab);
                    var newIndex = _form.MainTabControl.TabCount - 1;

                    _form.MainTabControl.SelectedIndex = newIndex;
                    _form.CurrentFile = _form.Session.Files[newIndex];
                }
                //Losqu'un seul onglet est ouvert, le contenu est auto° vidé et "Sans Titre 1" s'affiche
                else 
                {
                    var fileName = "Sans Titre 1";
                    var file = new TextFile(fileName);

                    _form.CurrentFile = file;
                    _form.CurrentRtb.Clear();
                    _form.MainTabControl.SelectedTab.Text = file.FileName;
                    _form.Session.Files.Add(file);
                    _form.Text = "Sans Titre 1 - Stord.NET";
                }
            };

            AllTabExceptThis.Click += (s, e) =>  
            {
                var filesToDelete = new List<TextFile>();

                if (_form.MainTabControl.TabCount > 1) 
                {
                    TabPage selectedTab = _form.MainTabControl.SelectedTab;
                      
                    //Suppresiion des onglets qui ne correspondent pas à l'onglet sélectionné
                    foreach(TabPage tabpage in _form.MainTabControl.TabPages) 
                    {
                        if(tabpage != selectedTab) 
                        {
                            _form.MainTabControl.TabPages.Remove(tabpage);
                        }
                    }
                        //Compare les fichiers de la session et les fichiers à supp° et supp° les fichiers identiques
                    foreach(var file in _form.Session.Files) 
                    {
                        if(file != _form.CurrentFile)
                        {
                            filesToDelete.Add(file);
                        }
                    }
                    _form.Session.Files = _form.Session.Files.Except(filesToDelete).ToList();
                }
            };

            OpenFileInEx.Click += (s, e) =>
            {
                var arguments = $"/Select, \"{_form.CurrentFile.FileName}\"";
                Process.Start("explorer.exe", arguments);
            };
        }
    }
}    
