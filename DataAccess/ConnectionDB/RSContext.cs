using Common.Entities;
using System.Data.Entity; 

namespace DataAccess.ConnectionDB
{
    public class RSContext : DbContext
    {
        // El constructor apunta a la cadena de conexión en Web.config
        public RSContext() : base("name=CadenaConexion")
        {
        }

        //Referencias de tablas
        public DbSet<AttributesUser> Usuarios { get; set; }
    }
}
