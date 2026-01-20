using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SandOperations
{
    public partial class FormSplash : Form
    {
        public FormSplash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Aumentamos la barra
            progressBar1.Increment(2); // Sube de 2 en 2

            // Cuando llega al final...
            if (progressBar1.Value >= 100)
            {
                timer1.Stop(); // Detenemos el reloj para que no siga contando

                this.DialogResult = DialogResult.OK; // Decimos "Todo salió bien"
                this.Close(); // Cerramos el Splash
            }
        }
    }
}
