
namespace IPC2_PROYECTO1_202401753.Estructuras
{
    public class ListaEnlazada<T>
    {
        private Nodo<T> cabeza;
        private int tamanio;

        public ListaEnlazada()
        {
            cabeza = null;
            tamanio = 0;
        }

        // Agregar al final de la lista
        public void Agregar(T dato)
        {
            Nodo<T> nuevo = new Nodo<T>(dato);
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo<T> actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
            tamanio++;
        }

        // Obtener elemento por índice
        public T Obtener(int indice)
        {
            if (indice < 0 || indice >= tamanio)
                throw new IndexOutOfRangeException("Índice fuera de rango");

            Nodo<T> actual = cabeza;
            for (int i = 0; i < indice; i++)
            {
                actual = actual.Siguiente;
            }
            return actual.Dato;
        }

        // Obtener el tamaño
        public int Tamanio()
        {
            return tamanio;
        }

        // Verificar si está vacía
        public bool EstaVacia()
        {
            return cabeza == null;
        }

        // Limpiar la lista
        public void Limpiar()
        {
            cabeza = null;
            tamanio = 0;
        }

        // Obtener la cabeza (para recorrer manualmente)
        public Nodo<T> ObtenerCabeza()
        {
            return cabeza;
        }
    }
}