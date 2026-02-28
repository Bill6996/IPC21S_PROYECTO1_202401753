using System.Xml;
using IPC2_PROYECTO1_202401753.Estructuras;
using IPC2_PROYECTO1_202401753.Modelos;

namespace IPC2_PROYECTO1_202401753.Archivos
{
    public class EscritorXML
    {
        public void GenerarSalida(ListaEnlazada<Paciente> pacientes, string rutaSalida)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                // Nodo raíz
                XmlElement raiz = doc.CreateElement("pacientes");
                doc.AppendChild(raiz);

                Nodo<Paciente> actual = pacientes.ObtenerCabeza();
                while (actual != null)
                {
                    Paciente p = actual.Dato;
                    XmlElement nodoPaciente = doc.CreateElement("paciente");

                    // Datos personales
                    XmlElement datosPer = doc.CreateElement("datospersonales");
                    XmlElement nombre = doc.CreateElement("nombre");
                    nombre.InnerText = p.Nombre;
                    XmlElement edad = doc.CreateElement("edad");
                    edad.InnerText = p.Edad.ToString();
                    datosPer.AppendChild(nombre);
                    datosPer.AppendChild(edad);
                    nodoPaciente.AppendChild(datosPer);

                    // Periodos
                    XmlElement periodos = doc.CreateElement("periodos");
                    periodos.InnerText = p.Periodos.ToString();
                    nodoPaciente.AppendChild(periodos);

                    // M
                    XmlElement m = doc.CreateElement("m");
                    m.InnerText = p.Rejilla.Tamanio.ToString();
                    nodoPaciente.AppendChild(m);

                    // Resultado
                    XmlElement resultado = doc.CreateElement("resultado");
                    resultado.InnerText = p.Resultado;
                    nodoPaciente.AppendChild(resultado);

                    // N (si aplica)
                    if (p.N > 0)
                    {
                        XmlElement n = doc.CreateElement("n");
                        n.InnerText = p.N.ToString();
                        nodoPaciente.AppendChild(n);
                    }

                    // N1 (si aplica)
                    if (p.N1 > 0)
                    {
                        XmlElement n1 = doc.CreateElement("n1");
                        n1.InnerText = p.N1.ToString();
                        nodoPaciente.AppendChild(n1);
                    }

                    raiz.AppendChild(nodoPaciente);
                    actual = actual.Siguiente;
                }

                doc.Save(rutaSalida);
                Console.WriteLine($"✅ Archivo de salida generado en: {rutaSalida}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al generar el archivo: {ex.Message}");
            }
        }
    }
}