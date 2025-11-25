namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiosApplication : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Postulaciones", "Egresado_IdUsuario", "dbo.Usuarios");
            DropIndex("dbo.Postulaciones", new[] { "Egresado_IdUsuario" });
            DropColumn("dbo.Postulaciones", "IdEgresado");
            RenameColumn(table: "dbo.Postulaciones", name: "Egresado_IdUsuario", newName: "IdEgresado");
            AlterColumn("dbo.Postulaciones", "IdEgresado", c => c.Int(nullable: false));
            CreateIndex("dbo.Postulaciones", "IdEgresado");
            AddForeignKey("dbo.Postulaciones", "IdEgresado", "dbo.Usuarios", "IdUsuario", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Postulaciones", "IdEgresado", "dbo.Usuarios");
            DropIndex("dbo.Postulaciones", new[] { "IdEgresado" });
            AlterColumn("dbo.Postulaciones", "IdEgresado", c => c.Int());
            RenameColumn(table: "dbo.Postulaciones", name: "IdEgresado", newName: "Egresado_IdUsuario");
            AddColumn("dbo.Postulaciones", "IdEgresado", c => c.Int(nullable: false));
            CreateIndex("dbo.Postulaciones", "Egresado_IdUsuario");
            AddForeignKey("dbo.Postulaciones", "Egresado_IdUsuario", "dbo.Usuarios", "IdUsuario");
        }
    }
}
