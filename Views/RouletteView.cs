using Spectre.Console;
using Roulette.Models;
using Roulette.Shared.Utilities;

namespace Roulette.Views
{
    public class RouletteView
    {
        public void MostrarInicioSeleccion()
        {
            Console.Clear();
            AnsiConsole.Write(new Panel(new FigletText("Seleccion Aleatoria").Color(Color.White).Centered())
                .Border(BoxBorder.Double)
                .BorderColor(Color.LightPink4));
        }

        public void MostrarAnimacionRuleta()
        {
            AnimarRuleta.IniciarAnimacion();
        }

        public void MostrarResultadoSeleccion(Seleccion seleccion)
        {
            Console.Clear();
            
            var panel = new Panel(
                new Markup($"[bold yellow]🎉 RESULTADO DE LA SELECCIÓN 🎉[/]\n\n" +
                          $"[bold cyan]Estudiante:[/] [white]{seleccion.Estudiante.Nombre}[/]\n" +
                          $"[bold magenta]Rol:[/] [white]{seleccion.Rol.Nombre}[/]\n" +
                          $"[bold green]Fecha:[/] [white]{seleccion.FechaSeleccion:dd/MM/yyyy HH:mm}[/]"))
                .Border(BoxBorder.Double)
                .BorderColor(Color.Gold1)
                .Padding(2, 1);

            AnsiConsole.Write(panel);
            
            AnsiConsole.MarkupLine("\n[dim]Presione cualquier tecla para continuar...[/]");
            Console.ReadKey(true);
        }

        public void MostrarHistorial(List<Seleccion> historial)
        {
            Console.Clear();
            AnsiConsole.Write(new Panel(new FigletText("Historial").Color(Color.White).Centered())
                .Border(BoxBorder.Double)
                .BorderColor(Color.LightPink4));

            if (!historial.Any())
            {
                AnsiConsole.MarkupLine("\n[yellow]No hay selecciones en el historial.[/]");
            }
            else
            {
                var table = new Table();
                table.AddColumn(new TableColumn("[bold]#[/]").Centered());
                table.AddColumn(new TableColumn("[bold]Fecha[/]").Centered());
                table.AddColumn(new TableColumn("[bold]Estudiante[/]").LeftAligned());
                table.AddColumn(new TableColumn("[bold]Rol[/]").LeftAligned());

                for (int i = 0; i < Math.Min(historial.Count, 20); i++) // Mostrar solo los últimos 20
                {
                    var seleccion = historial[i];
                    table.AddRow(
                        (i + 1).ToString(),
                        seleccion.FechaSeleccion.ToString("dd/MM/yyyy HH:mm"),
                        seleccion.Estudiante.Nombre,
                        seleccion.Rol.Nombre);
                }

                AnsiConsole.Write(table);

                if (historial.Count > 20)
                {
                    AnsiConsole.MarkupLine($"\n[dim]Mostrando los últimos 20 registros de {historial.Count} total.[/]");
                }
            }

            AnsiConsole.MarkupLine("\n[dim]Presione cualquier tecla para continuar...[/]");
            Console.ReadKey(true);
        }

        public void MostrarError(string mensaje)
        {
            AnsiConsole.MarkupLine($"\n[red]{mensaje}[/]");
            Console.ReadKey(true);
        }
    }
}
