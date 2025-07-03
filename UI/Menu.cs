using Spectre.Console;
namespace Roulette.UI
{
    class Menu
    {
        public static void menu()
        {
            while (true)
            {
                Console.CursorVisible = false;
                Console.Clear();

                AnsiConsole.Write(new Panel(new FigletText("Ruleta de Roles").Centered())
                .Border(BoxBorder.Double)
                .BorderColor(Color.LightPink4));

                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\n\n[bold LightPink4]Seleccione una opcion:[/]")
                        .PageSize(5)
                        .HighlightStyle(new Style(foreground: Color.LightPink4, decoration: Decoration.Bold))
                        .AddChoices(
                            "🎯 Iniciar Ruleta de Seleccion",
                            "📜 Ver Historial de Selecciones",
                            "🎓 Gestionar Estudiantes",
                            "🔖 Gestionar Roles",
                            "🚪 Salir del Programa"));

                Console.CursorVisible = false;
                switch (opcion)
                {
                    case "🎯 Iniciar Ruleta de Seleccion":
                        IniciarSeleccionAleatoria.Ruleta();
                        break;
                    case "📜 Ver Historial de Selecciones":
                        VerHistorialSelecciones.Ruleta();
                        break;
                    case "🎓 Gestionar Estudiantes":
                        AdministrarListaEstudiantes.Ruleta();
                        break;
                    case "🔖 Gestionar Roles":
                        GestionarRoles.Ruleta();
                        break;
                    case "🚪 Salir del Programa":
                        SalirDelPrograma();
                        return;
                    default:
                        AnsiConsole.MarkupLine("[red]Seleccion no valida. Por favor, elija una opcion del menu.[/]");
                        break;
                }
            }
        }
        static void SalirDelPrograma()
        {
            Console.Clear();
            AnsiConsole.Write(new Panel(new FigletText("Suerte!").Centered())
                .Border(BoxBorder.Double)
                .BorderColor(Color.LightPink4));
            Thread.Sleep(3000);
            Console.Clear();
        }
    }
}