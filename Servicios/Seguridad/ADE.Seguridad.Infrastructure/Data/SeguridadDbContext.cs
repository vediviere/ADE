using ADE.Seguridad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ADE.Seguridad.Infrastructure.Data;

public class SeguridadDbContext : DbContext
{
    public SeguridadDbContext(DbContextOptions<SeguridadDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Rol> Roles => Set<Rol>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rol>(e =>
        {
            e.ToTable("roles_usuarios");
            e.HasKey(r => r.Id);
            e.Property(r => r.Id).HasColumnName("id_rol");
            e.Property(r => r.Nombre).HasColumnName("rol").HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("usuarios_acceso");
            e.HasKey(u => u.Id);
            e.Property(u => u.Id).HasColumnName("id_usuario");
            e.Property(u => u.Email).HasColumnName("email").HasMaxLength(300);
            e.Property(u => u.PasswordHash).HasColumnName("password_hash").HasMaxLength(500);
            e.Property(u => u.Activo).HasColumnName("activo");
            e.Property(u => u.FechaCreacion).HasColumnName("fecha_creacion");
            e.Property(u => u.IdPersona).HasColumnName("id_persona");
            e.Property(u => u.IdRol).HasColumnName("id_rol");

            e.HasOne(u => u.Rol)
             .WithMany(r => r.Usuarios)
             .HasForeignKey(u => u.IdRol);
        });

        modelBuilder.Entity<Rol>().HasData(
            new Rol { Id = 1, Nombre = "ADMIN" },
            new Rol { Id = 2, Nombre = "DOCENTE" },
            new Rol { Id = 3, Nombre = "ESTUDIANTE" },
            new Rol { Id = 4, Nombre = "JEFATURA" }
        );
    }
}
