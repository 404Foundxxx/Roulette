using Spectre.Console;

public class GestionarRoles
{
    public static void Ruleta()
    {
        bool continuar = true;
        while (continuar)
        {
            Console.Clear();
            AnsiConsole.Write(new Panel(new FigletText("Gestionar Roles").Color(Color.White).Centered())
            .Border(BoxBorder.Double)
            .BorderColor(Color.LightPink4));

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n\n[bold white]Seleccione una opcion:[/]")
                    .PageSize(3)
                    .HighlightStyle(new Style(foreground: Color.LightPink4, decoration: Decoration.Bold))
                    .AddChoices(
                        "✏️ Personalizar Roles Predeterminados",
                        "🚪 Atras"
                    ));

            switch (opcion)
            {
                case "✏️ Personalizar Roles Predeterminados":
                    PersonalizarRolesPredeterminados.Ruleta();
                    break;
                case "🚪 Atras":
                    continuar = false;
                    break;
                default:
                    AnsiConsole.MarkupLine("[red]Seleccion no valida. Por favor, elija una opcion del menu.[/]");
                    break;
            }
        }
    }

    public class PersonalizarRolesPredeterminados
    {
        private static string archivoRoles = @"Archivos\Datos\roles.txt";
        private static Dictionary<int, string> roles = new Dictionary<int, string>
        {
            { 1, "Desarrollador en Vivo" },
            { 2, "Facilitador de Ejercicio" }
        };

        public static void Ruleta()
        {
            CargarRoles();

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new Panel(new FigletText("Personalizar Roles").Color(Color.White).Centered())
                    .Border(BoxBorder.Double)
                    .BorderColor(Color.LightPink4));

                MostrarRolesActuales();

                var opcionRol = AnsiConsole.Prompt(
                    new SelectionPrompt<int>()
                        .Title("\nSeleccione el rol que desea personalizar:")
                        .PageSize(3)
                        .HighlightStyle(new Style(foreground: Color.LightPink4, decoration: Decoration.Bold))
                        .AddChoices(roles.Keys.ToArray())
                );

                Console.Write("\nIngrese el nuevo nombre para el rol seleccionado: ");
                string nuevoRol = Console.ReadLine()!.Trim();

                if (string.IsNullOrWhiteSpace(nuevoRol))
                {
                    AnsiConsole.Markup("[red]Error: El rol no puede estar vacio. Intente de nuevo.[/]");
                    Console.ReadKey();
                    continue;
                }

                roles[opcionRol] = nuevoRol;
                GuardarRoles();

                AnsiConsole.Markup("[green]✔  Rol personalizado correctamente.[/]");
                break;
            }

            AnsiConsole.Markup("[bold green]\nPresione cualquier tecla para continuar...[/]");
            Console.ReadKey();
        }

        private static void MostrarRolesActuales()
        {
            var table = new Table()
                .Border(TableBorder.Rounded)
                .BorderColor(Color.LightPink4)
                .AddColumn("[White]ID[/]")
                .AddColumn("[White]Rol[/]");

            foreach (var rol in roles)
            {
                table.AddRow(rol.Key.ToString(), rol.Value);
            }

            AnsiConsole.Write(table);
        }

        private static void CargarRoles()
        {
            if (!File.Exists(archivoRoles))
            {
                File.WriteAllLines(archivoRoles, roles.Values);
                return;
            }

            string[] lineas = File.ReadAllLines(archivoRoles).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

            if (lineas.Length >= roles.Count)
            {
                int index = 1;
                foreach (var linea in lineas)
                {
                    roles[index] = linea;
                    index++;
                }
            }
        }

        private static void GuardarRoles()
        {
            File.WriteAllLines(archivoRoles, roles.Values);
        }
    }
}
