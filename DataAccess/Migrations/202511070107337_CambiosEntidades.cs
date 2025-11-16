namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiosEntidades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "Nit", c => c.String());
            AddColumn("dbo.Usuarios", "NombreEmpresa", c => c.String());
            AddColumn("dbo.Usuarios", "DescripcionEmpresa", c => c.String());
            AddColumn("dbo.Usuarios", "UbicacionEmpresa", c => c.String());
            AddColumn("dbo.Usuarios", "PersonaRepresentante", c => c.String());
            AddColumn("dbo.Usuarios", "CargoRepresentante", c => c.String());
            AddColumn("dbo.Usuarios", "TelefonoContacto", c => c.String());
            AddColumn("dbo.Usuarios", "SectorIndustria", c => c.String());
            AddColumn("dbo.Usuarios", "CiudadEmpresa", c => c.String());
            AddColumn("dbo.Usuarios", "Verificado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "Verificado");
            DropColumn("dbo.Usuarios", "CiudadEmpresa");
            DropColumn("dbo.Usuarios", "SectorIndustria");
            DropColumn("dbo.Usuarios", "TelefonoContacto");
            DropColumn("dbo.Usuarios", "CargoRepresentante");
            DropColumn("dbo.Usuarios", "PersonaRepresentante");
            DropColumn("dbo.Usuarios", "UbicacionEmpresa");
            DropColumn("dbo.Usuarios", "DescripcionEmpresa");
            DropColumn("dbo.Usuarios", "NombreEmpresa");
            DropColumn("dbo.Usuarios", "Nit");
        }
    }
}
