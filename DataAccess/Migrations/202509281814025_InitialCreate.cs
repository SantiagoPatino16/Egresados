namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        IdCategoria = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.IdCategoria);
            
            CreateTable(
                "dbo.Publicaciones",
                c => new
                    {
                        IdPublicacion = c.Int(nullable: false, identity: true),
                        IdUsuario = c.Int(nullable: false),
                        NombreUsuario = c.String(),
                        Contenido = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        IdCategoria = c.Int(),
                    })
                .PrimaryKey(t => t.IdPublicacion)
                .ForeignKey("dbo.Categorias", t => t.IdCategoria)
                .ForeignKey("dbo.Usuarios", t => t.IdUsuario)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdCategoria);
            
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
                        AttributesComments_IdComentario = c.Int(),
                        AttributesPublications_IdPublicacion = c.Int(),
                    })
                .PrimaryKey(t => t.IdComentario)
                .ForeignKey("dbo.Publicaciones", t => t.IdPublicacion, cascadeDelete: true)
                .ForeignKey("dbo.Comentarios", t => t.AttributesComments_IdComentario)
                .ForeignKey("dbo.Usuarios", t => t.IdUsuario, cascadeDelete: true)
                .ForeignKey("dbo.Publicaciones", t => t.AttributesPublications_IdPublicacion)
                .Index(t => t.IdPublicacion)
                .Index(t => t.IdUsuario)
                .Index(t => t.AttributesComments_IdComentario)
                .Index(t => t.AttributesPublications_IdPublicacion);
            
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
                        FotoPerfil = c.String(),
                    })
                .PrimaryKey(t => t.IdUsuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Publicaciones", "IdUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.Comentarios", "AttributesPublications_IdPublicacion", "dbo.Publicaciones");
            DropForeignKey("dbo.Comentarios", "IdUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.Comentarios", "AttributesComments_IdComentario", "dbo.Comentarios");
            DropForeignKey("dbo.Comentarios", "IdPublicacion", "dbo.Publicaciones");
            DropForeignKey("dbo.Publicaciones", "IdCategoria", "dbo.Categorias");
            DropIndex("dbo.Comentarios", new[] { "AttributesPublications_IdPublicacion" });
            DropIndex("dbo.Comentarios", new[] { "AttributesComments_IdComentario" });
            DropIndex("dbo.Comentarios", new[] { "IdUsuario" });
            DropIndex("dbo.Comentarios", new[] { "IdPublicacion" });
            DropIndex("dbo.Publicaciones", new[] { "IdCategoria" });
            DropIndex("dbo.Publicaciones", new[] { "IdUsuario" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Comentarios");
            DropTable("dbo.Publicaciones");
            DropTable("dbo.Categorias");
        }
    }
}
