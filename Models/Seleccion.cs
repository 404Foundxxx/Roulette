namespace Roulette.Models
{
    public class Seleccion
    {
        public Estudiante Estudiante { get; set; }
        public Rol Rol { get; set; }
        public DateTime FechaSeleccion { get; set; }

        public Seleccion(Estudiante estudiante, Rol rol)
        {
            Estudiante = estudiante;
            Rol = rol;
            FechaSeleccion = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Estudiante.Nombre} - {Rol.Nombre} ({FechaSeleccion:dd/MM/yyyy HH:mm})";
        }
    }
}
