namespace Roulette.Models
{
    public class EstadoSeleccion
    {
        public Dictionary<string, Dictionary<string, int>> Estado { get; set; }

        public EstadoSeleccion()
        {
            Estado = new Dictionary<string, Dictionary<string, int>>();
        }

        public void RegistrarSeleccion(string estudiante, string rol)
        {
            if (!Estado.ContainsKey(estudiante))
            {
                Estado[estudiante] = new Dictionary<string, int>();
            }

            if (!Estado[estudiante].ContainsKey(rol))
            {
                Estado[estudiante][rol] = 0;
            }

            Estado[estudiante][rol]++;
        }

        public int ObtenerVecesSeleccionado(string estudiante, string rol)
        {
            if (Estado.ContainsKey(estudiante) && Estado[estudiante].ContainsKey(rol))
            {
                return Estado[estudiante][rol];
            }
            return 0;
        }
    }
}
