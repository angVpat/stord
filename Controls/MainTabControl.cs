using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms_projet.Controls
{
    public class MainTabControl : TabControl
    {
        private const string NAME = "MainTabControl";

        public MainTabControl()
        { //Constructeur
            ContextMenuStrip = new TabControlContextMS();
            Name = NAME;
            ContextMenuStrip contextMenuStrip;
            BackColor = Color.FromArgb(35, 37, 46);
            ForeColor = Color.FromArgb(255, 255, 255);
            Dock = DockStyle.Fill;
        }
    }
}
