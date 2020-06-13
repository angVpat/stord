﻿using System;
using System.Windows.Forms;

namespace WindowsForms_projet
{
    static class Program
    {
        public static MainForm MainForm;
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm = new MainForm();
            Application.Run(MainForm);
        }
    }
}
