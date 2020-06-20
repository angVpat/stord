using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WindowsForms_projet.Controls;
using WindowsForms_projet.Objects;

namespace WindowsForms_projet
{
    //Constructeur 
    public partial class MainForm : Form
    {
        public RichTextBox CurrentRtb;
        public TabControl MainTabControl;
        public TextFile CurrentFile;
        public Session Session;
        public static Color colorNoir= Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
        public static Color colorNoir2 = Color.FromArgb(30, 30, 30);
        public static Color colorBlanche= Color.FromArgb(255, 255, 255);

        public MainForm()
        {
            InitializeComponent();
            

            var menuStrip = new MainMenuStrip();
            MainTabControl = new MainTabControl();
            //MainTabControl.Padding = new Point(12, 12);
            BackColor = colorNoir;
            ForeColor = colorBlanche;
            //MainTabControl.Appearance = TabAppearance.Buttons;



            Controls.AddRange(new Control[] { MainTabControl, menuStrip });

            InitializeFile();

        }
        private async void InitializeFile()
        {
            Session = await Session.Load();
            
            if (Session.Files.Count == 0)
            {
                var file = new TextFile("Sans Titre");
                MainTabControl.TabPages.Add(file.SafeFileName);
                var rtb = new CustomRichTextBox();
                var tabPages = MainTabControl.TabPages[0];
                tabPages.Controls.Add(rtb);
                tabPages.BorderStyle = BorderStyle.None;
                rtb.Select();
                Session.Files.Add(file);
                CurrentFile = file;
                CurrentRtb = rtb;
            }
            else
            {
                var activeIndex = Session.ActiveIndex;

                foreach (var file in Session.Files)
                {
                    if (File.Exists(file.FileName) || File.Exists(file.BackUpFileName))
                    {
                        var rtb = new CustomRichTextBox();
                        var tabCount = MainTabControl.TabCount;

                        MainTabControl.TabPages.Add(file.SafeFileName);
                        MainTabControl.TabPages[tabCount].Controls.Add(rtb);

                        rtb.Text = file.Contents;
                    }
                }

                CurrentFile = Session.Files[activeIndex];
                CurrentRtb = (CustomRichTextBox)MainTabControl.TabPages[activeIndex].Controls.Find("RtbTextFileContents", true).First();
                CurrentRtb.Select();

                MainTabControl.SelectedIndex = activeIndex;
                Text = $"{CurrentFile.FileName} - Stord";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Session.ActiveIndex = MainTabControl.SelectedIndex;
            Session.Save();

            foreach(var file in Session.Files)
            {
                var fileIndex = Session.Files.IndexOf(file);
                var rtb = MainTabControl.TabPages[fileIndex].Controls.Find("RtbTextFileContents",true).First();
                if(file.FileName.StartsWith("Sans Titre"))
                {
                    file.Contents=rtb.Text;
                    Session.BackupFile(file);
                }
            }

        }

    }
}
