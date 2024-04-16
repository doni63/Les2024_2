using SwitchSelect.Models;

namespace SwitchSelect.Repositorios.Interfaces;

public interface IEnderecoRepositorio
{
    IEnumerable<Endereco> Enderecos {  get; }
    IEnumerable<Endereco> ObterEnderecosPorCliente(int clienteId);
    Endereco GetEnderecoPorId(int id);
}
