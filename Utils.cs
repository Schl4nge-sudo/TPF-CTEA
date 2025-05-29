using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiW
{
    internal class Utils
    {
        public static string patron = System.IO.Directory.GetCurrentDirectory()+"\\datasets";
        public static string patron_imgs = "\\imgs\\{0}.png";
        public static string file_preguntas = "\\preguntas.csv";

       
        public static int w = 100;
        public static int h = 145;

        public static void init_patron()
        {
            patron = System.IO.Directory.GetCurrentDirectory() + "\\dataset";  
        }

        public static string get_path_img(string nombre) {
            return (patron + String.Format(Utils.patron_imgs, nombre));
        }

        public static string get_path_preguntas()
        {
            return patron + file_preguntas;
        }

        public static void set_patron(string patron_parm)
        {
            patron = patron_parm;
        }

    }
}
