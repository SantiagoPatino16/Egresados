namespace DataAccess.Migrations
{
    using Common.Attributes;
    using Common.Entities;
    using DataAccess.ConnectionDB;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.ConnectionDB.RSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RSContext context)
        {
            // --- Categorías por defecto ---
            var categorias = new List<AttributesCategories>
            {
                new AttributesCategories { IdCategoria = 1, Nombre = "General", Descripcion = "Categoría por defecto" },
                new AttributesCategories { IdCategoria = 2, Nombre = "Tecnología", Descripcion = "Publicaciones relacionadas con avances tecnológicos" },
                new AttributesCategories { IdCategoria = 3, Nombre = "Política", Descripcion = "Publicaciones sobre temas políticos" },
                new AttributesCategories { IdCategoria = 4, Nombre = "Social", Descripcion = "Publicaciones sobre temas sociales" },
                new AttributesCategories { IdCategoria = 5, Nombre = "Entretenimiento", Descripcion = "Noticias y novedades de entretenimiento" }
            };

            foreach (var cat in categorias)
            {
                if (!context.Categorias.Any(c => c.IdCategoria == cat.IdCategoria))
                {
                    context.Categorias.Add(cat);
                }
            }

            // --- Usuario administrador por defecto ---
            if (!context.Usuarios.Any(u => u.Correo == "Administrador@mastercontrol.es"))
            {
                var admin = new AttributesUser
                {
                    Nombre = "Administrador",
                    Correo = "Administrador@mastercontrol.es",
                    ClaveHash = HashPasswordSeed("89010362988"), // 👈 Usa versión local
                    Rol = "Administrador",
                    FechaRegistro = DateTime.Now,
                    Activo = true,
                    FotoPerfil = "/Uploads/Perfiles/default.png"
                };
                context.Usuarios.Add(admin);
            }
            context.SaveChanges();
        }

        // Mini versión local del SecurityHelper
        private string HashPasswordSeed(string password, int iterations = 10000)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(password, 16, iterations, HashAlgorithmName.SHA256))
            {
                byte[] salt = rfc2898.Salt;
                byte[] hash = rfc2898.GetBytes(32);
                return Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
            }
        }
    }
}
