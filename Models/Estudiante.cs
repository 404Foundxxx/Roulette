namespace Roulette.Models
{
    public class Estudiante
    {
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Estudiante(string nombre)
        {
            Nombre = nombre;
            FechaRegistro = DateTime.Now;
        }

        public override string ToString()
        {
            return Nombre;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Estudiante estudiante)
            {
                return Nombre.Equals(estudiante.Nombre, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Nombre.GetHashCode(StringComparison.OrdinalIgnoreCase);
        }
    }
}
