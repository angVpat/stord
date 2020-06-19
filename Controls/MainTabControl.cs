using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace WindowsForms_projet.Controls
{
    public class MainTabControl : TabControl
    {
        private const string NAME = "MainTabControl";
        private ContextMenuStrip contextMenuStrip;
        private MainForm _form;
        public MainTabControl()
        { //Constructeur
            ContextMenuStrip = new TabControlContextMS();
            Name = NAME;
            ContextMenuStrip contextMenuStrip;
            BackColor = Color.FromArgb(35, 37, 46);
            ForeColor = Color.FromArgb(255, 255, 255);
            Dock = DockStyle.Fill;

            HandleCreated += (s, e) => 
            {
                _form = FindForm() as MainForm;
            }; 

            SelectedIndexChanged += (s, e) => 
            {
                _form.CurrentFile = _form.Session.Files[SelectedIndex];
                _form.CurrentRtb = (CustomRichTextBox)_form.MainTabControl.TabPages[SelectedIndex].Controls.Find("RtbTextFileContents", true).First();
                _form.Text = $"{_form.CurrentFile.FileName} - Stord.NET";
            };
            //Permet aux clics droits et gauches de sélectionner un fichier ouvert
            MouseUp += (s, e) => 
            {
                if(e.Button == MouseButtons.Right) 
                {
                    for(int i=0; i< TabCount; i++) 
                    {
                        var rect = GetTabRect(i);
                        if (rect.Contains(e.Location))
                        {
                            SelectedIndex = i;
                            break;
                        }
                    };
                };
            };
        }
    }
}
