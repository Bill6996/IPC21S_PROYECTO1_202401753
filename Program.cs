using IPC2_PROYECTO1_202401753.Archivos;
using IPC2_PROYECTO1_202401753.Estructuras;
using IPC2_PROYECTO1_202401753.Logica;
using IPC2_PROYECTO1_202401753.Modelos;
using System.Diagnostics;


ListaEnlazada<Paciente> pacientes = new ListaEnlazada<Paciente>();
LectorXML lector = new LectorXML();
EscritorXML escritor = new EscritorXML();
GeneradorGraphviz graphviz = new GeneradorGraphviz();
Simulador simuladorActivo = null;
Paciente pacienteActivo = null;

bool salir = false;

while (!salir)
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════════════════╗");
    Console.WriteLine("║   LABORATORIO EPIDEMIOLÓGICO GUATEMALA   ║");
    Console.WriteLine("╠══════════════════════════════════════════╣");
    Console.WriteLine("║  1. Cargar archivo XML de entrada        ║");
    Console.WriteLine("║  2. Seleccionar paciente                 ║");
    Console.WriteLine("║  3. Ejecutar un período                  ║");
    Console.WriteLine("║  4. Ejecutar todos los períodos          ║");
    Console.WriteLine("║  5. Generar archivo XML de salida        ║");
    Console.WriteLine("║  6. Generar imagen Graphviz              ║"); 
    Console.WriteLine("║  7. Limpiar pacientes                    ║");
    Console.WriteLine("║  8. Salir                                ║");
    if (pacienteActivo != null)
        Console.WriteLine($"\n👤 Paciente activo: {pacienteActivo.Nombre}");

    Console.Write("\nElige una opción: ");
    string opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            Console.Clear();
            Console.Write("Ingresa la ruta del archivo XML: ");
            string ruta = Console.ReadLine();
            pacientes = lector.CargarPacientes(ruta);
            simuladorActivo = null;
            pacienteActivo = null;
            Console.ReadKey();
            break;

        case "2":
            Console.Clear();
            if (pacientes.EstaVacia())
            {
                Console.WriteLine("❌ No hay pacientes cargados. Carga un archivo XML primero.");
                Console.ReadKey();
                break;
            }

            Console.WriteLine("👥 Pacientes disponibles:");
            Nodo<Paciente> actual = pacientes.ObtenerCabeza();
            int i = 1;
            while (actual != null)
            {
                Console.WriteLine($"  {i}. {actual.Dato.Nombre} (Edad: {actual.Dato.Edad})");
                actual = actual.Siguiente;
                i++;
            }

            Console.Write("\nElige el número del paciente: ");
            if (int.TryParse(Console.ReadLine(), out int numPaciente) &&
                numPaciente >= 1 && numPaciente <= pacientes.Tamanio())
            {
                pacienteActivo = pacientes.Obtener(numPaciente - 1);
                simuladorActivo = new Simulador(pacienteActivo);
                Console.WriteLine($"✅ Paciente {pacienteActivo.Nombre} seleccionado.");
            }
            else
            {
                Console.WriteLine("❌ Número inválido.");
            }
            Console.ReadKey();
            break;

        case "3":
            Console.Clear();
            if (simuladorActivo == null)
            {
                Console.WriteLine("❌ Selecciona un paciente primero.");
                Console.ReadKey();
                break;
            }

            if (pacienteActivo.Resultado != "")
            {
                Console.WriteLine("⚠️ La simulación ya terminó para este paciente.");
                simuladorActivo.MostrarEstado();
                Console.ReadKey();
                break;
            }

            bool terminado = simuladorActivo.EjecutarUnPeriodo();
            simuladorActivo.MostrarEstado();

            // Generar imagen automáticamente
            string carpetaPeriodo = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "imagenes"
            );
            graphviz.GenerarImagen(
                pacienteActivo.Rejilla,
                simuladorActivo.PeriodoActual,
                carpetaPeriodo
            );

            // Abrir la imagen automáticamente
            Process.Start(new ProcessStartInfo()
            {
                FileName = Path.Combine(carpetaPeriodo, $"periodo_{simuladorActivo.PeriodoActual}.png"),
                UseShellExecute = true
            });

            if (terminado)
                Console.WriteLine("✅ Simulación completada.");

            Console.ReadKey();
            break;

        case "4":
            Console.Clear();
            if (simuladorActivo == null)
            {
                Console.WriteLine("❌ Selecciona un paciente primero.");
                Console.ReadKey();
                break;
            }

            if (pacienteActivo.Resultado != "")
            {
                Console.WriteLine("⚠️ La simulación ya terminó para este paciente.");
                simuladorActivo.MostrarEstado();
                Console.ReadKey();
                break;
            }

            Console.WriteLine("⏳ Ejecutando todos los períodos...");
            simuladorActivo.EjecutarTodo();
            simuladorActivo.MostrarEstado();
            Console.WriteLine("✅ Simulación completada.");
            Console.ReadKey();
            break;

        case "5":
            Console.Clear();
            if (pacientes.EstaVacia())
            {
                Console.WriteLine("❌ No hay pacientes cargados.");
                Console.ReadKey();
                break;
            }

            Console.Write("Ingresa la ruta del archivo XML de salida: ");
            string rutaSalida = Console.ReadLine();
            escritor.GenerarSalida(pacientes, rutaSalida);
            Console.ReadKey();
            break;

        case "6":
            Console.Clear();
            if (simuladorActivo == null)
            {
                Console.WriteLine("❌ Selecciona un paciente primero.");
                Console.ReadKey();
                break;
            }
            string carpeta = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "imagenes"
            );
            graphviz.GenerarImagen(
                pacienteActivo.Rejilla,
                simuladorActivo.PeriodoActual,
                carpeta
            );
            Console.WriteLine($"📁 Imágenes guardadas en: {carpeta}");
            Console.ReadKey();
            break;

        case "7":
            Console.Clear();
            pacientes.Limpiar();
            simuladorActivo = null;
            pacienteActivo = null;
            Console.WriteLine("✅ Memoria limpiada correctamente.");
            Console.ReadKey();
            break;



        case "8":
            salir = true;
            break;
    }
}

Console.WriteLine("👋 Hasta luego!");