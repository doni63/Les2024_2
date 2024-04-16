using Microsoft.EntityFrameworkCore;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Service;

public class EnderecoService
{
    private readonly SwitchSelectContext _context;
    private readonly IEnderecoRepositorio _endRepositorio;
   

    public EnderecoService(SwitchSelectContext context, IEnderecoRepositorio endRepositorio)
    {
        _context = context;
        _endRepositorio = endRepositorio;
        
    }

    public async Task CriarEnderecoAsync(EnderecoViewModel model)
    {
        var estado = new Estado
        {
            Descricao = model.Estado,
        };

        var cidade = new Cidade
        {
            Descricao = model.Cidade,
            Estado = estado
        };

        var bairro = new Bairro
        {
            Descricao = model.Bairro,
            Cidade = cidade
        };

        var endereco = new Endereco
        {
            ClienteId = model.ClienteID,
            TipoEndereco = model.TipoEndereco,
            TipoResidencia = model.TipoResidencia,
            TipoLogradouro = model.TipoLogradouro,
            Logradouro = model.Logradouro,
            Numero = model.Numero,
            CEP = model.CEP,
            Complemento = model.Complemento,
            Bairro = bairro
            
        };
        
        _context.Enderecos.Add(endereco);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> EditEnderecoAsync(int id, EnderecoViewModel model)
    {
        var endereco = await _context.Enderecos
            .Include(e => e.Bairro)
                .ThenInclude(b => b.Cidade)
                    .ThenInclude(c => c.Estado)
            .FirstOrDefaultAsync(e => e.Id == id);

        if(endereco is null)
        {
            return false;
        }
        

        //atualiza dados de endereco
        endereco.TipoResidencia = model.TipoResidencia;
        endereco.TipoLogradouro = model.TipoLogradouro;
        endereco.Logradouro = model.Logradouro;
        endereco.Numero = model.Numero;
        endereco.CEP = model.CEP;
        endereco.Complemento = model.Complemento;

        var bairro = endereco.Bairro;
        if(bairro != null)
        {
            bairro.Descricao = model.Bairro;
            endereco.Bairro = bairro;

            var cidade = bairro.Cidade;
            if(cidade != null)
            {
                cidade.Descricao = model.Cidade;
                bairro.Cidade = cidade;

                var estado = cidade.Estado;
                if(estado != null)
                {
                    estado.Descricao = model.Estado;
                    cidade.Estado = estado;
                }
            }
        }
        try
        {
            _context.Update(endereco);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Console.WriteLine($"Ocorreu um erro de concorrência: {ex.Message}");
            return false;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
    public async Task DeleteEndereco(int enderecoId)
    {
        var endereco = _endRepositorio.GetEnderecoPorId(enderecoId);
        if(endereco is null)
        {
            throw new Exception("Endereco não encontrado"); 
        }

        _context.Enderecos.Remove(endereco);
        await _context.SaveChangesAsync();
    }

    public EnderecoViewModel ObterEnderecoPorId(int id)
    {
        var endereco = _endRepositorio.GetEnderecoPorId(id);
        if(endereco is null)
        {
            return null;
        }

        var endViewModel = new EnderecoViewModel
        {
            Id = endereco.Id,
            Estado = endereco.Bairro.Cidade.Estado.Descricao,
            Cidade = endereco.Bairro.Cidade.Descricao,
            Bairro = endereco.Bairro.Descricao,
            Logradouro = endereco.Logradouro,
            Numero = endereco.Numero,
            CEP = endereco.CEP,
            Complemento = endereco.Complemento,
            TipoEndereco = endereco.TipoEndereco,
            TipoLogradouro = endereco.TipoLogradouro,
            TipoResidencia = endereco.TipoResidencia
        };
        return endViewModel;
    }
}
