using BRANEF.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace BRANEF.Infra.EntityFramework;

public partial class BranefContext : DbContext
{

    public BranefContext(DbContextOptions<BranefContext> options): base(options){}

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<EmpresaPorte> EmpresaPortes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Clientes");

            entity.ToTable("Cliente");

            entity.HasIndex(e => e.Nome, "IX_Cliente").IsUnique();

            entity.Property(e => e.Nome).HasMaxLength(250);

            entity.HasOne(d => d.EmpresaPorte).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.EmpresaPorteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_EmpresaPorte");
        });

        modelBuilder.Entity<EmpresaPorte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EmpresaPortes");

            entity.ToTable("EmpresaPorte");

            entity.Property(e => e.Descricao).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
