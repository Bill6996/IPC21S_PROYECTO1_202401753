# IPC2_PROYECTO1_202401753

# IPC2_Proyecto1_202401753

##  Laboratorio Epidemiológico de Guatemala

### Descripción
Este proyecto simula el comportamiento de enfermedades en tejidos celulares humanos
utilizando un modelo basado en el Juego de la Vida de Conway. El sistema analiza 
rejillas cuadradas de células sanas y contagiadas, aplicando reglas de contagio 
período a período para determinar la gravedad de una enfermedad.

### ¿Cómo funciona?
El sistema carga rejillas de células desde un archivo XML donde cada celda puede 
estar sana (0) o contagiada (1). En cada período se aplican las siguientes reglas:

- **Regla 1:** Una célula contagiada sobrevive si tiene exactamente 2 o 3 vecinos 
contagiados, de lo contrario sana.
- **Regla 2:** Una célula sana se contagia si tiene exactamente 3 vecinos contagiados.

### Resultados posibles
-  **LEVE:** La enfermedad desaparece antes de completar los períodos configurados.
-  **GRAVE:** El patrón de células se repite cada N períodos (N > 1).
-  **MORTAL:** El patrón se repite cada 1 período, la enfermedad es incurable.

### Funcionalidades
- Cargar pacientes desde archivo XML de entrada
- Seleccionar un paciente para su análisis
- Ejecutar la simulación período a período con visualización gráfica
- Ejecutar todos los períodos automáticamente
- Visualizar la rejilla en cada período mediante Graphviz
- Generar archivo XML de salida con los resultados
- Limpiar la memoria del sistema

### Tecnologías utilizadas
- **Lenguaje:** C#
- **Framework:** .NET 10
- **Visualización:** Graphviz
- **Datos:** Archivos XML
- **IDE:** Visual Studio 2022

### Estructuras de datos implementadas
- **Nodo:** Clase genérica base para la lista enlazada
- **ListaEnlazada:** Implementación propia sin usar estructuras nativas de C#

### Autor
**Carnet:** 202401753  
**Universidad:** San Carlos de Guatemala  
**Facultad:** Ingeniería en Ciencias y Sistemas
**Curso:** Introducción a la Programación y Computación 2
