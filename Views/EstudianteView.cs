using Spectre.Console;
using Roulette.Models;

namespace Roulette.Views
{
    public class EstudianteView
    {
        public string MostrarMenuEstudiantes()
        {
            Console.Clear();
            AnsiConsole.Write(new Panel(new FigletText("Gestionar Estudiantes").Color(Color.White).Centered())
                .Border(BoxBorder.Double)
                .BorderColor(Color.LightPink4));

            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n\n[bold white]Seleccione una opcion:[/]")
                    .PageSize(4)
                    .HighlightStyle(new Style(foreground: Color.LightPink4, decoration: Decoration.Bold))
                    .AddChoices(
                        "➕ Agregar Estudiante",
                        "🗑️ Eliminar Estudiante",
                        "📋 Listar Estudiantes",
                        "🚪 Atras"));
        }

        public string SolicitarNombreEstudiante(string accion = "agregar")
        {
            return AnsiConsole.Ask<string>($"[bold LightPink4]Ingrese el nombre del estudiante a {accion}:[/]");
        }

        public string? SolicitarEstudianteAEliminar(List<Estudiante> estudiantes)
        {
            if (!estudiantes.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay estudiantes para eliminar.[/]");
                Console.ReadKey(true);
                return null;
            }

            var opciones = estudiantes.Select(e => e.Nombre).ToList();
            opciones.Add("🚪 Cancelar");

            var seleccion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold red]Seleccione el estudiante a eliminar:[/]")
                    .PageSize(10)
                    .HighlightStyle(new Style(foreground: Color.Red, decoration: Decoration.Bold))
                    .AddChoices(opciones));

            return seleccion == "🚪 Cancelar" ? null : seleccion;
        }

        public void MostrarListaEstudiantes(List<Estudiante> estudiantes)
        {
            Console.Clear();
            AnsiConsole.Write(new Panel(new FigletText("Lista de Estudiantes").Color(Color.White).Centered())
                .Border(BoxBorder.Double)
                .BorderColor(Color.LightPink4));

            if (!estudiantes.Any())
            {
                AnsiConsole.MarkupLine("\n[yellow]No hay estudiantes registrados.[/]");
            }
            else
            {
                var table = new Table();
                table.AddColumn(new TableColumn("[bold]#[/]").Centered());
                table.AddColumn(new TableColumn("[bold]Nombre[/]").LeftAligned());
                table.AddColumn(new TableColumn("[bold]Fecha Registro[/]").Centered());

                for (int i = 0; i < estudiantes.Count; i++)
                {
                    table.AddRow(
                        (i + 1).ToString(),
                        estudiantes[i].Nombre,
                        estudiantes[i].FechaRegistro.ToString("dd/MM/yyyy"));
                }

                AnsiConsole.Write(table);
            }

            AnsiConsole.MarkupLine("\n[dim]Presione cualquier tecla para continuar...[/]");
            Console.ReadKey(true);
        }

        public void MostrarMensaje(string mensaje, bool esError = false)
        {
            var color = esError ? "red" : "green";
            AnsiConsole.MarkupLine($"[{color}]{mensaje}[/]");
            Console.ReadKey(true);
        }
    }
}
