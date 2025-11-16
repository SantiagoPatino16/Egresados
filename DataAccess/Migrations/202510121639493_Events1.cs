namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Events1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Eventos",
                c => new
                    {
                        IdEvento = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(maxLength: 255),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(),
                        Lugar = c.String(maxLength: 100),
                        Tipo = c.String(maxLength: 50),
                        Organizador = c.String(maxLength: 100),
                        Activo = c.Boolean(nullable: false),
                        RutaImagen = c.String(),
                    })
                .PrimaryKey(t => t.IdEvento);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Eventos");
        }
    }
}
