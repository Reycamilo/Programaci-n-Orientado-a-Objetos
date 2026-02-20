
// Solo numeros positivos

// Menu : 
// 1. Crear el Arreglo
// 2. Agregar un valor en el arreglo
// 3. Imprimir el arreglo
// 4. Salir.

// Hacer un do-while , preguntamos si quiere agregar mas numeros

class Program
{

    static Arreglos arreglo;

    // PROGRAMA PRINCIPAL
    static void Main(string[] args)
    {
        bool salir = false;
        

        do
        {
            MostrarMenu();
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    CrearArreglo();
                    break;
                case "2":
                    AgregarValor();
                    break;
                case "3":
                    Console.Clear();
                    arreglo.Print();
                    Console.WriteLine("Presione Enter...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "4":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opcion invalida. Presione Enter...");
                    Console.ReadKey();
                    break;
            }

        } while (!salir);

       


    }

    // Funcion para mostrar el menu
    static void MostrarMenu()
    {
        Console.WriteLine("********************");
        Console.WriteLine("      MENU");
        Console.WriteLine("********************");
        Console.WriteLine("1. Crear el Arreglo");
        Console.WriteLine("2. Agregar un valor en el arreglo");
        Console.WriteLine("3. Imprimir el arreglo");
        Console.WriteLine("4. Salir.");
        Console.Write("Seleccione una opcion: ");
    }


    // Funcion para crear el arreglo
    static void CrearArreglo()
    {
        Console.Clear();
        Console.Write("Ingrese el tamaño del arreglo: ");
        int tamano = int.Parse(Console.ReadLine());
        arreglo = new Arreglos(tamano);
        Console.WriteLine("El Arreglo extrañamente se creo correctamente. Presione una tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
    }


    // Funcion para agregar un valor al arreglo
    static void AgregarValor() 
    {
        Console.Clear();
        bool numeroValido = false;
        int numero = 0;

        while (!numeroValido)
        {
            Console.Write("Ingrese un numero \"Positivo\" para agregar : ");

            if (int.TryParse(Console.ReadLine(), out numero))
                {
                    if (numero >= 0)
                    {
                        numeroValido = true;
                        arreglo.AddValor(numero);
                        Console.WriteLine("Numero agregado correctamente. Presione Enter...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                {
                    Console.WriteLine("Error: Solo se permiten numeros positivos. Intente de nuevo.");
                    
                }
                } else
            {
                Console.WriteLine("Error: Entrada invalida. Por favor ingrese un numero entero.");
            }
            
        }

    }

}

