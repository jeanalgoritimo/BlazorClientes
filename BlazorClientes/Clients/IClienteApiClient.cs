using BlazorClientes.DTOs;

namespace BlazorClientes.Clients;

public interface IClienteApiClient
{
    Task<IReadOnlyList<ClienteResponse>> PesquisarAsync(
        string? pesquisa);

    Task<ClienteResponse?> ObterPorIdAsync(int id);

    Task<ClienteResponse> AdicionarAsync(
        ClienteRequest request);

    Task AtualizarAsync(
        int id,
        ClienteRequest request);

    Task<bool> ExcluirAsync(int id);
}