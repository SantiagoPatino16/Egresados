namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiosMensajeria2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Mensajes", "IdReceptor", "dbo.Usuarios");
            AddForeignKey("dbo.Mensajes", "IdReceptor", "dbo.Usuarios", "IdUsuario");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mensajes", "IdReceptor", "dbo.Usuarios");
            AddForeignKey("dbo.Mensajes", "IdReceptor", "dbo.Usuarios", "IdUsuario", cascadeDelete: true);
        }
    }
}
