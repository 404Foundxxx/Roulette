using Roulette.Models;
using Roulette.Infrastructure.Storage;

namespace Roulette.Services
{
    public class RolService
    {
        private readonly TextFileManager _fileManager;
        private readonly string _archivoRoles = @"Infrastructure\Files\roles.txt";

        public RolService()
        {
            _fileManager = new TextFileManager();
        }

        public List<Rol> ObtenerTodos()
        {
            var lineas = _fileManager.LeerLineas(_archivoRoles);
            return lineas.Where(r => !string.IsNullOrWhiteSpace(r))
                        .Select(nombre => new Rol(nombre.Trim()))
                        .ToList();
        }

        public bool Agregar(Rol rol)
        {
            if (string.IsNullOrWhiteSpace(rol.Nombre))
                return false;

            var roles = ObtenerTodos();
            if (roles.Any(r => r.Equals(rol)))
                return false;

            _fileManager.AgregarLinea(_archivoRoles, rol.Nombre);
            return true;
        }

        public bool Eliminar(string nombre)
        {
            var roles = ObtenerTodos();
            var rolesFiltrados = roles.Where(r => !r.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                                     .Select(r => r.Nombre);

            if (roles.Count == rolesFiltrados.Count())
                return false;

            _fileManager.EscribirLineas(_archivoRoles, rolesFiltrados);
            return true;
        }

        public bool Existe(string nombre)
        {
            var roles = ObtenerTodos();
            return roles.Any(r => r.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }
    }
}
