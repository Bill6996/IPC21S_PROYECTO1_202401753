namespace IPC2_PROYECTO1_202401753.Modelos
{
    public class Celda
    {
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool EstaContagiada { get; set; }

        public Celda(int fila, int columna, bool estaContagiada)
        {
            Fila = fila;
            Columna = columna;
            EstaContagiada = estaContagiada;
        }

        // Copia la celda para no modificar la original al simular
        public Celda Clonar()
        {
            return new Celda(Fila, Columna, EstaContagiada);
        }

        // Convierte la celda a string para comparar patrones fácilmente
        public override string ToString()
        {
            return $"[{Fila},{Columna}:{(EstaContagiada ? "1" : "0")}]";
        }
    }
}