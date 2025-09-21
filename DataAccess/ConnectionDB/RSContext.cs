using Common.Attributes;
using Common.Entities;
using System.Data.Entity;


namespace DataAccess.ConnectionDB
{
    public class RSContext : DbContext
    {
        // El constructor apunta a la cadena de conexión en Web.config
        public RSContext() : base("name=CadenaConexion") { }

        //Referencias de tablas
        public DbSet<AttributesUser> Usuarios { get; set; }
        public DbSet<AttributesPublications> Publicaciones { get; set; }
        public DbSet<AttributesComments> Comentarios { get; set; }

        //Permite las relaciones y añade el "DELETE ON CASCADE" de Publicaciones --> comentarios
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Usuarios → Publicaciones (sin cascada)
            modelBuilder.Entity<AttributesPublications>()
                .HasRequired(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.IdUsuario)
                .WillCascadeOnDelete(false);

            // Publicaciones → Comentarios (puedes dejar cascada si quieres)
            modelBuilder.Entity<AttributesComments>()
                .HasRequired(c => c.Publicacion)
                .WithMany()
                .HasForeignKey(c => c.IdPublicacion)
                .WillCascadeOnDelete(true);
        }

    }
}
