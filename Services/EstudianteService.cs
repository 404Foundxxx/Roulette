using Roulette.Models;
using Roulette.Infrastructure.Storage;

namespace Roulette.Services
{
    public class EstudianteService
    {
        private readonly TextFileManager _fileManager;
        private readonly string _archivoEstudiantes = @"Infrastructure\Files\estudiantes.txt";

        public EstudianteService()
        {
            _fileManager = new TextFileManager();
        }

        public List<Estudiante> ObtenerTodos()
        {
            var lineas = _fileManager.LeerLineas(_archivoEstudiantes);
            return lineas.Where(e => !string.IsNullOrWhiteSpace(e))
                        .Distinct()
                        .Select(nombre => new Estudiante(nombre.Trim()))
                        .ToList();
        }

        public bool Agregar(Estudiante estudiante)
        {
            if (string.IsNullOrWhiteSpace(estudiante.Nombre))
                return false;

            var estudiantes = ObtenerTodos();
            if (estudiantes.Any(e => e.Equals(estudiante)))
                return false;

            _fileManager.AgregarLinea(_archivoEstudiantes, estudiante.Nombre);
            return true;
        }

        public bool Eliminar(string nombre)
        {
            var estudiantes = ObtenerTodos();
            var estudiantesFiltrados = estudiantes.Where(e => !e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                                                  .Select(e => e.Nombre);

            if (estudiantes.Count == estudiantesFiltrados.Count())
                return false;

            _fileManager.EscribirLineas(_archivoEstudiantes, estudiantesFiltrados);
            return true;
        }

        public bool Existe(string nombre)
        {
            var estudiantes = ObtenerTodos();
            return estudiantes.Any(e => e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }
    }
}
