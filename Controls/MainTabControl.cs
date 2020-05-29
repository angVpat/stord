using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms_projet.Controls
{
    public class MainTabControl : TabControl
    {
        private const string NAME = "MainTabControl";
       
        public MainTabControl() {
            ContextMenuStrip = new TabControlContextMS();
            Name = NAME;
            ContextMenuStrip contextMenuStrip;
            Dock = DockStyle.Fill;
        }
    }
}
