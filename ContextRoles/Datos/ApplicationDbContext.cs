using System;
using System.Collections.Generic;
using AutenticacionNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace AutenticacionNetCore.Datos;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GRUPO> GRUPOs { get; set; }

    public virtual DbSet<PROGRAMA> PROGRAMAs { get; set; }

    public virtual DbSet<REL_ROL_GRUPO> REL_ROL_GRUPOs { get; set; }

    public virtual DbSet<ROLE> ROLEs { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GRUPO>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.ID_PROGRAMANavigation).WithMany(p => p.GRUPOs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GRUPOS_PROGRAMAS");
        });

        modelBuilder.Entity<PROGRAMA>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
        });

        modelBuilder.Entity<REL_ROL_GRUPO>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.ID_GRUPONavigation).WithMany(p => p.REL_ROL_GRUPOs).HasConstraintName("FK_REL_ROL_GRUPO_GRUPOS");

            entity.HasOne(d => d.ID_PROGRAMANavigation).WithMany(p => p.REL_ROL_GRUPOs).HasConstraintName("FK_REL_ROL_GRUPO_PROGRAMAS");

            entity.HasOne(d => d.ID_ROLNavigation).WithMany(p => p.REL_ROL_GRUPOs).HasConstraintName("FK_REL_ROL_GRUPO_ROLES");
        });

        modelBuilder.Entity<ROLE>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.ID_PROGRAMANavigation).WithMany(p => p.ROLEs).HasConstraintName("FK_ROLES_PROGRAMAS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
