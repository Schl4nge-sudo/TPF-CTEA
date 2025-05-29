using System;
using System.Collections.Generic;

namespace tpfinal
{
	public class Pregunta
	{
		private int columna;
		private string valor;
		private string encabezado;
		private string texto;

		public string Texto
		{
			get
			{
				return texto;
			}
			set
			{
				{
					texto = value;
				}
			}
		}

		public string Valor
		{
			get
			{
				return valor;
			}
			set
			{
				{
					valor = value;
				}
			}
		}

		public Pregunta(int columna, string valor, string encabezado)
		{
			this.columna=columna;
			this.valor=valor;
			this.encabezado = encabezado;
			this.texto = String.Format(encabezado, valor);
		}
		
		public bool coincide(IList<string> ejemplo){
			// Compare the feature value in a parameter to the
			// feature value in this question.
			string val = ejemplo[this.columna];
			double vals;
			if (Double.TryParse(val, out vals))
				return Double.Parse(val) >= Double.Parse(this.valor);
			else
				return val.Equals(this.valor);
		}

		public bool coincide(ConjuntoDeDatos dataset, int fila){
			// Compare the feature value in a parameter to the
			// feature value in this question.
			string val = dataset.obtenerFila(fila)[this.columna];
			double vals;
			if (Double.TryParse(val, out vals))
				return Double.Parse(val) >= Double.Parse(this.valor);
			else
				return val.Equals(this.valor);
		}

		public bool coincide(string val)
		{
			// Compare the feature value in a parameter to the
			// feature value in this question.
			double vals;
			if (Double.TryParse(val, out vals))
				return Double.Parse(val) >= Double.Parse(this.valor);
			else
				return val.Equals(this.valor);
		}

		public override string ToString(){
			double vals;
			string condition = "==";
			if (Double.TryParse(this.valor, out vals))
				condition = ">=";
			
			return (this.encabezado+ " "+ condition +" " + this.valor) + "?";

		}

		public string TextoParaUsuario()
		{
			string result;
			if (encabezado.StartsWith("¿{0}"))
            {
				result = encabezado.Substring(5);
            }
            else
            {
				return String.Format(encabezado, valor);
			}

			return "¿"+char.ToUpper(result[0]) + result.Substring(1);

		}

		
	}
}
