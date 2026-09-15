using BlazorClientes.DTOs;

namespace BlazorClientes.Services;

public interface IClienteService
{
    Task<IReadOnlyList<ClienteResponse>> PesquisarAsync(
        string? pesquisa);

    Task<ClienteResponse?> ObterPorIdAsync(int id);

    Task<ClienteResponse> AdicionarAsync(
        ClienteRequest request);

    Task<bool> AtualizarAsync(
        int id,
        ClienteRequest request);

    Task<bool> ExcluirAsync(int id);
}