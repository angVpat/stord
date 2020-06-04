using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsForms_projet.Controls;
using WindowsForms_projet.Objects;

namespace WindowsForms_projet
{
    //Constructeur 
    public partial class MainForm : Form
    {
        public static RichTextBox RichTextBox;
        public MainForm()
        {
            InitializeComponent();
            var menuStrip = new MainMenuStrip();
            var mTabControl = new MainTabControl();
            var RichTextBox = new CustomRichTextBox();
            Controls.AddRange(new Control[] { mTabControl, menuStrip});
            mTabControl.TabPages.Add("Onglet 1");
            mTabControl.TabPages[0].Controls.Add(RichTextBox);
            TextFile file = new TextFile("C:/test.txt");
        }
    }
}
