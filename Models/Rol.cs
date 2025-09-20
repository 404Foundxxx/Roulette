namespace Roulette.Models
{
    public class Rol
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Rol(string nombre, string descripcion = "")
        {
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public override string ToString()
        {
            return Nombre;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Rol rol)
            {
                return Nombre.Equals(rol.Nombre, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Nombre.GetHashCode(StringComparison.OrdinalIgnoreCase);
        }
    }
}
