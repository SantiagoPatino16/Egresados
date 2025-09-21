using System;
using System.Security.Cryptography;

namespace LogicBusiness.Security
{
    public static class SecurityHelper
    {
        // PBKDF2: hash + salt (igual que ya tenías)
        public static string HashPassword(string password, int iterations = 10000)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(password, 16, iterations, HashAlgorithmName.SHA256))
            {
                byte[] salt = rfc2898.Salt;
                byte[] hash = rfc2898.GetBytes(32); // 256 bits

                return Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
            }
        }

        // Verificación usando comparación en tiempo constante portable
        public static bool VerifyPassword(string password, string storedHash, int iterations = 10000)
        {
            if (string.IsNullOrEmpty(storedHash))
                return false;

            var parts = storedHash.Split('|');
            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] storedBytes = Convert.FromBase64String(parts[1]);

            using (var rfc2898 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                byte[] computedHash = rfc2898.GetBytes(32);

                // Usa la función portable FixedTimeEquals (evita timing attacks)
                return FixedTimeEquals(computedHash, storedBytes);
            }
        }

        // Comparación en "tiempo constante" compatible con cualquier framework
        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null) return false;
            if (a.Length != b.Length) return false;

            // XOR acumulado; no salir antes para evitar diferencias en tiempo
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }
            return diff == 0;
        }
    }
}
