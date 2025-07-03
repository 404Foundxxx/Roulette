using Spectre.Console;
public class EliminarEstudiante
{
    private static string archivoEstudiantes = @"Infrastructure\Files\estudiantes.txt";

    public static void Ruleta()
    {
        Console.Clear();
        AnsiConsole.Write(new Panel(new FigletText("Eliminar Estudiante").Color(Color.White).Centered())
            .Border(BoxBorder.Double)
            .BorderColor(Color.LightPink4));

        string[] estudiantes;
        try
        {
            estudiantes = File.ReadAllLines(archivoEstudiantes);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo de estudiantes: {ex.Message}");
            Console.ReadKey();
            return;
        }

        if (estudiantes.Length == 0)
        {
            Console.WriteLine("No hay estudiantes en la lista");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            return;
        }

        var tabla = new Table().Border(TableBorder.Rounded).BorderColor(Color.LightPink4);
        tabla.AddColumn("No.").AddColumn("Nombre del Estudiante");

        for (int i = 0; i < estudiantes.Length; i++)
        {
            tabla.AddRow((i + 1).ToString(), estudiantes[i]);
        }

        AnsiConsole.Write(tabla);

        int numeroEstudiante;
        while (true)
        {
            Console.Write("Ingrese el numero del estudiante a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out numeroEstudiante) && numeroEstudiante >= 1 && numeroEstudiante <= estudiantes.Length)
            {
                break;
            }
            Console.WriteLine("Numero de estudiante invalido. Intente de nuevo.");
        }

        string estudianteAEliminar = estudiantes[numeroEstudiante - 1];

        if (!AnsiConsole.Confirm($"¿Esta seguro de que desea eliminar a '{estudianteAEliminar}'?"))
        {
            Console.WriteLine("Operacion cancelada.");
            Console.ReadKey();
            return;
        }

        try
        {
            var nuevosEstudiantes = estudiantes.Where((e, i) => i != numeroEstudiante - 1).ToArray();
            File.WriteAllLines(archivoEstudiantes, nuevosEstudiantes);

            Console.WriteLine("Estudiante eliminado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar el estudiante: {ex.Message}");
        }

        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
    }
}