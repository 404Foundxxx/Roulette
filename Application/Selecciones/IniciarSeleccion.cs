using Spectre.Console;
public class IniciarSeleccionAleatoria
{
    private static string archivoEstudiantes = @"Infrastructure\Files\estudiantes.txt";
    private static string archivoEstadoSeleccion = @"Infrastructure\Files\estado_seleccion.txt";
    private static string archivoRoles = @"Infrastructure\Files\roles.txt";
    private static string archivoHistorial = @"Infrastructure\Files\historial.txt";
    

    public static void Ruleta()
    {
        Console.CursorVisible = false;

        if (!File.Exists(archivoEstudiantes) || new FileInfo(archivoEstudiantes).Length == 0)
        {
            AnsiConsole.MarkupLine("\n[red]El archivo de estudiantes no existe o está vacío.[/]");
            Console.ReadKey(true);
            return;
        }

        if (!File.Exists(archivoRoles) || new FileInfo(archivoRoles).Length == 0)
        {
            AnsiConsole.MarkupLine("\n[red]El archivo de roles no existe o está vacío.[/]");
            Console.ReadKey(true);
            return;
        }

        string[] estudiantes = File.ReadAllLines(archivoEstudiantes)
            .Where(e => !string.IsNullOrWhiteSpace(e))
            .Distinct()
            .ToArray();

        if (estudiantes.Length < 2)
        {
            Console.WriteLine("No hay suficientes estudiantes en la lista para seleccionar.");
            return;
        }

        string[] roles = File.ReadAllLines(archivoRoles)
            .Where(r => !string.IsNullOrWhiteSpace(r))
            .ToArray();

        if (roles.Length < 2)
        {
            Console.WriteLine("No hay suficientes roles en la lista.");
            return;
        }

        Dictionary<string, Dictionary<string, int>> estadoSeleccion = CargarEstadoSeleccion();

        Random random = new Random();

        while (true)
        {
            // Si todos han sido seleccionados dos veces, pedir reiniciar el historial
            if (estadoSeleccion.Count > 0 && estadoSeleccion.All(e => e.Value.Values.Sum() >= 2))
            {
                AnsiConsole.Markup("\n[bold orange1]⚠️ Todos los estudiantes han sido asignados a sus respectivos roles.[/]");
                AnsiConsole.Markup("\n[bold yellow]Para volver a usar la ruleta se necesita reiniciar el historial.[/]\n\n");

                var seleccion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Selecciona una opción:[/]")
                        .HighlightStyle(new Style(foreground: Color.LightPink4, decoration: Decoration.Bold))
                        .AddChoices("Reiniciar", "Salir")
                );

                if (seleccion == "Salir")
                {
                    Console.Clear();
                    return;
                }

                ReiniciarHistorial();
                Console.Clear();
            }

            // Filtrar estudiantes disponibles (menos de 2 selecciones en total)
            List<string> estudiantesDisponibles = estudiantes
                .Where(e => !estadoSeleccion.ContainsKey(e) || estadoSeleccion[e].Values.Sum() < 2)
                .ToList();

            if (estudiantesDisponibles.Count < 2)
            {
                Console.WriteLine("No hay suficientes estudiantes disponibles para asignar roles.");
                return;
            }

            string estudiante1, estudiante2;
            do
            {
                estudiante1 = estudiantesDisponibles[random.Next(estudiantesDisponibles.Count)];
            } while (estadoSeleccion.ContainsKey(estudiante1) &&
                     estadoSeleccion[estudiante1].ContainsKey(roles[0]) &&
                     estadoSeleccion[estudiante1][roles[0]] >= 1);

            do
            {
                estudiante2 = estudiantesDisponibles[random.Next(estudiantesDisponibles.Count)];
            } while (estadoSeleccion.ContainsKey(estudiante2) &&
                     estadoSeleccion[estudiante2].ContainsKey(roles[1]) &&
                     estadoSeleccion[estudiante2][roles[1]] >= 1 ||
                     estudiante1 == estudiante2);

            AnimarRuleta.Ruleta(estudiantes, random);
            MostrarSeleccion(roles[0], estudiante1);
            SaltoDeLinea.SaltoDeLinea12();
            Console.Write("\nPresione cualquier tecla para continuar con el siguiente rol...");
            Console.ReadKey();

            AnimarRuleta.Ruleta(estudiantes, random);
            MostrarSeleccion(roles[1], estudiante2);

            GuardarHistorial(roles[0], estudiante1, roles[1], estudiante2);

            ActualizarEstadoSeleccion(estadoSeleccion, estudiante1, roles[0]);
            ActualizarEstadoSeleccion(estadoSeleccion, estudiante2, roles[1]);
            GuardarEstadoSeleccion(estadoSeleccion);

            SaltoDeLinea.SaltoDeLinea12();
            Console.Write("\nPresione [ESC] para salir o cualquier otra tecla para continuar...");
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                Console.Clear();
                return;
            }

            Console.Clear();
        }
    }

    private static void MostrarSeleccion(string rol, string nombre)
    {
        Console.Clear();
        Console.CursorVisible = false;
        AnsiConsole.Write(new FigletText(rol).Color(Color.LightPink4).Centered());
        SaltoDeLinea.SaltoDeLinea10();
        AnsiConsole.Write(new FigletText(nombre).Color(Color.White).Centered());
    }

    private static void GuardarHistorial(string rol1, string estudiante1, string rol2, string estudiante2)
    {
        string fechaFormato = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
        string registro = $"{fechaFormato}\n{rol1}: {estudiante1}\n{rol2}: {estudiante2}\n";
        File.AppendAllText(archivoHistorial, registro + Environment.NewLine);
    }

    private static Dictionary<string, Dictionary<string, int>> CargarEstadoSeleccion()
    {
        Dictionary<string, Dictionary<string, int>> estadoSeleccion = new();

        if (File.Exists(archivoEstadoSeleccion))
        {
            string[] estado = File.ReadAllLines(archivoEstadoSeleccion);
            foreach (var linea in estado)
            {
                var partes = linea.Split(',');
                if (partes.Length == 3)
                {
                    string estudiante = partes[0];
                    string rol = partes[1];
                    int vecesSeleccionado = int.Parse(partes[2]);

                    if (!estadoSeleccion.ContainsKey(estudiante))
                    {
                        estadoSeleccion[estudiante] = new Dictionary<string, int>();
                    }

                    estadoSeleccion[estudiante][rol] = vecesSeleccionado;
                }
            }
        }
        return estadoSeleccion;
    }

    private static void ActualizarEstadoSeleccion(Dictionary<string, Dictionary<string, int>> estadoSeleccion, string estudiante, string rol)
    {
        if (!estadoSeleccion.ContainsKey(estudiante))
        {
            estadoSeleccion[estudiante] = new Dictionary<string, int>();
        }
        if (!estadoSeleccion[estudiante].ContainsKey(rol))
        {
            estadoSeleccion[estudiante][rol] = 0;
        }
        estadoSeleccion[estudiante][rol]++;
    }

    private static void GuardarEstadoSeleccion(Dictionary<string, Dictionary<string, int>> estadoSeleccion)
    {
        List<string> lineas = new();

        foreach (var estudiante in estadoSeleccion)
        {
            foreach (var rol in estudiante.Value)
            {
                lineas.Add($"{estudiante.Key},{rol.Key},{rol.Value}");
            }
        }

        File.WriteAllLines(archivoEstadoSeleccion, lineas);
    }

    private static void ReiniciarHistorial()
    {
        File.WriteAllText(archivoEstadoSeleccion, string.Empty);
        File.WriteAllText(archivoHistorial, string.Empty);
        Console.WriteLine("Historial reiniciado correctamente.");
    }
}