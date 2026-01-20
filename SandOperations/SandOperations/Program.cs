using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SandOperations
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //1. Creamos y mostramos el Splash primero
            FormSplash splash = new FormSplash();

            //ShowDialog hace que el codigo espere aqui hasta que el splash se cierre
            if(splash.ShowDialog() == DialogResult.OK)
            {
                //2. Si el splash termino (llego a 100), abrimos el login
                Application.Run(new FormLogin());
            }
        }
    }
}
