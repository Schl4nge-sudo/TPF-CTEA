using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WiW
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Form1 form1, string @direccion, string titulo, System.Drawing.Color backColor)
        {
            InitializeComponent();
            this.Parent = form1;
            Image file = Image.FromFile(@direccion);
            pictureBox1.Image = file;
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            label1.Text = titulo;
            label1.ForeColor = backColor;
            caras.BackColor= backColor;    
            barra.BackColor= backColor;


        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Parent.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Parent.Show();

        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();


        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void barra_MouseDown(object sender, MouseEventArgs e)
        {


                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);

            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Parent.Dispose();
            tpfinal.Backend.reset();
            this.Close();
            Inicio inicio = new Inicio();
            inicio.Show();
            
         
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void caras_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
