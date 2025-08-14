namespace Roulette.Infrastructure.Storage
{
    public class TextFileManager
    {
        public string[] LeerLineas(string ruta)
        {
            if (!File.Exists(ruta))
            {
                // Crear el directorio si no existe
                var directorio = Path.GetDirectoryName(ruta);
                if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }
                
                // Crear el archivo vacío
                File.Create(ruta).Dispose();
                return Array.Empty<string>();
            }

            return File.ReadAllLines(ruta);
        }

        public void EscribirLineas(string ruta, IEnumerable<string> lineas)
        {
            var directorio = Path.GetDirectoryName(ruta);
            if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            File.WriteAllLines(ruta, lineas);
        }

        public void AgregarLinea(string ruta, string linea)
        {
            var directorio = Path.GetDirectoryName(ruta);
            if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            File.AppendAllLines(ruta, new[] { linea });
        }

        public string LeerArchivo(string ruta)
        {
            return File.Exists(ruta) ? File.ReadAllText(ruta) : "";
        }

        public void EscribirArchivo(string ruta, string contenido)
        {
            var directorio = Path.GetDirectoryName(ruta);
            if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            File.WriteAllText(ruta, contenido);
        }
    }
}
