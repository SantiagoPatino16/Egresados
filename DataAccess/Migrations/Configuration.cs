namespace DataAccess.Migrations
{
    using Common.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.ConnectionDB.RSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccess.ConnectionDB.RSContext context)
        {
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

            context.SaveChanges();
        }
    }
}
