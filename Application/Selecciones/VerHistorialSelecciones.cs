using Spectre.Console;

public class VerHistorialSelecciones
{
    private static string archivoHistorial = @"Infrastructure\Files\historial.txt";
    private static string archivoRoles = @"Infrastructure\Files\roles.txt";

    public static void Ruleta()
    {
        Console.CursorVisible = false;
        Console.Clear();
        AnsiConsole.Write(new Panel(new FigletText("Consultar Historial").Color(Color.White).Centered())
            .Border(BoxBorder.Double)
            .BorderColor(Color.LightPink4));

        if (!File.Exists(archivoHistorial) || new FileInfo(archivoHistorial).Length == 0)
        {
            AnsiConsole.MarkupLine("[red]No hay historial de selecciones.[/]");
            Console.ReadKey(true);
            return;
        }

        string[] roles = File.Exists(archivoRoles) ? File.ReadAllLines(archivoRoles) : null!;

        if (roles == null || roles.Length == 0)
        {
            AnsiConsole.MarkupLine("[red]Error: No se encontro el archivo de roles o esta vacio.[/]");
            Console.ReadKey(true);
            return;
        }

        try
        {
            string[] lineas = File.ReadAllLines(archivoHistorial);
            var tabla = new Table().Border(TableBorder.Rounded).BorderColor(Color.LightPink4);

            tabla.AddColumn("Fecha y Hora");
            foreach (var rol in roles)
            {
                tabla.AddColumn(rol);
            }

            string fechaHora = "";
            string[] valoresRoles = new string[roles.Length];

            foreach (var linea in lineas)
            {
                if (DateTime.TryParse(linea, out _))
                {
                    if (!string.IsNullOrWhiteSpace(fechaHora))
                    {
                        var fila = new string[roles.Length + 1];
                        fila[0] = $"[white]{fechaHora}[/]";

                        for (int i = 0; i < roles.Length; i++)
                        {
                            fila[i + 1] = !string.IsNullOrWhiteSpace(valoresRoles[i]) ? $"[navajowhite1]{valoresRoles[i]}[/]" : "[red]N/A[/]";
                        }

                        tabla.AddRow(fila);
                    }

                    fechaHora = linea;
                    valoresRoles = new string[roles.Length];
                }
                else
                {
                    for (int i = 0; i < roles.Length; i++)
                    {
                        if (linea.StartsWith(roles[i] + ":"))
                        {
                            valoresRoles[i] = linea.Split(':')[1].Trim();
                            break;
                        }
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(fechaHora))
            {
                var fila = new string[roles.Length + 1];
                fila[0] = $"[white]{fechaHora}[/]";

                for (int i = 0; i < roles.Length; i++)
                {
                    fila[i + 1] = !string.IsNullOrWhiteSpace(valoresRoles[i]) ? $"[navajowhite1]{valoresRoles[i]}[/]" : "[red]N/A[/]";
                }

                tabla.AddRow(fila);
            }

            AnsiConsole.Write("\n");
            AnsiConsole.Write(tabla);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]Error al leer el historial: {ex.Message}[/]");
        }

        AnsiConsole.MarkupLine("[yellow]Presione cualquier tecla para continuar...[/]");
        Console.ReadKey(true);
    }
}
