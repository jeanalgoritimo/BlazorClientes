using BlazorClientes.Data;
using BlazorClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorClientes.Repositories;

public class ClienteRepository(AppDbContext context)
    : IClienteRepository
{
    public async Task<IReadOnlyList<Cliente>> PesquisarAsync(
        string? pesquisa)
    {
        var query = context.Clientes
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(pesquisa))
        {
            var termo = pesquisa.Trim().ToLower();

            query = query.Where(x =>
                x.Nome.ToLower().Contains(termo) ||
                x.Email.ToLower().Contains(termo) ||
                x.Telefone.Contains(termo));
        }

        return await query
            .OrderBy(x => x.Nome)
            .ToListAsync();
    }

    public Task<Cliente?> ObterPorIdAsync(int id)
    {
        return context.Clientes
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<bool> EmailExisteAsync(
        string email,
        int? ignorarId = null)
    {
        return context.Clientes.AnyAsync(x =>
            x.Email == email &&
            (!ignorarId.HasValue || x.Id != ignorarId.Value));
    }

    public async Task AdicionarAsync(Cliente cliente)
    {
        context.Clientes.Add(cliente);
        await context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Cliente cliente)
    {
        context.Clientes.Update(cliente);
        await context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(Cliente cliente)
    {
        context.Clientes.Remove(cliente);
        await context.SaveChangesAsync();
    }
}