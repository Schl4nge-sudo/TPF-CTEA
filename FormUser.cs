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
    public partial class FormUser : Form
    {
        private string selected="";
        private bool inicio = false;
        private List<System.Windows.Forms.PictureBox> pics= new List<PictureBox>();
        public FormUser()
        {
            InitializeComponent();
        }

        public FormUser(Form1 form1, bool inicio)
        {
            InitializeComponent();
            this.Parent = form1;

            System.Windows.Forms.PictureBox ptboxcara;
            string nombre, direccion;


            int x = 0;
            int y = 0;
            int i = 0;
            int w = 70;
            int h = 105;

            foreach (IList<string> item in tpfinal.Backend.getFilas())
            {
                nombre = item[item.Count - 1];
                ptboxcara = new System.Windows.Forms.PictureBox();
                ptboxcara.Size = new System.Drawing.Size(w, h);
                direccion = Utils.get_path_img(nombre);

                Image file = Image.FromFile(@direccion);
                if (null != file)
                {
                    ptboxcara.Cursor = System.Windows.Forms.Cursors.Hand;
                    ptboxcara.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
                    ptboxcara.Image = file;
                    ptboxcara.Location = new System.Drawing.Point(x * w + 3, y * h + 3);
                    ptboxcara.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    ptboxcara.Name = nombre;
                    ptboxcara.Click += new EventHandler(picOneFaceUpA_Click);
                    answerPanel.Controls.Add(ptboxcara);
                    pics.Add(ptboxcara);
                }
                i++;
                y = i / 6;
                x = i % 6;

            }

            this.inicio = inicio;
        }
        void picOneFaceUpA_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.PictureBox pictureBox = (System.Windows.Forms.PictureBox)sender;

            whitedSelect();

            if (pictureBox.Name.StartsWith("Sel_"))
            {
                pictureBox.Name = pictureBox.Name.Substring(4);
                this.selected = "";
            }
            else
            {
                this.selected = pictureBox.Name;
                pictureBox.Name = "Sel_" + pictureBox.Name;

            }

            pictureBox.Image = Image.FromFile(Utils.get_path_img(pictureBox.Name));

        }

        private void whitedSelect()
        {
            foreach (var item in pics)
            {
                if (item.Name.StartsWith("Sel_"))
                {
                    item.Name = item.Name.Substring(4);
                    this.selected = "";
                }
                item.Image = Image.FromFile(Utils.get_path_img(item.Name));
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.inicio)
            {
                if (!selected.Equals(""))
                {
                    DialogResult dr = MessageBox.Show("Desea jugar con " + selected + "?", "Confirmacion", MessageBoxButtons.YesNo);
                    switch (dr)
                    {
                        case DialogResult.Yes:
                            this.Parent.pictureBox_sel.Image = Image.FromFile(Utils.get_path_img("Sel_"+selected));
                            this.Close();
                            this.Parent.Show();
                            break;
                        case DialogResult.No:
                            whitedSelect();
                            break;
                    }

                }else {
                    DialogResult dr = MessageBox.Show("Debe seleccionar un personaje !", "Aviso", MessageBoxButtons.OK);

                }
            }
            else{
                if (!selected.Equals(""))
                {

                    DialogResult dr = MessageBox.Show("Esta seguro de arriesgar con "+ selected + "?",
                          "Pregunta", MessageBoxButtons.YesNo);
                    switch (dr)
                    {
                        case DialogResult.Yes:
                            string nombre = tpfinal.Backend.Nombre_seleccionado;
                            string path = Utils.get_path_img(nombre);
                            Form2 resultado;
                            if (nombre.Equals(selected))
                            {
                                resultado = new Form2(this.Parent, path, "Correcto!!", System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(200)))), ((int)(((byte)(80))))));
                            }
                            else
                            {
                                resultado = new Form2(this.Parent, path, "Incorrecto!!", System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(0))))));
                            }
                            resultado.Show();
                            break;
                        case DialogResult.No:
                            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
                            this.Parent.pictureBox_sel.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_sel.Image")));
                            break;
                    }
                }else{
                    DialogResult dr = MessageBox.Show("Debe seleccionar un personaje !", "Aviso", MessageBoxButtons.OK);

                }


                this.Close();
                //this.Parent.Show();
            }
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

        private void caras_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
