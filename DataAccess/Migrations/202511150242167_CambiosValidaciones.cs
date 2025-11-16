namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiosValidaciones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "Nombre", c => c.String());
            DropColumn("dbo.Usuarios", "NombreCompleto");
            DropColumn("dbo.Usuarios", "NombreEmpresa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "NombreEmpresa", c => c.String());
            AddColumn("dbo.Usuarios", "NombreCompleto", c => c.String());
            DropColumn("dbo.Usuarios", "Nombre");
        }
    }
}
