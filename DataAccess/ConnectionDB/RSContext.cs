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
        public DbSet<AttributesCategories> Categorias { get; set; }
        public DbSet<AttributesEvents> Eventos { get; set; }
        public DbSet<AttributesFavoritesJobs> OfertasFavoritas { get; set; }
        public DbSet<AttributesJobOfferts> OfertasEmpleo { get; set; }
        public DbSet<AttributesApplications> Postulaciones { get; set; }
        public DbSet<AttributesContractType> TiposContrato { get; set; }
        public DbSet<AttributesCategoriesJobs> CategoriasEmpleo { get; set; }
        public DbSet<AttributesWorkModalities> ModalidadesTrabajo { get; set; }
        public DbSet<AttributesStatusApplication> EstadoPostulacion { get; set; }
        public DbSet<AttributesMessages> Mensajes { get; set; }
        public DbSet<AttributesMessageReactions> Chats { get; set; }
        public DbSet<AttributesMessageAttachments> Adjuntos { get; set; }




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

            modelBuilder.Entity<AttributesMessages>()
                .HasRequired(m => m.Receptor)
                .WithMany()
                .HasForeignKey(m => m.IdReceptor)
                .WillCascadeOnDelete(false);
        }

    }
}
