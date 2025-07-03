using Spectre.Console;
public class AgregarEstudiante
{
    private static string archivoEstudiantes = @"Infrastructure\Files\estudiantes.txt";
    public static void Ruleta()
    {
        string nombre, apellido;

        while (true)
        {
            Console.Clear();
            AnsiConsole.Write(new Panel(new FigletText("Agregar Estudiante").Color(Color.White).Centered())
            .Border(BoxBorder.Double)
            .BorderColor(Color.LightPink4));

            Console.Write("\nIngrese el nombre del estudiante: ");
            nombre = Console.ReadLine()!.Trim();

            Console.Write("Ingrese el apellido del estudiante: ");
            apellido = Console.ReadLine()!.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
            {
                Console.WriteLine("Error: Nombre y apellido no pueden estar vacios. Intente de nuevo.");
                Console.ReadKey();
                continue;
            }

            string nuevoEstudiante = $"{nombre} {apellido}".Trim();

            try
            {
                if (!File.Exists(archivoEstudiantes))
                {
                    File.Create(archivoEstudiantes).Dispose();
                }

                var estudiantes = File.ReadAllLines(archivoEstudiantes)
                    .Select(e => e.Trim()).ToArray();

                if (estudiantes.Any(e => e.Equals(nuevoEstudiante, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Error: El estudiante ya esta registrado. Intente de nuevo.");
                    Console.ReadKey();
                    continue;
                }

                File.AppendAllText(archivoEstudiantes, nuevoEstudiante + "\n");
                Console.WriteLine("Estudiante agregado correctamente.");
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el estudiante: {ex.Message}");
                Console.ReadKey();
            }
        }

        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
    }
}