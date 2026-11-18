
using System;

class Program
{
    static void Main(string[] args)
    {
        GestionEquipos gestion = new GestionEquipos();

        int opcion = 0;

        do
        {
            Console.Clear();

            Console.WriteLine("====================================");
            Console.WriteLine("       SISTEMA LABTECH");
            Console.WriteLine("  CONTROL DE PRESTAMOS DE EQUIPOS");
            Console.WriteLine("====================================");
            Console.WriteLine("1. Registrar equipo");
            Console.WriteLine("2. Registrar prestamo");
            Console.WriteLine("3. Devolver equipo");
            Console.WriteLine("4. Listar equipos");
            Console.WriteLine("5. Consultar disponibilidad por categoria");
            Console.WriteLine("6. Salir");
            Console.WriteLine("====================================");

            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                opcion = 0;
            }

            switch (opcion)
            {
                case 1:
                    gestion.RegistrarEquipo();
                    break;

                case 2:
                    gestion.RegistrarPrestamo();
                    break;

                case 3:
                    gestion.DevolverEquipo();
                    break;

                case 4:
                    gestion.ListarEquipos();
                    break;

                case 5:
                    gestion.ConsultarPorCategoria();
                    break;

                case 6:
                    Console.WriteLine("\nGracias por utilizar el sistema LabTech.");
                    break;

                default:
                    Console.WriteLine("\nOpcion invalida. Intente nuevamente.");
                    break;
            }

            if (opcion != 6)
            {
                Console.WriteLine("\nPresione ENTER para continuar...");
                Console.ReadLine();
            }

        } while (opcion != 6);
    }
}

