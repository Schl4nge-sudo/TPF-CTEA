using tp2;
using tpfinal;
using System.Runtime.InteropServices;



namespace WiW
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            Backend.initilize();

            InitializeComponent();

            LocalInit();

		}

        private void LocalInit()
        {
            this.txtPregunta.Text = Backend.getPregunta();

            System.Windows.Forms.PictureBox ptboxcara;
            string direccion;
            string nombre;


            int x = 0;
            int y = 0;
            int i = 0;
            foreach (IList<string> item in Backend.getFilas())
            {
                nombre = item[item.Count - 1];
               // nombre = nombre.Equals(Backend.Nombre_seleccionado) ? "Sel_" + nombre : nombre; 
                ptboxcara = new System.Windows.Forms.PictureBox();
                direccion = Utils.get_path_img(nombre);

                Image file = Image.FromFile(@direccion);
                if (null != file)
                {
                    ptboxcara.Cursor = System.Windows.Forms.Cursors.Hand;
                    ptboxcara.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
                    ptboxcara.Image = file;
                    ptboxcara.Location = this.pictureBox2.Location = new System.Drawing.Point(x * Utils.w + 10, y * Utils.h + 10);
                    ptboxcara.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
                    ptboxcara.Name = nombre;
                    ptboxcara.Click += new EventHandler(picOneFaceUpA_Click);
                    caras.Controls.Add(ptboxcara);
                }
                i++;
                y = i / 6;
                x = i % 6;

            }
            
            foreach (var item in tpfinal.Backend.Pregunta_usuario)
            {
                this.comboBox1.Items.Add(item.TextoParaUsuario());
            }
            this.comboBox1.SelectedIndex = 0;
            this.comboBox1.Enabled = false;
            this.button7.Enabled = false;
            this.button8.Enabled = false;


        }
        void picOneFaceUpA_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.PictureBox pictureBox = (System.Windows.Forms.PictureBox) sender;

            if (pictureBox.Name.StartsWith("No_"))
            {
                pictureBox.Name = pictureBox.Name.Substring(3);
            }
            else
            {
                pictureBox.Name = "No_" + pictureBox.Name;
            }

            pictureBox.Image = Image.FromFile(Utils.get_path_img(pictureBox.Name));

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void checks(bool valor)
        {
            Backend.evaluar_respuesta(valor);
            
           if (Backend.final())
            {
                foreach (var item in Backend.getPredicciones())
                {
                    string nombre = item.Key.ToString(); 
                    string path = Utils.get_path_img(nombre);
                    Form2 resultado = new Form2(this, path, "Su eleccion fue....", System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200))))));
                    resultado.Show();
                }
                Backend.restart();
                this.Hide();

            }
            else
            {
                this.txtPregunta.Text = Backend.getPregunta();
                this.comboBox1.Enabled = true;
                this.button7.Enabled = true;
                this.button8.Enabled = true;
                this.pictureBox2.Visible = true;
                this.pictureBoxSi.Visible = false;
                this.pictureBoxNo.Visible = false;
                this.btnNo.Enabled = false;
                this.button1.Enabled = false; 
                this.txtPregunta.Enabled = false;
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.checks(true);

        }
        private void btnNo_Click(object sender, EventArgs e)
        {
            this.checks(false);
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

        private void btn_consulta1_Click(object sender, EventArgs e)
        {
            string resultado = Backend.todasLasPredicciones();
            this.mostrarConsulta(resultado);

        }

        private void btn_consulta2_Click(object sender, EventArgs e)
        {
            string resultado = Backend.caminoAPrediccion();
            this.mostrarConsulta(resultado);
        }

        private void btn_consulta3_Click(object sender, EventArgs e)
        {
            string resultado = Backend.aProfundidad();
            this.mostrarConsulta(resultado);
        }

        private void mostrarConsulta(string resultado)
        {
            Consultas consulta = new Consultas(this, resultado);
            consulta.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.comboBox1.Enabled = false;
            this.button7.Enabled = false;
            this.button8.Enabled = false;
            this.btnNo.Enabled = true;
            this.button1.Enabled = true;
            this.txtPregunta.Enabled = true;

            string ans_sel = tpfinal.Backend.Respuesta_usuario[this.comboBox1.SelectedIndex];
            this.pictureBoxSi.Visible = false;
            this.pictureBoxNo.Visible = false;
            this.pictureBox2.Visible = false;
            if (ans_sel.Equals("si"))
            {
                this.pictureBoxSi.Visible = true;
            }
            else
            {
                this.pictureBoxNo.Visible = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FormUser fmrusr = new FormUser(this, false);
            fmrusr.Show();
            this.Hide();
        }
    }
}