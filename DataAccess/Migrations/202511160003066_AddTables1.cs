namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OfertasEmpleo",
                c => new
                    {
                        IdOferta = c.Int(nullable: false, identity: true),
                        IdEmpresa = c.Int(nullable: false),
                        IdTipoContrato = c.Int(nullable: false),
                        IdModalidadTrabajo = c.Int(nullable: false),
                        IdCategoria = c.Int(nullable: false),
                        IdPostulacion = c.Int(nullable: false),
                        IdFavorito = c.Int(nullable: false),
                        Titulo = c.String(),
                        Descripcion = c.String(),
                        Requisitos = c.String(),
                        Salario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ciudad = c.String(),
                        FechaPublicacion = c.DateTime(nullable: false),
                        FechaCierre = c.DateTime(nullable: false),
                        Estado = c.Int(nullable: false),
                        Empresa_IdUsuario = c.Int(),
                    })
                .PrimaryKey(t => t.IdOferta)
                .ForeignKey("dbo.CategoriasEmpleo", t => t.IdCategoria, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.Empresa_IdUsuario)
                .ForeignKey("dbo.ModalidadesTrabajo", t => t.IdModalidadTrabajo, cascadeDelete: true)
                .ForeignKey("dbo.TiposContrato", t => t.IdTipoContrato, cascadeDelete: true)
                .Index(t => t.IdTipoContrato)
                .Index(t => t.IdModalidadTrabajo)
                .Index(t => t.IdCategoria)
                .Index(t => t.Empresa_IdUsuario);
            
            CreateTable(
                "dbo.CategoriasEmpleo",
                c => new
                    {
                        IdCategoria = c.Int(nullable: false, identity: true),
                        Categoria = c.String(),
                    })
                .PrimaryKey(t => t.IdCategoria);
            
            CreateTable(
                "dbo.OfertasFavoritas",
                c => new
                    {
                        IdFavorito = c.Int(nullable: false, identity: true),
                        IdEgresado = c.Int(nullable: false),
                        IdOferta = c.Int(nullable: false),
                        FechaGuardado = c.DateTime(nullable: false),
                        Egresado_IdUsuario = c.Int(),
                    })
                .PrimaryKey(t => t.IdFavorito)
                .ForeignKey("dbo.Usuarios", t => t.Egresado_IdUsuario)
                .ForeignKey("dbo.OfertasEmpleo", t => t.IdOferta, cascadeDelete: true)
                .Index(t => t.IdOferta)
                .Index(t => t.Egresado_IdUsuario);
            
            CreateTable(
                "dbo.ModalidadesTrabajo",
                c => new
                    {
                        IdModalidadTrabajo = c.Int(nullable: false, identity: true),
                        Modalidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdModalidadTrabajo);
            
            CreateTable(
                "dbo.Postulaciones",
                c => new
                    {
                        IdPostulacion = c.Int(nullable: false, identity: true),
                        IdOferta = c.Int(nullable: false),
                        IdEgresado = c.Int(nullable: false),
                        FechaPostulacion = c.DateTime(nullable: false),
                        EstadoFinal = c.Int(nullable: false),
                        Mensaje = c.String(),
                        CvUrl = c.String(),
                        Egresado_IdUsuario = c.Int(),
                    })
                .PrimaryKey(t => t.IdPostulacion)
                .ForeignKey("dbo.Usuarios", t => t.Egresado_IdUsuario)
                .ForeignKey("dbo.OfertasEmpleo", t => t.IdOferta, cascadeDelete: true)
                .Index(t => t.IdOferta)
                .Index(t => t.Egresado_IdUsuario);
            
            CreateTable(
                "dbo.TiposContrato",
                c => new
                    {
                        IdTipoContrato = c.Int(nullable: false, identity: true),
                        TipoContrato = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdTipoContrato);
            
            CreateTable(
                "dbo.EstadoPostulacion",
                c => new
                    {
                        IdEstadoPostulacion = c.Int(nullable: false, identity: true),
                        IdPostulacion = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                        FechaPostulacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdEstadoPostulacion)
                .ForeignKey("dbo.Postulaciones", t => t.IdPostulacion, cascadeDelete: true)
                .Index(t => t.IdPostulacion);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EstadoPostulacion", "IdPostulacion", "dbo.Postulaciones");
            DropForeignKey("dbo.OfertasEmpleo", "IdTipoContrato", "dbo.TiposContrato");
            DropForeignKey("dbo.Postulaciones", "IdOferta", "dbo.OfertasEmpleo");
            DropForeignKey("dbo.Postulaciones", "Egresado_IdUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.OfertasEmpleo", "IdModalidadTrabajo", "dbo.ModalidadesTrabajo");
            DropForeignKey("dbo.OfertasFavoritas", "IdOferta", "dbo.OfertasEmpleo");
            DropForeignKey("dbo.OfertasFavoritas", "Egresado_IdUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.OfertasEmpleo", "Empresa_IdUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.OfertasEmpleo", "IdCategoria", "dbo.CategoriasEmpleo");
            DropIndex("dbo.EstadoPostulacion", new[] { "IdPostulacion" });
            DropIndex("dbo.Postulaciones", new[] { "Egresado_IdUsuario" });
            DropIndex("dbo.Postulaciones", new[] { "IdOferta" });
            DropIndex("dbo.OfertasFavoritas", new[] { "Egresado_IdUsuario" });
            DropIndex("dbo.OfertasFavoritas", new[] { "IdOferta" });
            DropIndex("dbo.OfertasEmpleo", new[] { "Empresa_IdUsuario" });
            DropIndex("dbo.OfertasEmpleo", new[] { "IdCategoria" });
            DropIndex("dbo.OfertasEmpleo", new[] { "IdModalidadTrabajo" });
            DropIndex("dbo.OfertasEmpleo", new[] { "IdTipoContrato" });
            DropTable("dbo.EstadoPostulacion");
            DropTable("dbo.TiposContrato");
            DropTable("dbo.Postulaciones");
            DropTable("dbo.ModalidadesTrabajo");
            DropTable("dbo.OfertasFavoritas");
            DropTable("dbo.CategoriasEmpleo");
            DropTable("dbo.OfertasEmpleo");
        }
    }
}
