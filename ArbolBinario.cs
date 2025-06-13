using System;
using tp1;

namespace tp2
{
	public class ArbolBinario<T>
	{
		
		private T dato;
		private ArbolBinario<T> hijoIzquierdo;
		private ArbolBinario<T> hijoDerecho;
	
		
		public ArbolBinario(T dato) {
			this.dato = dato;
		}
	
		public T getDatoRaiz() {
			return this.dato;
		}
	
		public ArbolBinario<T> getHijoIzquierdo() {
			return this.hijoIzquierdo;
		}
	
		public ArbolBinario<T> getHijoDerecho() {
			return this.hijoDerecho;
		}
	
		public void agregarHijoIzquierdo(ArbolBinario<T> hijo) {
			this.hijoIzquierdo=hijo;
		}
	
		public void agregarHijoDerecho(ArbolBinario<T> hijo) {
			this.hijoDerecho=hijo;
		}
	
		public void eliminarHijoIzquierdo() {
			this.hijoIzquierdo=null;
		}
	
		public void eliminarHijoDerecho() {
			this.hijoDerecho=null;
		}
	
		public bool esHoja() {
			return this.hijoIzquierdo==null && this.hijoDerecho==null;
		}
		
		public void inorden() {

            if (this.getHijoIzquierdo() != null) this.getHijoIzquierdo().inorden();

            Console.WriteLine(this.getDatoRaiz());

            if (this.getHijoDerecho() != null) this.getHijoIzquierdo().inorden();
        }
		
		public void preorden() {

			Console.WriteLine(this.getDatoRaiz());

			if (this.getHijoIzquierdo() != null) this.getHijoIzquierdo().preorden();
            if (this.getHijoDerecho() != null) this.getHijoIzquierdo().preorden();

        }

        public void postorden() {

            if (this.getHijoIzquierdo() != null) this.getHijoIzquierdo().postorden();
            if (this.getHijoDerecho() != null) this.getHijoIzquierdo().postorden();

            Console.WriteLine(this.getDatoRaiz());
        }

        public void recorridoPorNiveles()
		{
			Queue<ArbolBinario<T>> cola = new Queue<ArbolBinario<T>>();

			cola.Enqueue(this);

			while (cola.Count > 0)
			{
				ArbolBinario<T> actual = cola.Dequeue();
				Console.WriteLine(actual.getDatoRaiz());
				if (actual.getHijoIzquierdo() != null)
				{
					cola.Enqueue(actual.getHijoIzquierdo());
				}
				if (actual.getHijoDerecho() != null)
				{
					cola.Enqueue(actual.getHijoDerecho());
				}
			}
		}
	
		public int contarHojas() {

            Queue<ArbolBinario<T>> cola = new Queue<ArbolBinario<T>>();

            int contador = 0;

            // Comenzamos agregando la raíz del árbol a la cola
            cola.Enqueue(this);

            while (cola.Count > 0)
            {
                // Sacamos el nodo frontal de la cola
                ArbolBinario<T> actual = cola.Dequeue();

                // Verificar si el nodo es una hoja
                if (actual.getHijoIzquierdo() == null && actual.getHijoDerecho() == null)
                {
                    contador++; // Contar el nodo como hoja
                }
                else
                {
                    // Agregar los hijos a la cola si existen
                    if (actual.getHijoIzquierdo() != null)
                    {
                        cola.Enqueue(actual.getHijoIzquierdo());
                    }

                    if (actual.getHijoDerecho() != null)
                    {
                        cola.Enqueue(actual.getHijoDerecho());
                    }

                }
            }

            return contador;
        }

        public void recorridoEntreNiveles(int n, int m)
        {
            if (n > m || n < 0)
            {
                Console.WriteLine("Parámetros de nivel inválidos.");
                return;
            }

            Cola <(ArbolBinario<T> nodo, int nivel)> cola = new Cola<(ArbolBinario<T>, int)>();
            cola.encolar((this, 0));

            while (cola.cantidadElementos() > 0)
            {
                var (actual, nivel) = cola.desencolar();

                if (nivel >= n && nivel <= m)
                {
                    Console.WriteLine(actual.getDatoRaiz());
                }

                if (nivel < m)
                {
                    if (actual.getHijoIzquierdo() != null)
                        cola.encolar((actual.getHijoIzquierdo(), nivel + 1));
                    if (actual.getHijoDerecho() != null)
                        cola.encolar((actual.getHijoDerecho(), nivel + 1));
                }
            }
        }
    }
}
