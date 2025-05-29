using System;
using System.Collections.Generic;

namespace tpfinal
{
	
	public class Clasificador
	{
		
		private Dictionary<Pregunta, double> resultados;
		private List<Pregunta> preguntas;
		private double ganancia;
		private Pregunta pregunta;
		private IList<IList<string>> filas;
		private IList<string> encabezados;
		private IList<IList<IList<string>>> datosDeHijos;
		
		public Clasificador(ConjuntoDeDatos dataset)
		{
			this.filas = dataset.Filas;
			this.encabezados = dataset.Encabezados;
			resultados = Particionador.find_best_split(filas, encabezados);
			preguntas = new List<Pregunta>(resultados.Keys);
			pregunta = preguntas[0];
			ganancia = resultados[pregunta];
			datosDeHijos = Particionador.partition(filas, pregunta);
		}
		
		public bool crearHoja()
		{
			return(ganancia == 0.0);
		}
		
		public Dictionary<string, int> obtenerDatoHoja()
		{
			return Particionador.class_counts(filas);
		}
		
		public Pregunta obtenerPregunta()
		{
			return pregunta;
		}
		
		public ConjuntoDeDatos obtenerDatosIzquierdo()
		{
			return new ConjuntoDeDatos(datosDeHijos[0], this.encabezados);
		}
		
		public ConjuntoDeDatos obtenerDatosDerecho()
		{
			return new ConjuntoDeDatos(datosDeHijos[1], this.encabezados);
		}

		public Clasificador obtenerClasificadorIzquierdo()
		{
			return new Clasificador(this.obtenerDatosIzquierdo());
		}

		public Clasificador obtenerClasificadorDerecho()
		{
			return new Clasificador(this.obtenerDatosDerecho());
		}
	}
}
