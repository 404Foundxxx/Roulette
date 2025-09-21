using Roulette.Models;
using Roulette.Infrastructure.Storage;
using System.Text.Json;

namespace Roulette.Services
{
    public class RouletteService
    {
        private readonly EstudianteService _estudianteService;
        private readonly RolService _rolService;
        private readonly TextFileManager _fileManager;
        private readonly string _archivoEstadoSeleccion = @"Infrastructure\Files\estado_seleccion.txt";
        private readonly string _archivoHistorial = @"Infrastructure\Files\historial.txt";

        public RouletteService(EstudianteService estudianteService, RolService rolService)
        {
            _estudianteService = estudianteService;
            _rolService = rolService;
            _fileManager = new TextFileManager();
        }

        public Seleccion? RealizarSeleccionAleatoria()
        {
            var estudiantes = _estudianteService.ObtenerTodos();
            var roles = _rolService.ObtenerTodos();

            if (estudiantes.Count < 2 || roles.Count < 2)
                return null;

            var estadoSeleccion = CargarEstadoSeleccion();
            var estudianteSeleccionado = SeleccionarEstudianteOptimo(estudiantes, roles, estadoSeleccion);
            var rolSeleccionado = SeleccionarRolOptimo(estudianteSeleccionado, roles, estadoSeleccion);

            var seleccion = new Seleccion(estudianteSeleccionado, rolSeleccionado);
            
            // Actualizar estado y historial
            estadoSeleccion.RegistrarSeleccion(estudianteSeleccionado.Nombre, rolSeleccionado.Nombre);
            GuardarEstadoSeleccion(estadoSeleccion);
            GuardarEnHistorial(seleccion);

            return seleccion;
        }

        private Estudiante SeleccionarEstudianteOptimo(List<Estudiante> estudiantes, List<Rol> roles, EstadoSeleccion estadoSeleccion)
        {
            var conteoEstudiantes = estudiantes.ToDictionary(
                e => e,
                e => roles.Sum(r => estadoSeleccion.ObtenerVecesSeleccionado(e.Nombre, r.Nombre))
            );

            int minimoConteo = conteoEstudiantes.Values.Min();
            var estudiantesCandidatos = conteoEstudiantes
                .Where(kvp => kvp.Value == minimoConteo)
                .Select(kvp => kvp.Key)
                .ToList();

            var random = new Random();
            return estudiantesCandidatos[random.Next(estudiantesCandidatos.Count)];
        }

        private Rol SeleccionarRolOptimo(Estudiante estudiante, List<Rol> roles, EstadoSeleccion estadoSeleccion)
        {
            var conteoRoles = roles.ToDictionary(
                r => r,
                r => estadoSeleccion.ObtenerVecesSeleccionado(estudiante.Nombre, r.Nombre)
            );

            int minimoConteo = conteoRoles.Values.Min();
            var rolesCandidatos = conteoRoles
                .Where(kvp => kvp.Value == minimoConteo)
                .Select(kvp => kvp.Key)
                .ToList();

            var random = new Random();
            return rolesCandidatos[random.Next(rolesCandidatos.Count)];
        }

        private EstadoSeleccion CargarEstadoSeleccion()
        {
            var estado = new EstadoSeleccion();
            
            if (!File.Exists(_archivoEstadoSeleccion))
                return estado;

            try
            {
                var json = File.ReadAllText(_archivoEstadoSeleccion);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    var estadoDict = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, int>>>(json);
                    if (estadoDict != null)
                    {
                        estado.Estado = estadoDict;
                    }
                }
            }
            catch
            {
                // Si hay error al leer, empezar con estado limpio
            }

            return estado;
        }

        private void GuardarEstadoSeleccion(EstadoSeleccion estado)
        {
            try
            {
                var json = JsonSerializer.Serialize(estado.Estado, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_archivoEstadoSeleccion, json);
            }
            catch
            {
                // Manejar error silenciosamente
            }
        }

        private void GuardarEnHistorial(Seleccion seleccion)
        {
            var linea = $"{seleccion.FechaSeleccion:yyyy-MM-dd HH:mm:ss} - {seleccion.Estudiante.Nombre} - {seleccion.Rol.Nombre}";
            _fileManager.AgregarLinea(_archivoHistorial, linea);
        }

        public List<Seleccion> ObtenerHistorial()
        {
            var lineas = _fileManager.LeerLineas(_archivoHistorial);
            var historial = new List<Seleccion>();

            foreach (var linea in lineas.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                try
                {
                    var partes = linea.Split(" - ");
                    if (partes.Length >= 3)
                    {
                        var fecha = DateTime.Parse(partes[0]);
                        var estudiante = new Estudiante(partes[1]);
                        var rol = new Rol(partes[2]);
                        var seleccion = new Seleccion(estudiante, rol) { FechaSeleccion = fecha };
                        historial.Add(seleccion);
                    }
                }
                catch
                {
                    // Ignorar líneas mal formateadas
                }
            }

            return historial.OrderByDescending(s => s.FechaSeleccion).ToList();
        }
    }
}
