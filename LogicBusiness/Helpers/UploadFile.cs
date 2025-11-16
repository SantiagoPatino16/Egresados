using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicBusiness.Helpers
{
    public class UploadFile
    {
        // Tamaño máximo (2 MB en este ejemplo)
        private const int MAX_FILE_SIZE = 2 * 1024 * 1024;

        // Extensiones permitidas
        private static readonly string[] EXTENSIONES_PERMITIDAS = { ".jpg", ".jpeg", ".png"}; //Formatos de imágenes estáticas.

        public static string GuardarImagen(HttpPostedFile archivo, string categoria, string nombrePersonalizado = null)
        {
            // Validar si se recibió un archivo
            if (archivo == null || archivo.ContentLength == 0)
                throw new Exception("No se recibió ningún archivo.");

            // Validar tamaño
            if (archivo.ContentLength > MAX_FILE_SIZE)
                throw new Exception("El archivo excede el tamaño máximo permitido (2 MB).");

            // Validar extensión
            string extension = Path.GetExtension(archivo.FileName).ToLower();
            if (Array.IndexOf(EXTENSIONES_PERMITIDAS, extension) < 0)
                throw new Exception("Tipo de archivo no permitido. Solo se aceptan: JPG, JPEG, PNG.");

            // Carpeta base en Resources
            string carpetaBase = "~/Resources/";
            string carpeta = carpetaBase + categoria.ToLower() + "/";

            // Crear carpeta si no existe
            string rutaCarpeta = HttpContext.Current.Server.MapPath(carpeta);
            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            // Nombre del archivo
            string nombreArchivo = !string.IsNullOrEmpty(nombrePersonalizado)
                                    ? nombrePersonalizado + extension
                                    : Path.GetFileName(archivo.FileName);

            string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            // Borrar cualquier archivo existente con el mismo nombre base, sin importar la extensión
            string nombreBase = Path.GetFileNameWithoutExtension(nombreArchivo);
            string[] archivosExistentes = Directory.GetFiles(rutaCarpeta, nombreBase + ".*");
            foreach (string a in archivosExistentes)
            {
                File.Delete(a);
            }

            // Guardar archivo
            archivo.SaveAs(rutaCompleta);

            // Retornar ruta relativa para almacenar la ruta en la BD o usar en la aplicación
            return VirtualPathUtility.ToAbsolute(carpeta + nombreArchivo);
        }

    }
}
