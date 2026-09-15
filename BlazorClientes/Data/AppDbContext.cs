using BlazorClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorClientes.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var cliente = modelBuilder.Entity<Cliente>();

        cliente.ToTable("clientes");

        cliente.HasKey(x => x.Id);

        cliente.Property(x => x.Id)
            .HasColumnName("id");

        cliente.Property(x => x.Nome)
            .HasColumnName("nome")
            .HasMaxLength(100)
            .IsRequired();

        cliente.Property(x => x.Email)
            .HasColumnName("email")
            .HasMaxLength(150)
            .IsRequired();

        cliente.Property(x => x.Telefone)
            .HasColumnName("telefone")
            .HasMaxLength(20)
            .IsRequired();

        cliente.Property(x => x.Ativo)
            .HasColumnName("ativo");

        cliente.Property(x => x.DataCadastro)
            .HasColumnName("data_cadastro");

        cliente.HasIndex(x => x.Email)
            .IsUnique();
    }
}