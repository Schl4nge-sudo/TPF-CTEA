using tp2;
using tpfinal;
using System.Runtime.InteropServices;



namespace WiW
{
    public partial class Inicio : Form
    {
        public Inicio()
        {

            
            InitializeComponent();

            pathDataSet.Text = Utils.patron;

        }


        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fch = new FolderBrowserDialog();
            if (fch.ShowDialog() == DialogResult.OK)
            {
                pathDataSet.Text = fch.SelectedPath;
                Utils.set_patron(pathDataSet.Text);
            }
            else
            {
                Utils.init_patron();
                pathDataSet.Text = Utils.patron;
            }
        }
        private void btnNo_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 juego = new Form1();
                FormUser select = new FormUser(juego, true);
                select.Show();
                this.Hide();
            }
            catch (Exception)
            {
                DialogResult dr = MessageBox.Show("Directorio invalido, Por favor seleccione otro...", "Aviso", MessageBoxButtons.OK);
            }
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

    }
}