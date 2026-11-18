
using System;
using System.Collections.Generic;
using System.Linq;

public class Equipo
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public string Categoria { get; set; }
    public bool Disponible { get; set; }
    public string Estudiante { get; set; }
    public DateTime? FechaPrestamo { get; set; }

    public Equipo(string codigo, string nombre, string categoria)
    {
        Codigo = codigo;
        Nombre = nombre;
        Categoria = categoria;
        Disponible = true;
        Estudiante = "";
        FechaPrestamo = null;
    }
}

public class GestionEquipos
{
    private List<Equipo> equipos = new List<Equipo>();

    // Valida que un texto no este vacio.
    private string LeerTextoValido(string mensaje)
    {
        string texto;

        do
        {
            Console.Write(mensaje);
            texto = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(texto))
            {
                Console.WriteLine("Error: este campo no puede estar vacio.");
            }

        } while (string.IsNullOrWhiteSpace(texto));

        return texto.Trim();
    }

    // Busca un equipo por su codigo.
    private Equipo? BuscarEquipo(string codigo)
    {
        return equipos.FirstOrDefault(
            e => e.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase)
        );
    }

    // 1. Registrar un equipo.
    public void RegistrarEquipo()
    {
        Console.WriteLine("\n--- REGISTRAR EQUIPO ---");

        string codigo = LeerTextoValido("Codigo del equipo: ").ToUpper();

        if (BuscarEquipo(codigo) != null)
        {
            Console.WriteLine("Error: ya existe un equipo con ese codigo.");
            return;
        }

        string nombre = LeerTextoValido("Nombre del equipo: ");
        string categoria = LeerTextoValido("Categoria: ");

        Equipo nuevoEquipo = new Equipo(codigo, nombre, categoria);

        equipos.Add(nuevoEquipo);

        Console.WriteLine("\nEquipo registrado correctamente.");
    }

    // 2. Registrar un prestamo.
    public void RegistrarPrestamo()
    {
        Console.WriteLine("\n--- REGISTRAR PRESTAMO ---");

        string codigo = LeerTextoValido("Codigo del equipo: ").ToUpper();

        Equipo? equipo = BuscarEquipo(codigo);

        if (equipo == null)
        {
            Console.WriteLine("Error: el codigo del equipo no existe.");
            return;
        }

        if (!equipo.Disponible)
        {
            Console.WriteLine("Error: este equipo ya se encuentra prestado.");
            return;
        }

        string estudiante = LeerTextoValido("Nombre del estudiante: ");

        DateTime fecha;

        while (true)
        {
            Console.Write("Fecha del prestamo (dd/MM/yyyy): ");
            string entrada = Console.ReadLine() ?? "";

            if (DateTime.TryParseExact(
                entrada,
                "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out fecha))
            {
                break;
            }

            Console.WriteLine("Fecha invalida. Intente nuevamente.");
        }

        equipo.Disponible = false;
        equipo.Estudiante = estudiante;
        equipo.FechaPrestamo = fecha;

        Console.WriteLine("\nPrestamo registrado correctamente.");
        Console.WriteLine("Equipo: " + equipo.Nombre);
        Console.WriteLine("Estudiante: " + equipo.Estudiante);
        Console.WriteLine("Fecha: " + fecha.ToString("dd/MM/yyyy"));
    }

    // 3. Devolver un equipo.
    public void DevolverEquipo()
    {
        Console.WriteLine("\n--- DEVOLVER EQUIPO ---");

        string codigo = LeerTextoValido("Codigo del equipo: ").ToUpper();

        Equipo? equipo = BuscarEquipo(codigo);

        if (equipo == null)
        {
            Console.WriteLine("Error: el codigo del equipo no existe.");
            return;
        }

        if (equipo.Disponible)
        {
            Console.WriteLine("Este equipo ya se encuentra disponible.");
            return;
        }

        equipo.Disponible = true;
        equipo.Estudiante = "";
        equipo.FechaPrestamo = null;

        Console.WriteLine("\nEquipo devuelto correctamente.");
        Console.WriteLine("El equipo ahora esta disponible.");
    }

    // 4. Listar todos los equipos.
    public void ListarEquipos()
    {
        Console.WriteLine("\n--- LISTA DE EQUIPOS ---");

        if (equipos.Count == 0)
        {
            Console.WriteLine("No hay equipos registrados.");
            return;
        }

        foreach (Equipo equipo in equipos)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Codigo: " + equipo.Codigo);
            Console.WriteLine("Nombre: " + equipo.Nombre);
            Console.WriteLine("Categoria: " + equipo.Categoria);
            Console.WriteLine("Estado: " +
                (equipo.Disponible ? "Disponible" : "Prestado"));

            if (!equipo.Disponible)
            {
                Console.WriteLine("Estudiante: " + equipo.Estudiante);
                Console.WriteLine("Fecha de prestamo: " +
                    equipo.FechaPrestamo?.ToString("dd/MM/yyyy"));
            }
        }

        Console.WriteLine("--------------------------------");
    }

    // 5. Consultar equipos disponibles por categoria.
    public void ConsultarPorCategoria()
    {
        Console.WriteLine("\n--- DISPONIBILIDAD POR CATEGORIA ---");

        if (equipos.Count == 0)
        {
            Console.WriteLine("No hay equipos registrados.");
            return;
        }

        var categorias = equipos
            .GroupBy(e => e.Categoria, StringComparer.OrdinalIgnoreCase)
            .OrderBy(g => g.Key);

        foreach (var categoria in categorias)
        {
            int disponibles = categoria.Count(e => e.Disponible);
            int total = categoria.Count();

            Console.WriteLine("\nCategoria: " + categoria.Key);
            Console.WriteLine("Total de equipos: " + total);
            Console.WriteLine("Disponibles: " + disponibles);
        }
    }
}
