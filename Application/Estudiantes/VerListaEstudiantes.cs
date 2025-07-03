using Spectre.Console;
public class VerListaEstudiantes
{
    private static string archivoEstudiantes = @"Infrastructure\Files\historial.txt";

    public static void Ruleta()
    {
        Console.Clear();
        AnsiConsole.Write(
            new Panel(new FigletText("Lista de Estudiantes").Color(Color.White).Centered())
            .Border(BoxBorder.Double)
            .BorderColor(Color.LightPink4)
        );

        string[] estudiantes;
        try
        {
            estudiantes = File.ReadAllLines(archivoEstudiantes)
                .Select(e => e.Trim())
                .Where(e => !string.IsNullOrWhiteSpace(e))
                .ToArray();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[red]Error al leer el archivo de estudiantes:[/] " + ex.Message);
            Console.Write("Presione cualquier tecla para continuar...");
            Console.ReadKey(true);
            return;
        }

        if (estudiantes.Length == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No hay estudiantes en la lista[/]");
            Console.Write("Presione cualquier tecla para continuar...");
            Console.ReadKey(true);
            return;
        }

        var table = new Table().Border(TableBorder.Rounded).BorderColor(Color.LightPink4);
        table.AddColumn("No.");
        table.AddColumn("Estudiantes");

        for (int i = 0; i < estudiantes.Length; i++)
        {
            table.AddRow((i + 1).ToString(), estudiantes[i]);
        }

        AnsiConsole.Write(table);

        Console.Write("Presione cualquier tecla para continuar...");
        Console.ReadKey(true);
    }
}