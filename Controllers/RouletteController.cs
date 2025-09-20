using Roulette.Services;
using Roulette.Views;

namespace Roulette.Controllers
{
    public class RouletteController
    {
        private readonly RouletteService _rouletteService;
        private readonly RouletteView _rouletteView;

        public RouletteController(RouletteService rouletteService)
        {
            _rouletteService = rouletteService;
            _rouletteView = new RouletteView();
        }

        public void IniciarSeleccion()
        {
            try
            {
                _rouletteView.MostrarInicioSeleccion();
                
                var seleccion = _rouletteService.RealizarSeleccionAleatoria();
                
                if (seleccion == null)
                {
                    _rouletteView.MostrarError("No hay suficientes estudiantes o roles para realizar la selección.\nSe necesitan al menos 2 estudiantes y 2 roles.");
                    return;
                }

                _rouletteView.MostrarAnimacionRuleta();
                _rouletteView.MostrarResultadoSeleccion(seleccion);
            }
            catch (Exception ex)
            {
                _rouletteView.MostrarError($"Error al realizar la selección: {ex.Message}");
            }
        }

        public void MostrarHistorial()
        {
            try
            {
                var historial = _rouletteService.ObtenerHistorial();
                _rouletteView.MostrarHistorial(historial);
            }
            catch (Exception ex)
            {
                _rouletteView.MostrarError($"Error al mostrar el historial: {ex.Message}");
            }
        }
    }
}
