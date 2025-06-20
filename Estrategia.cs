
using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.Logging;
using System.Security.Policy;
using tp1;
using tp2;
using System.Text;

namespace tpfinal
{

    class Estrategia
    {

        public ArbolBinario<DecisionData> CrearArbol(Clasificador clasificador)
        {
            // Caso base 
            if (clasificador.crearHoja())
            {
                return new ArbolBinario<DecisionData>(new DecisionData(clasificador.obtenerDatoHoja()));
            }

            ArbolBinario<DecisionData> arbol = new ArbolBinario<DecisionData>(new DecisionData(clasificador.obtenerPregunta()));
            arbol.agregarHijoIzquierdo(CrearArbol(clasificador.obtenerClasificadorIzquierdo()));
            arbol.agregarHijoDerecho(CrearArbol(clasificador.obtenerClasificadorDerecho()));

            return arbol;
        }

        // Retorna un texto con todas las posibles predicciones que puede calcular el árbol de decisión del sistema.
        public string Consulta1(ArbolBinario<DecisionData> arbol)
        {
            if (arbol == null) // Caso base, si el arbol es nulo (por ejemplo, si un hijo no existe), devuelve una cadena vacia
            {
                return "";
            }
            string resultado = "";

            if (arbol.getHijoIzquierdo() != null)
            {
                resultado += Consulta1(arbol.getHijoIzquierdo());
            }

            if (arbol.getHijoDerecho() != null)
            {
                resultado += Consulta1(arbol.getHijoDerecho());
            }

            if (arbol.esHoja())
            {
                resultado += arbol.getDatoRaiz().ToString() + "\n";
            }

            return resultado;
        }

        // Retorna un texto que contiene todos los caminos hasta cada predicción
        public string Consulta2(ArbolBinario<DecisionData> arbol)
        {
            int contador = 0;
            return "Caminos:\n" + Recorrer(arbol, "", ref contador);
        }

        //Retorna un texto que contiene los datos almacenados en los nodos del árbol diferenciados por el nivel en que se encuentran.
        public string Consulta3(ArbolBinario<DecisionData> arbol)
        {
            if (arbol == null) return "";

            // El uso de StringBuilder tiene como objetivo mejorar el rendimiento al concatenar muchas cadenas en un bucle. 
            var sb = new StringBuilder();
            Cola<ArbolBinario<DecisionData>> cola = new Cola <ArbolBinario<DecisionData>>();
            cola.encolar(arbol);
            int nivel = 0;

            while (cola.cantidadElementos() > 0)
            {
                sb.AppendLine($"Nivel {nivel}:");
                int contador = cola.cantidadElementos();

                for (int i = 0; i < contador; i++)
                {
                    var nodo = cola.desencolar();
                    sb.AppendLine(nodo.getDatoRaiz().ToString());
                    if (nodo.getHijoIzquierdo() != null) cola.encolar(nodo.getHijoIzquierdo());
                    if (nodo.getHijoDerecho() != null) cola.encolar(nodo.getHijoDerecho());
                }
                nivel++;
                sb.AppendLine();
            }

            return sb.ToString().TrimEnd();
        }

        // Construimos recursivamente cada ruta de decisión y la numeramos
        private string Recorrer(ArbolBinario<DecisionData> nodo, string camino, ref int contador)
        {
            if (nodo == null)
                return "";

            if (nodo.esHoja())
            {
                contador++;
                // ToString() en DecisionData imprimirá todas las predicciones con sus %
                string textoPreds = nodo.getDatoRaiz().ToString();

                return $"Opción N° {contador}: {camino}El personaje es: {textoPreds}\n";
            }

            string resultado = "";

            if (nodo.getHijoIzquierdo() != null)
            {
                resultado += Recorrer(nodo.getHijoIzquierdo(), camino + $"{nodo.getDatoRaiz()} >> ", ref contador);
            }

            if (nodo.getHijoDerecho() != null)
            {
                resultado += Recorrer(nodo.getHijoDerecho(), camino + $"{nodo.getDatoRaiz()} >> ", ref contador);
            }

            return resultado;
        }
    }
}