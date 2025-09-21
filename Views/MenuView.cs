using Spectre.Console;
using Roulette.Models;

namespace Roulette.Views
{
    public class MenuView
    {
        public string MostrarMenuPrincipal()
        {
            Console.CursorVisible = false;
            Console.Clear();

            AnsiConsole.Write(new Panel(new FigletText("Ruleta de Roles").Centered())
                .Border(BoxBorder.Double)
                .BorderColor(Color.LightPink4));

            return AnsiConsole.Prompt(
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
        }

        public void MostrarDespedida()
        {
            Console.Clear();
            AnsiConsole.Write(new Panel(new FigletText("Suerte!").Centered())
                .Border(BoxBorder.Double)
                .BorderColor(Color.LightPink4));
            Thread.Sleep(3000);
            Console.Clear();
        }

        public void MostrarError(string mensaje)
        {
            AnsiConsole.MarkupLine($"[red]{mensaje}[/]");
            Console.ReadKey(true);
        }

        public void MostrarExito(string mensaje)
        {
            AnsiConsole.MarkupLine($"[green]{mensaje}[/]");
            Console.ReadKey(true);
        }

        public void EsperarTecla()
        {
            Console.ReadKey(true);
        }
    }
}
