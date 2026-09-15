using BlazorClientes.Models;

namespace BlazorClientes.Repositories;

public interface IClienteRepository
{
    Task<IReadOnlyList<Cliente>> PesquisarAsync(string? pesquisa);
    Task<Cliente?> ObterPorIdAsync(int id);
    Task<bool> EmailExisteAsync(string email, int? ignorarId = null);
    Task AdicionarAsync(Cliente cliente);
    Task AtualizarAsync(Cliente cliente);
    Task ExcluirAsync(Cliente cliente);
}