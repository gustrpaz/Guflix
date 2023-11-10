using System;
using System.Collections.Generic;
using Guflix.webAPI.Domains;
using Microsoft.EntityFrameworkCore;

namespace Guflix.webAPI.Contexts;

public partial class GuflixContext : DbContext
{
    public GuflixContext()
    {
    }

    public GuflixContext(DbContextOptions<GuflixContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Filme> Filmes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-Q4CU27L\\SQLEXPRESS; initial catalog=GUFLIX; user Id=sa; pwd=senai@132;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filme>(entity =>
        {
            entity.HasKey(e => e.IdFilme).HasName("PK__Filme__6E8F2A76C463CA71");

            entity.ToTable("Filme");

            entity.Property(e => e.Genero)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.NomeFilme)
                .HasMaxLength(70)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Usuario__B7C926387FCADB09");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534EE776C09").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Passwd)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(256)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
