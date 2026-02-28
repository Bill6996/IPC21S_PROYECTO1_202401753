using System.Xml;
using IPC2_PROYECTO1_202401753.Estructuras;
using IPC2_PROYECTO1_202401753.Modelos;

namespace IPC2_PROYECTO1_202401753.Archivos
{
    public class LectorXML
    {
        public ListaEnlazada<Paciente> CargarPacientes(string rutaArchivo)
        {
            ListaEnlazada<Paciente> pacientes = new ListaEnlazada<Paciente>();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(rutaArchivo);

                XmlNodeList listaPacientes = doc.GetElementsByTagName("paciente");

                foreach (XmlNode nodo in listaPacientes)
                {
                    // Datos personales
                    string nombre = nodo["datospersonales"]["nombre"].InnerText;
                    int edad = int.Parse(nodo["datospersonales"]["edad"].InnerText);
                    int periodos = int.Parse(nodo["periodos"].InnerText);
                    int m = int.Parse(nodo["m"].InnerText);

                    // Crear paciente
                    Paciente paciente = new Paciente(nombre, edad, periodos, m);

                    // Cargar celdas contagiadas
                    XmlNode rejillaNode = nodo["rejilla"];
                    if (rejillaNode != null)
                    {
                        XmlNodeList celdas = rejillaNode.SelectNodes("celda");
                        foreach (XmlNode celda in celdas)
                        {
                            int fila = int.Parse(celda.Attributes["f"].Value);
                            int columna = int.Parse(celda.Attributes["c"].Value);
                            paciente.Rejilla.ContagiarCelda(fila, columna);
                        }
                    }

                    pacientes.Agregar(paciente);
                }

                Console.WriteLine($"✅ Se cargaron {pacientes.Tamanio()} pacientes correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al cargar el archivo: {ex.Message}");
            }

            return pacientes;
        }
    }
}