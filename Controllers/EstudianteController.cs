using Roulette.Models;
using Roulette.Services;
using Roulette.Views;

namespace Roulette.Controllers
{
    public class EstudianteController
    {
        private readonly EstudianteService _estudianteService;
        private readonly EstudianteView _estudianteView;

        public EstudianteController(EstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
            _estudianteView = new EstudianteView();
        }

        public void GestionarEstudiantes()
        {
            bool continuar = true;
            while (continuar)
            {
                var opcion = _estudianteView.MostrarMenuEstudiantes();

                switch (opcion)
                {
                    case "➕ Agregar Estudiante":
                        AgregarEstudiante();
                        break;
                    case "🗑️ Eliminar Estudiante":
                        EliminarEstudiante();
                        break;
                    case "📋 Listar Estudiantes":
                        ListarEstudiantes();
                        break;
                    case "🚪 Atras":
                        continuar = false;
                        break;
                    default:
                        _estudianteView.MostrarMensaje("Selección no válida. Por favor, elija una opción del menú.", true);
                        break;
                }
            }
        }

        private void AgregarEstudiante()
        {
            try
            {
                var nombre = _estudianteView.SolicitarNombreEstudiante();
                
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    _estudianteView.MostrarMensaje("El nombre no puede estar vacío.", true);
                    return;
                }

                var estudiante = new Estudiante(nombre.Trim());
                
                if (_estudianteService.Agregar(estudiante))
                {
                    _estudianteView.MostrarMensaje($"Estudiante '{estudiante.Nombre}' agregado exitosamente.");
                }
                else
                {
                    _estudianteView.MostrarMensaje($"El estudiante '{estudiante.Nombre}' ya existe.", true);
                }
            }
            catch (Exception ex)
            {
                _estudianteView.MostrarMensaje($"Error al agregar estudiante: {ex.Message}", true);
            }
        }

        private void EliminarEstudiante()
        {
            try
            {
                var estudiantes = _estudianteService.ObtenerTodos();
                var nombreAEliminar = _estudianteView.SolicitarEstudianteAEliminar(estudiantes);
                
                if (nombreAEliminar == null)
                    return;

                if (_estudianteService.Eliminar(nombreAEliminar))
                {
                    _estudianteView.MostrarMensaje($"Estudiante '{nombreAEliminar}' eliminado exitosamente.");
                }
                else
                {
                    _estudianteView.MostrarMensaje($"No se pudo eliminar el estudiante '{nombreAEliminar}'.", true);
                }
            }
            catch (Exception ex)
            {
                _estudianteView.MostrarMensaje($"Error al eliminar estudiante: {ex.Message}", true);
            }
        }

        private void ListarEstudiantes()
        {
            try
            {
                var estudiantes = _estudianteService.ObtenerTodos();
                _estudianteView.MostrarListaEstudiantes(estudiantes);
            }
            catch (Exception ex)
            {
                _estudianteView.MostrarMensaje($"Error al listar estudiantes: {ex.Message}", true);
            }
        }
    }
}
