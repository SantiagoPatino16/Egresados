using System.Security.Cryptography;
using System.Text;

namespace LogicBusiness.Security
{
    public static class SecurityHelper
    {
        // Generar hash SHA256 de una cadena
        public static string GetSHA256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(texto);
                byte[] hash = sha256.ComputeHash(bytes);

                // Convertimos el hash a string en formato HEX
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }
    }
}
