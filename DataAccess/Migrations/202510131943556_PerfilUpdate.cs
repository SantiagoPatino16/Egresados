namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PerfilUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "ProgramaAcademico", c => c.String());
            AddColumn("dbo.Usuarios", "AnioGraduacion", c => c.Int(nullable: false));
            AddColumn("dbo.Usuarios", "EmpresaActual", c => c.String());
            AddColumn("dbo.Usuarios", "CargoActual", c => c.String());
            AddColumn("dbo.Usuarios", "CiudadResidencia", c => c.String());
            AddColumn("dbo.Usuarios", "LinkedIn", c => c.String());
            AddColumn("dbo.Usuarios", "SitioWebPersonal", c => c.String());
            AddColumn("dbo.Usuarios", "UltimaConexion", c => c.DateTime());
            AddColumn("dbo.Usuarios", "Biografia", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "Biografia");
            DropColumn("dbo.Usuarios", "UltimaConexion");
            DropColumn("dbo.Usuarios", "SitioWebPersonal");
            DropColumn("dbo.Usuarios", "LinkedIn");
            DropColumn("dbo.Usuarios", "CiudadResidencia");
            DropColumn("dbo.Usuarios", "CargoActual");
            DropColumn("dbo.Usuarios", "EmpresaActual");
            DropColumn("dbo.Usuarios", "AnioGraduacion");
            DropColumn("dbo.Usuarios", "ProgramaAcademico");
        }
    }
}
