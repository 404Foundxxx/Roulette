using Spectre.Console;
using Roulette.Models;

namespace Roulette.Views
{
    public class RolView
    {
        public string MostrarMenuRoles()
        {
            Console.Clear();
            AnsiConsole.Write(new Panel(new FigletText("Gestionar Roles").Color(Color.White).Centered())
                .Border(BoxBorder.Double)
                .BorderColor(Color.LightPink4));

            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n\n[bold white]Seleccione una opcion:[/]")
                    .PageSize(4)
                    .HighlightStyle(new Style(foreground: Color.LightPink4, decoration: Decoration.Bold))
                    .AddChoices(
                        "➕ Agregar Rol",
                        "🗑️ Eliminar Rol",
                        "📋 Listar Roles",
                        "🚪 Atras"));
        }

        public string SolicitarNombreRol(string accion = "agregar")
        {
            return AnsiConsole.Ask<string>($"[bold LightPink4]Ingrese el nombre del rol a {accion}:[/]");
        }

        public string? SolicitarRolAEliminar(List<Rol> roles)
        {
            if (!roles.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay roles para eliminar.[/]");
                Console.ReadKey(true);
                return null;
            }

            var opciones = roles.Select(r => r.Nombre).ToList();
            opciones.Add("🚪 Cancelar");

            var seleccion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold red]Seleccione el rol a eliminar:[/]")
                    .PageSize(10)
                    .HighlightStyle(new Style(foreground: Color.Red, decoration: Decoration.Bold))
                    .AddChoices(opciones));

            return seleccion == "🚪 Cancelar" ? null : seleccion;
        }

        public void MostrarListaRoles(List<Rol> roles)
        {
            Console.Clear();
            AnsiConsole.Write(new Panel(new FigletText("Lista de Roles").Color(Color.White).Centered())
                .Border(BoxBorder.Double)
                .BorderColor(Color.LightPink4));

            if (!roles.Any())
            {
                AnsiConsole.MarkupLine("\n[yellow]No hay roles registrados.[/]");
            }
            else
            {
                var table = new Table();
                table.AddColumn(new TableColumn("[bold]#[/]").Centered());
                table.AddColumn(new TableColumn("[bold]Nombre[/]").LeftAligned());

                for (int i = 0; i < roles.Count; i++)
                {
                    table.AddRow(
                        (i + 1).ToString(),
                        roles[i].Nombre);
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
