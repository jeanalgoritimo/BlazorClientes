using BlazorClientes.DTOs;
using BlazorClientes.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorClientes.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController(IClienteService service)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ClienteResponse>>>
        Pesquisar([FromQuery] string? pesquisa)
    {
        var clientes =
            await service.PesquisarAsync(pesquisa);

        return Ok(clientes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClienteResponse>>
        ObterPorId(int id)
    {
        var cliente = await service.ObterPorIdAsync(id);

        return cliente is null
            ? NotFound()
            : Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<ClienteResponse>> Adicionar(
        [FromBody] ClienteRequest request)
    {
        try
        {
            var cliente =
                await service.AdicionarAsync(request);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = cliente.Id },
                cliente);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new
            {
                mensagem = exception.Message
            });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(
        int id,
        [FromBody] ClienteRequest request)
    {
        try
        {
            var atualizado =
                await service.AtualizarAsync(id, request);

            return atualizado
                ? NoContent()
                : NotFound();
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new
            {
                mensagem = exception.Message
            });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var excluido = await service.ExcluirAsync(id);

        return excluido
            ? NoContent()
            : NotFound();
    }
}