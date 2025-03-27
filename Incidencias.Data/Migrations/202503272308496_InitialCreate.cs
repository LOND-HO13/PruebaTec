namespace Incidencias.Data.Migrations
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
                        Id = c.Int(nullable: false, identity: true),
                        Contenido = c.String(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Autor = c.String(nullable: false),
                        EsInterno = c.Boolean(nullable: false),
                        IncidenciaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Incidencias", t => t.IncidenciaId, cascadeDelete: true)
                .Index(t => t.IncidenciaId);
            
            CreateTable(
                "dbo.Incidencias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(nullable: false),
                        Estado = c.Int(nullable: false),
                        Prioridad = c.Int(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaActualizacion = c.DateTime(nullable: false),
                        UsuarioReportaId = c.Int(nullable: false),
                        TecnicoAsignadoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tecnicoes", t => t.TecnicoAsignadoId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioReportaId)
                .Index(t => t.UsuarioReportaId)
                .Index(t => t.TecnicoAsignadoId);
            
            CreateTable(
                "dbo.Tecnicoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Incidencias", "UsuarioReportaId", "dbo.Usuarios");
            DropForeignKey("dbo.Incidencias", "TecnicoAsignadoId", "dbo.Tecnicoes");
            DropForeignKey("dbo.Comentarios", "IncidenciaId", "dbo.Incidencias");
            DropIndex("dbo.Incidencias", new[] { "TecnicoAsignadoId" });
            DropIndex("dbo.Incidencias", new[] { "UsuarioReportaId" });
            DropIndex("dbo.Comentarios", new[] { "IncidenciaId" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Tecnicoes");
            DropTable("dbo.Incidencias");
            DropTable("dbo.Comentarios");
        }
    }
}
