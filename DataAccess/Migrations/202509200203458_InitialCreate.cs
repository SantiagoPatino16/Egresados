namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comentarios",
                c => new
                    {
                        IdComentario = c.Int(nullable: false, identity: true),
                        IdPublicacion = c.Int(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                        NombreUsuario = c.String(),
                        Contenido = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        IdComentarioPadre = c.Int(),
                        AttributesPublications_IdPublicacion = c.Int(),
                        AttributesComments_IdComentario = c.Int(),
                    })
                .PrimaryKey(t => t.IdComentario)
                .ForeignKey("dbo.Publicaciones", t => t.AttributesPublications_IdPublicacion)
                .ForeignKey("dbo.Publicaciones", t => t.IdPublicacion, cascadeDelete: true)
                .ForeignKey("dbo.Comentarios", t => t.AttributesComments_IdComentario)
                .ForeignKey("dbo.Usuarios", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdPublicacion)
                .Index(t => t.IdUsuario)
                .Index(t => t.AttributesPublications_IdPublicacion)
                .Index(t => t.AttributesComments_IdComentario);
            
            CreateTable(
                "dbo.Publicaciones",
                c => new
                    {
                        IdPublicacion = c.Int(nullable: false, identity: true),
                        IdUsuario = c.Int(nullable: false),
                        NombreUsuario = c.String(),
                        Contenido = c.String(),
                        Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdPublicacion)
                .ForeignKey("dbo.Usuarios", t => t.IdUsuario)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false, identity: true),
                        NombreCompleto = c.String(),
                        Correo = c.String(),
                        ClaveHash = c.String(),
                        Rol = c.String(),
                        FechaRegistro = c.DateTime(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdUsuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comentarios", "IdUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.Comentarios", "AttributesComments_IdComentario", "dbo.Comentarios");
            DropForeignKey("dbo.Comentarios", "IdPublicacion", "dbo.Publicaciones");
            DropForeignKey("dbo.Publicaciones", "IdUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.Comentarios", "AttributesPublications_IdPublicacion", "dbo.Publicaciones");
            DropIndex("dbo.Publicaciones", new[] { "IdUsuario" });
            DropIndex("dbo.Comentarios", new[] { "AttributesComments_IdComentario" });
            DropIndex("dbo.Comentarios", new[] { "AttributesPublications_IdPublicacion" });
            DropIndex("dbo.Comentarios", new[] { "IdUsuario" });
            DropIndex("dbo.Comentarios", new[] { "IdPublicacion" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Publicaciones");
            DropTable("dbo.Comentarios");
        }
    }
}
