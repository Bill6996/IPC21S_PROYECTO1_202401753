using IPC2_PROYECTO1_202401753.Estructuras;


namespace IPC2_PROYECTO1_202401753.Modelos
{
    public class Rejilla
    {
        public int Tamanio { get; set; }
        private ListaEnlazada<Celda> celdas;

        public Rejilla(int tamanio)
        {
            Tamanio = tamanio;
            celdas = new ListaEnlazada<Celda>();

            // Inicializar toda la rejilla con células sanas
            for (int i = 1; i <= tamanio; i++)
            {
                for (int j = 1; j <= tamanio; j++)
                {
                    celdas.Agregar(new Celda(i, j, false));
                }
            }
        }

        // Obtener una celda por fila y columna
        public Celda ObtenerCelda(int fila, int columna)
        {
            Nodo<Celda> actual = celdas.ObtenerCabeza();
            while (actual != null)
            {
                if (actual.Dato.Fila == fila && actual.Dato.Columna == columna)
                    return actual.Dato;
                actual = actual.Siguiente;
            }
            return null;
        }

        // Marcar una celda como contagiada
        public void ContagiarCelda(int fila, int columna)
        {
            Celda celda = ObtenerCelda(fila, columna);
            if (celda != null)
                celda.EstaContagiada = true;
        }

        // Contar vecinos contagiados de una celda
        private int ContarVecinosContagiados(int fila, int columna)
        {
            int count = 0;
            for (int df = -1; df <= 1; df++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    if (df == 0 && dc == 0) continue; // saltar la celda misma
                    Celda vecino = ObtenerCelda(fila + df, columna + dc);
                    if (vecino != null && vecino.EstaContagiada)
                        count++;
                }
            }
            return count;
        }

        // Aplicar las reglas de Conway para un período
        public void EjecutarPeriodo()
        {
            // Primero calculamos el siguiente estado sin modificar el actual
            ListaEnlazada<bool> nuevoEstado = new ListaEnlazada<bool>();

            Nodo<Celda> actual = celdas.ObtenerCabeza();
            while (actual != null)
            {
                int vecinos = ContarVecinosContagiados(actual.Dato.Fila, actual.Dato.Columna);
                bool contagiadaActual = actual.Dato.EstaContagiada;
                bool nuevoValor;

                if (contagiadaActual)
                    // Regla 1: contagiada sobrevive si tiene 2 o 3 vecinos
                    nuevoValor = (vecinos == 2 || vecinos == 3);
                else
                    // Regla 2: sana se contagia si tiene exactamente 3 vecinos
                    nuevoValor = (vecinos == 3);

                nuevoEstado.Agregar(nuevoValor);
                actual = actual.Siguiente;
            }

            // Ahora aplicamos el nuevo estado
            actual = celdas.ObtenerCabeza();
            int indice = 0;
            while (actual != null)
            {
                actual.Dato.EstaContagiada = nuevoEstado.Obtener(indice);
                actual = actual.Siguiente;
                indice++;
            }
        }

        // Contar células contagiadas
        public int ContarContagiadas()
        {
            int count = 0;
            Nodo<Celda> actual = celdas.ObtenerCabeza();
            while (actual != null)
            {
                if (actual.Dato.EstaContagiada) count++;
                actual = actual.Siguiente;
            }
            return count;
        }

        // Contar células sanas
        public int ContarSanas()
        {
            return (Tamanio * Tamanio) - ContarContagiadas();
        }

        // Convertir el estado actual a string para comparar patrones
        public string ObtenerPatron()
        {
            string patron = "";
            Nodo<Celda> actual = celdas.ObtenerCabeza();
            while (actual != null)
            {
                patron += actual.Dato.EstaContagiada ? "1" : "0";
                actual = actual.Siguiente;
            }
            return patron;
        }

        // Clonar la rejilla completa
        public Rejilla Clonar()
        {
            Rejilla clon = new Rejilla(Tamanio);
            Nodo<Celda> actual = celdas.ObtenerCabeza();
            while (actual != null)
            {
                if (actual.Dato.EstaContagiada)
                    clon.ContagiarCelda(actual.Dato.Fila, actual.Dato.Columna);
                actual = actual.Siguiente;
            }
            return clon;
        }
    }
}