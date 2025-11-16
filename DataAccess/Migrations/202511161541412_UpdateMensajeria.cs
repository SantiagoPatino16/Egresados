namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMensajeria : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdjuntosMensaje",
                c => new
                    {
                        IdAdjunto = c.Int(nullable: false, identity: true),
                        IdMensaje = c.Int(nullable: false),
                        RutaArchivo = c.String(),
                        TipoMime = c.String(),
                        FechaAdjunto = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdAdjunto)
                .ForeignKey("dbo.Mensajes", t => t.IdMensaje, cascadeDelete: true)
                .Index(t => t.IdMensaje);
            
            CreateTable(
                "dbo.Mensajes",
                c => new
                    {
                        IdMensaje = c.Int(nullable: false, identity: true),
                        IdEmisor = c.Int(nullable: false),
                        IdReceptor = c.Int(nullable: false),
                        Contenido = c.String(nullable: false),
                        FechaEnvio = c.DateTime(nullable: false),
                        EliminadoPorEmisor = c.Boolean(nullable: false),
                        EliminadoPorReceptor = c.Boolean(nullable: false),
                        Editado = c.Boolean(nullable: false),
                        FechaEdicion = c.DateTime(),
                        Leido = c.Boolean(nullable: false),
                        AttributesUser_IdUsuario = c.Int(),
                        AttributesUser_IdUsuario1 = c.Int(),
                    })
                .PrimaryKey(t => t.IdMensaje)
                .ForeignKey("dbo.Usuarios", t => t.AttributesUser_IdUsuario)
                .ForeignKey("dbo.Usuarios", t => t.AttributesUser_IdUsuario1)
                .ForeignKey("dbo.Usuarios", t => t.IdEmisor, cascadeDelete: false)
                .ForeignKey("dbo.Usuarios", t => t.IdReceptor, cascadeDelete: false)
                .Index(t => t.IdEmisor)
                .Index(t => t.IdReceptor)
                .Index(t => t.AttributesUser_IdUsuario)
                .Index(t => t.AttributesUser_IdUsuario1);
            
            CreateTable(
                "dbo.ReaccionesMensaje",
                c => new
                    {
                        IdReaccion = c.Int(nullable: false, identity: true),
                        IdMensaje = c.Int(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                        TipoReaccion = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdReaccion)
                .ForeignKey("dbo.Mensajes", t => t.IdMensaje, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdMensaje)
                .Index(t => t.IdUsuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mensajes", "IdReceptor", "dbo.Usuarios");
            DropForeignKey("dbo.Mensajes", "IdEmisor", "dbo.Usuarios");
            DropForeignKey("dbo.ReaccionesMensaje", "IdUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.ReaccionesMensaje", "IdMensaje", "dbo.Mensajes");
            DropForeignKey("dbo.Mensajes", "AttributesUser_IdUsuario1", "dbo.Usuarios");
            DropForeignKey("dbo.Mensajes", "AttributesUser_IdUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.AdjuntosMensaje", "IdMensaje", "dbo.Mensajes");
            DropIndex("dbo.ReaccionesMensaje", new[] { "IdUsuario" });
            DropIndex("dbo.ReaccionesMensaje", new[] { "IdMensaje" });
            DropIndex("dbo.Mensajes", new[] { "AttributesUser_IdUsuario1" });
            DropIndex("dbo.Mensajes", new[] { "AttributesUser_IdUsuario" });
            DropIndex("dbo.Mensajes", new[] { "IdReceptor" });
            DropIndex("dbo.Mensajes", new[] { "IdEmisor" });
            DropIndex("dbo.AdjuntosMensaje", new[] { "IdMensaje" });
            DropTable("dbo.ReaccionesMensaje");
            DropTable("dbo.Mensajes");
            DropTable("dbo.AdjuntosMensaje");
        }
    }
}
