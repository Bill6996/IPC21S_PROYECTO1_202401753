using IPC2_PROYECTO1_202401753.Estructuras;
using IPC2_PROYECTO1_202401753.Modelos;

namespace IPC2_PROYECTO1_202401753.Logica
{
    public class Simulador
    {
        private Paciente paciente;
        private ListaEnlazada<string> historialPatrones;
        private string patronInicial;
        private int periodoActual;

        public int PeriodoActual => periodoActual;

        public Simulador(Paciente paciente)
        {
            this.paciente = paciente;
            historialPatrones = new ListaEnlazada<string>();
            patronInicial = paciente.Rejilla.ObtenerPatron();
            periodoActual = 0;

            // Guardamos el patrón inicial en el historial
            historialPatrones.Agregar(patronInicial);
        }

        // Ejecutar UN solo período y retorna true si encontró un ciclo
        public bool EjecutarUnPeriodo()
        {
            paciente.Rejilla.EjecutarPeriodo();
            periodoActual++;

            string patronActual = paciente.Rejilla.ObtenerPatron();

            // Si no hay células contagiadas, es leve
            if (paciente.Rejilla.ContarContagiadas() == 0)
            {
                paciente.Resultado = "leve";
                paciente.N = periodoActual;
                paciente.N1 = 0;
                return true;
            }

            // Buscar si el patrón actual ya existió antes
            int indicePeriodo = BuscarPatron(patronActual);

            if (indicePeriodo != -1)
            {
                int N1 = periodoActual - indicePeriodo;

                paciente.N = periodoActual;
                paciente.N1 = N1;

                if (N1 == 1)
                    paciente.Resultado = "mortal";
                else
                    paciente.Resultado = "grave";

                return true;
            }

            historialPatrones.Agregar(patronActual);

            if (periodoActual >= paciente.Periodos)
            {
                paciente.Resultado = "leve";
                return true;
            }

            return false;
        }

        // Ejecutar todos los períodos automáticamente
        public void EjecutarTodo()
        {
            bool terminado = false;
            while (!terminado)
            {
                terminado = EjecutarUnPeriodo();
            }
        }

        // Buscar un patrón en el historial, retorna el índice o -1
        private int BuscarPatron(string patron)
        {
            Nodo<string> actual = historialPatrones.ObtenerCabeza();
            int indice = 0;
            while (actual != null)
            {
                if (actual.Dato == patron)
                    return indice;
                actual = actual.Siguiente;
                indice++;
            }
            return -1;
        }

        // Mostrar estado actual en consola
        public void MostrarEstado()
        {
            Console.WriteLine($"\n📊 Período: {periodoActual}");
            Console.WriteLine($"🦠 Células contagiadas: {paciente.Rejilla.ContarContagiadas()}");
            Console.WriteLine($"💚 Células sanas: {paciente.Rejilla.ContarSanas()}");
            if (paciente.Resultado != "")
                Console.WriteLine($"📋 Resultado: {paciente.Resultado.ToUpper()} | N={paciente.N} | N1={paciente.N1}");
        }
    }
}