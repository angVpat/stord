using System.IO;

namespace WindowsForms_projet.Objects
{
    public class TextFile
    {
        /// <summary>
        /// constructeur de la class
        /// </summary>
        public TextFile()
        {

        }
        public TextFile(string fileName)
        {
            FileName = fileName;
            SafeFileName = Path.GetFileName(fileName);
        }

        //Chemin d'accès et nom du fichier
        public string FileName { get; set; }
        //Chemin d'accès et nom du fichier BackUp
        public string BackUpFileName { get; set; }
        //Nom et extension du fichier : Le nom du fichier n'inclut pas le ch d'accès
        public string SafeFileName { get; set; }
        //Nom et extension du fichier Backup: Le nom du fichier n'inclut pas le ch d'accès
        public string SafeBackUpFileName { get; set; }
        //Contenu du fichier
        public string Contents { get; set; } = string.Empty;

    }
}
