using System.Drawing;
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
        public MainForm()
        {
            InitializeComponent();
            Session = new Session();

            var menuStrip = new MainMenuStrip();
            MainTabControl = new MainTabControl();
            //MainTabControl.Padding = new Point(12, 12);
            BackColor = Color.FromArgb(35, 37, 46);
            ForeColor = Color.FromArgb(255, 255, 255);
            //MainTabControl.Appearance = TabAppearance.Buttons;

            MainTabControl.BackColor = Color.FromArgb(35, 37, 46);
            MainTabControl.ForeColor = Color.FromArgb(255, 255, 255);

            Controls.AddRange(new Control[] { MainTabControl, menuStrip });

            InitializeFile();

        }
        private void InitializeFile()
        {
            if (Session.TextFiles.Count == 0)
            {
                var file = new TextFile("Sans Titre");
                MainTabControl.TabPages.Add(file.SafeFileName);
                var rtb = new CustomRichTextBox();
                var tabPages = MainTabControl.TabPages[0];
                tabPages.Controls.Add(rtb);
                tabPages.BorderStyle = BorderStyle.None;
                rtb.Select();
                Session.TextFiles.Add(file);
                CurrentFile = file;
                CurrentRtb = rtb;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            Session.Save();
        }

    }
}
