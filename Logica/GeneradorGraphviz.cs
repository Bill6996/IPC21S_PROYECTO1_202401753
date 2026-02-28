using IPC2_PROYECTO1_202401753.Modelos;
using System.Diagnostics;

namespace IPC2_PROYECTO1_202401753.Logica
{
    public class GeneradorGraphviz
    {
        public void GenerarImagen(Rejilla rejilla, int periodo, string carpetaSalida)
        {
            try
            {
                if (!Directory.Exists(carpetaSalida))
                    Directory.CreateDirectory(carpetaSalida);

                string rutaDot = Path.Combine(carpetaSalida, $"periodo_{periodo}.dot");
                string rutaPng = Path.Combine(carpetaSalida, $"periodo_{periodo}.png");

                using (StreamWriter sw = new StreamWriter(rutaDot))
                {
                    sw.WriteLine("digraph rejilla {");
                    sw.WriteLine("    graph [splines=false, nodesep=0.1, ranksep=0.1]");
                    sw.WriteLine("    node [shape=square, width=0.3, height=0.3, fixedsize=true, fontsize=1, label=\"\"]");
                    sw.WriteLine("    edge [style=invis]");
                    sw.WriteLine($"    labelloc=top");
                    sw.WriteLine($"    label=\"Periodo: {periodo} | Contagiadas: {rejilla.ContarContagiadas()} | Sanas: {rejilla.ContarSanas()}\"");

                    // Nodos con color
                    for (int f = 1; f <= rejilla.Tamanio; f++)
                    {
                        for (int c = 1; c <= rejilla.Tamanio; c++)
                        {
                            var celda = rejilla.ObtenerCelda(f, c);
                            string color = celda.EstaContagiada ? "blue" : "white";
                            sw.WriteLine($"    c{f}_{c} [style=filled, fillcolor={color}]");
                        }
                    }

                    // Conexiones verticales para formar columnas
                    for (int f = 1; f < rejilla.Tamanio; f++)
                    {
                        for (int c = 1; c <= rejilla.Tamanio; c++)
                        {
                            sw.WriteLine($"    c{f}_{c} -> c{f + 1}_{c}");
                        }
                    }

                    // Rank same para alinear filas horizontalmente
                    for (int f = 1; f <= rejilla.Tamanio; f++)
                    {
                        sw.Write("    {rank=same;");
                        for (int c = 1; c <= rejilla.Tamanio; c++)
                        {
                            sw.Write($" c{f}_{c};");
                        }
                        sw.WriteLine("}");
                    }

                    // Conexiones horizontales invisibles para ordenar columnas
                    for (int f = 1; f <= rejilla.Tamanio; f++)
                    {
                        for (int c = 1; c < rejilla.Tamanio; c++)
                        {
                            sw.WriteLine($"    c{f}_{c} -> c{f}_{c + 1} [style=invis]");
                        }
                    }

                    sw.WriteLine("}");
                }

                Process proceso = new Process();
                proceso.StartInfo.FileName = @"C:\Program Files\Graphviz\bin\dot.exe";
                proceso.StartInfo.Arguments = $"-Tpng \"{rutaDot}\" -o \"{rutaPng}\"";
                proceso.StartInfo.UseShellExecute = false;
                proceso.StartInfo.CreateNoWindow = true;
                proceso.Start();
                proceso.WaitForExit();

                Console.WriteLine($"✅ Imagen generada: {rutaPng}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al generar imagen: {ex.Message}");
            }
        }
    }
}