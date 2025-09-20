using Roulette.Models;
using Roulette.Services;
using Roulette.Views;

namespace Roulette.Controllers
{
    public class RolController
    {
        private readonly RolService _rolService;
        private readonly RolView _rolView;

        public RolController(RolService rolService)
        {
            _rolService = rolService;
            _rolView = new RolView();
        }

        public void GestionarRoles()
        {
            bool continuar = true;
            while (continuar)
            {
                var opcion = _rolView.MostrarMenuRoles();

                switch (opcion)
                {
                    case "➕ Agregar Rol":
                        AgregarRol();
                        break;
                    case "🗑️ Eliminar Rol":
                        EliminarRol();
                        break;
                    case "📋 Listar Roles":
                        ListarRoles();
                        break;
                    case "🚪 Atras":
                        continuar = false;
                        break;
                    default:
                        _rolView.MostrarMensaje("Selección no válida. Por favor, elija una opción del menú.", true);
                        break;
                }
            }
        }

        private void AgregarRol()
        {
            try
            {
                var nombre = _rolView.SolicitarNombreRol();
                
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    _rolView.MostrarMensaje("El nombre no puede estar vacío.", true);
                    return;
                }

                var rol = new Rol(nombre.Trim());
                
                if (_rolService.Agregar(rol))
                {
                    _rolView.MostrarMensaje($"Rol '{rol.Nombre}' agregado exitosamente.");
                }
                else
                {
                    _rolView.MostrarMensaje($"El rol '{rol.Nombre}' ya existe.", true);
                }
            }
            catch (Exception ex)
            {
                _rolView.MostrarMensaje($"Error al agregar rol: {ex.Message}", true);
            }
        }

        private void EliminarRol()
        {
            try
            {
                var roles = _rolService.ObtenerTodos();
                var nombreAEliminar = _rolView.SolicitarRolAEliminar(roles);
                
                if (nombreAEliminar == null)
                    return;

                if (_rolService.Eliminar(nombreAEliminar))
                {
                    _rolView.MostrarMensaje($"Rol '{nombreAEliminar}' eliminado exitosamente.");
                }
                else
                {
                    _rolView.MostrarMensaje($"No se pudo eliminar el rol '{nombreAEliminar}'.", true);
                }
            }
            catch (Exception ex)
            {
                _rolView.MostrarMensaje($"Error al eliminar rol: {ex.Message}", true);
            }
        }

        private void ListarRoles()
        {
            try
            {
                var roles = _rolService.ObtenerTodos();
                _rolView.MostrarListaRoles(roles);
            }
            catch (Exception ex)
            {
                _rolView.MostrarMensaje($"Error al listar roles: {ex.Message}", true);
            }
        }
    }
}
