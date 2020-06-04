using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms_projet.Objects
{
    public class session
    {
        private const string FILENAME = "session.xml";
        private static string _applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string _applicationPath = Path.Combine(_applicationDataPath, "Stord.NET");
        //chemin d'accès et nom du fichier xml
        public string FileName { get; } = Path.Combine(_applicationPath, FILENAME);
        public int ActiveIndex { get; set; } = 0;
        public List<TextFile> TextFiles { get; set; }

        public session()
        {
            TextFiles = new List<TextFile>();
        }
    }
}
