namespace IPC2_PROYECTO1_202401753.Modelos
{
    public class Paciente
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int Periodos { get; set; }
        public Rejilla Rejilla { get; set; }

        // Resultados del análisis
        public string Resultado { get; set; }  // "leve", "grave", "mortal"
        public int N { get; set; }             // período en que se repite
        public int N1 { get; set; }            // período secundario

        public Paciente(string nombre, int edad, int periodos, int tamanioRejilla)
        {
            Nombre = nombre;
            Edad = edad;
            Periodos = periodos;
            Rejilla = new Rejilla(tamanioRejilla);
            Resultado = "";
            N = 0;
            N1 = 0;
        }
    }
}