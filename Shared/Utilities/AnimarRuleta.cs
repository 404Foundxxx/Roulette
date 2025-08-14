using Spectre.Console;

namespace Roulette.Shared.Utilities
{
    public class AnimarRuleta
    {
        public static void IniciarAnimacion()
        {
            var mensajes = new[]
            {
                "Seleccionando...",
                "Girando la ruleta...",
                "Calculando resultado...",
                "¡Ya casi está!"
            };

            Console.CursorVisible = false;

            for (int i = 0; i < 60; i++)
            {
                Console.Clear();
                SaltoDeLinea.SaltoDeLinea16();
                
                var mensaje = mensajes[i % mensajes.Length];
                var puntos = new string('.', (i % 4) + 1);
                
                AnsiConsole.Write(new FigletText($"{mensaje}{puntos}")
                    .Color(Color.Yellow)
                    .Centered());
                
                Thread.Sleep(100);
            }
        }
    }
}