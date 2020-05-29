using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsForms_projet.Controls;

namespace WindowsForms_projet
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var menuStrip = new MainMenuStrip();
            var mTabControl = new MainTabControl();
            Controls.AddRange(new Control[] { mTabControl, menuStrip});
            mTabControl.TabPages.Add("Onglet 1");
        }
    }
}
