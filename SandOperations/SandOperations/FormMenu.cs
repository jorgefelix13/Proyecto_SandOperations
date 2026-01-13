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
    public partial class FormMenu : Form
    {
        string rol;
        public FormMenu()  
        {
            InitializeComponent();
            
        }

        public FormMenu(string rol)
        {
            InitializeComponent();
            this.rol = rol;

            if (rol == "administrador")
            {
               // textBox1.Visible = false; // Ocultamos el control
            }
            else
            {
                //textBox1.Visible = true;
            }
        }

    }
}
