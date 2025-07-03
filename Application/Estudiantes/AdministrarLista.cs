using Spectre.Console;
public class AdministrarListaEstudiantes
{
    public static void Ruleta()
    {
        bool continuar = true;
        while (continuar)
        {
            Console.Clear();
            AnsiConsole.Write(new Panel(new FigletText("Gestionar Estudiantes").Color(Color.White).Centered())
            .Border(BoxBorder.Double)
            .BorderColor(Color.LightPink4));

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n\n[bold white]Seleccione una opcion:[/]")
                    .PageSize(4)
                    .HighlightStyle(new Style(foreground: Color.LightPink4, decoration: Decoration.Bold))
                    .AddChoices(
                        "➕ Agregar Estudiante",
                        "🗑️ Eliminar Estudiante",
                        "📋 Listar Estudiantes",
                        "🚪 Atras"
                    ));

            switch (opcion)
            {
                case "➕ Agregar Estudiante":
                    AgregarEstudiante.Ruleta();
                    break;
                case "🗑️ Eliminar Estudiante":
                    EliminarEstudiante.Ruleta();
                    break;
                case "📋 Listar Estudiantes":
                    VerListaEstudiantes.Ruleta();
                    break;
                case "🚪 Atras":
                    continuar = false;
                    return;
                default:
                    AnsiConsole.MarkupLine("[red]Seleccion no valida. Por favor, elija una opcion del menu.[/]");
                    break;
            }
        }
    }
}
