using Spectre.Console;
public class AnimarRuleta
{
    public static void Ruleta(string[] estudiantes, Random random)
    {
        Console.CursorVisible = false;
        for (int i = 0; i < 100; i++)
        {
            Console.Clear();
            int indice = random.Next(estudiantes.Length);
            SaltoDeLinea.SaltoDeLinea16();
            AnsiConsole.Write(new FigletText($"{estudiantes[indice]}").Color(Color.White).Centered());
            Thread.Sleep(50);
        }
    }
}