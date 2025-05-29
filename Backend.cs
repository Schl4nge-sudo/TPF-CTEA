using System;
using tp2;
using tp1;
using System.Collections.Generic;

namespace tpfinal
{

	public class Backend
	{
		private static ArbolBinario<DecisionData> arbol, init_arbol;
		private static ConjuntoDeDatos datasetEntrenamiento;
		private static Estrategia estrategia= new Estrategia();
		private static List<Pregunta> pregunta_usuario = new List<Pregunta>();
		private static List<string> respuesta_usuario = new List<string>();
		private static string nombre_seleccionado;
		private static int pregunta_seleccionada;

        public static List<Pregunta> Pregunta_usuario { get => pregunta_usuario; set => pregunta_usuario = value; }
        public static List<string> Respuesta_usuario { get => respuesta_usuario; set => respuesta_usuario = value; }
        public static string Nombre_seleccionado { get => nombre_seleccionado; set => nombre_seleccionado = value; }

        public static void initilize ()
		{

			datasetEntrenamiento = new ConjuntoDeDatos();

			arbol = Backend.crear_arbol(datasetEntrenamiento);

			init_arbol = arbol;

			Random rnd = new Random();
			pregunta_seleccionada = rnd.Next(1, datasetEntrenamiento.cantidadFilas());

			nombre_seleccionado = datasetEntrenamiento.obtenerEtiquetaFila(pregunta_seleccionada);


			HashSet<string> set = new HashSet<string>();
			IList<IList<string>> rows = datasetEntrenamiento.Filas;
			IList<string> header = datasetEntrenamiento.Encabezados;
			int n_features = header.Count - 1;
			List<string> values;
			Pregunta question;
			for (int col = 0; col < n_features; col++)
			{
				values = new List<string>();
				for (int i = 0; i < rows.Count; i++)
				{
					if (!values.Contains(rows[i][col]))
					{
						values.Add(rows[i][col]);
					}
				}
				foreach (var val in values)
				{
					question = new Pregunta(col, val, header[col]);
					if (set.Add(question.TextoParaUsuario()))
					{
						Pregunta_usuario.Add(question);
                        if (val.Equals("si") || val.Equals("no"))
                        {
							Respuesta_usuario.Add(rows[pregunta_seleccionada][col]);
                        } else { 
							if (val.Equals(rows[pregunta_seleccionada][col]))
							{
								Respuesta_usuario.Add("si");

							}
							else
							{
								Respuesta_usuario.Add("no");
							}
						}

					}

				}

			}

		}
	
		public static string getPregunta()
        {
			return arbol==null?"Implementar":arbol.getDatoRaiz().Question.Texto;
		}

		public static bool final()
		{
			return arbol == null ? false : arbol.esHoja();
		}

		public static void restart()
		{
			arbol=init_arbol;
		}

		public static Dictionary<string, int> getPredicciones()
		{
			return arbol == null ? new Dictionary<string, int>(): arbol.getDatoRaiz().Predictions;
		}

		public static List<IList<string>> getFilas()
		{
			return datasetEntrenamiento.Filas;
		}


		//Imprimir todas las predicciones.
		public static string todasLasPredicciones()
		{
			return estrategia.Consulta1(arbol);
		}
		//Imprimir las preguntas con sus respuestas hasta llegar a una prediccion, pasando como parametro el conjunto de caracteristicas.

		public static string caminoAPrediccion()
		{
			return estrategia.Consulta2(arbol);
		}

		//Dada una profundidad imprimir las preguntas o predicciones ubicadas a dicha profundidad
		public static string aProfundidad()
		{
			return estrategia.Consulta3(arbol);
		}

		//Armar el arbol de decision con un conjunto de entrenamiento
		public static ArbolBinario<DecisionData> crear_arbol(ConjuntoDeDatos dataset)
		{

			return estrategia.CrearArbol(new Clasificador(dataset));

		}

		//Probar el arbol de decision con un conjunto de prueba
		public static void evaluar_respuesta(bool valor)
		{

			if (valor && arbol != null)
			{
				arbol = arbol.getHijoIzquierdo();
			}
			else if (arbol != null)
			{
				arbol = arbol.getHijoDerecho();
			}
		}

		public static void reset()
		{
			arbol = null;
			init_arbol = null;
			datasetEntrenamiento = null;
		    estrategia = new Estrategia();
		    pregunta_usuario = new List<Pregunta>();
		    respuesta_usuario = new List<string>();
		    nombre_seleccionado=null;
		    pregunta_seleccionada=0;
		}
	}
}