public class Arreglos
{
    public int[] Numeros;

    public int PosicionActual;

    // Constructor con el que se crea el arreglo
    public Arreglos(int tamano)
    {
        Numeros = new int[tamano];
        PosicionActual = 0;
    }
    
    // Metodo para agregar un valor al arreglo
    public void AddValor(int numero)
    {
        Numeros[PosicionActual] = numero;
        PosicionActual++;
    }

    // Metodo para imprimir el arreglo
    public void Print()
    {
        for (int i = 0; i < Numeros.Length; i++)
        {
            Console.WriteLine($"{i} - {Numeros[i]}");
        }
    }
}