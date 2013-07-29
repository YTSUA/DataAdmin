using System;
using System.Windows.Forms;
using DataAdmin.Forms;

namespace DataAdmin
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            

            Application.Run(new FormMain());


        }
    }
}
