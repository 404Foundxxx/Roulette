using Roulette.Views;
using Roulette.Services;

namespace Roulette.Controllers
{
    public class MainController
    {
        private readonly MenuView _menuView;
        private readonly EstudianteController _estudianteController;
        private readonly RolController _rolController;
        private readonly RouletteController _rouletteController;

        public MainController()
        {
            _menuView = new MenuView();
            
            var estudianteService = new EstudianteService();
            var rolService = new RolService();
            var rouletteService = new RouletteService(estudianteService, rolService);

            _estudianteController = new EstudianteController(estudianteService);
            _rolController = new RolController(rolService);
            _rouletteController = new RouletteController(rouletteService);
        }

        public void Iniciar()
        {
            while (true)
            {
                var opcion = _menuView.MostrarMenuPrincipal();

                switch (opcion)
                {
                    case "🎯 Iniciar Ruleta de Seleccion":
                        _rouletteController.IniciarSeleccion();
                        break;
                    case "📜 Ver Historial de Selecciones":
                        _rouletteController.MostrarHistorial();
                        break;
                    case "🎓 Gestionar Estudiantes":
                        _estudianteController.GestionarEstudiantes();
                        break;
                    case "🔖 Gestionar Roles":
                        _rolController.GestionarRoles();
                        break;
                    case "🚪 Salir del Programa":
                        _menuView.MostrarDespedida();
                        return;
                    default:
                        _menuView.MostrarError("Selección no válida. Por favor, elija una opción del menú.");
                        break;
                }
            }
        }
    }
}
