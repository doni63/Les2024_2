using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Service;

public class CartaoService
{
    private readonly SwitchSelectContext _context;
    private readonly ICartaoRepositorio _cartaoRepositorio;

    public CartaoService(SwitchSelectContext context, ICartaoRepositorio cartaoRepositorio)
    {
        _context = context;
        _cartaoRepositorio = cartaoRepositorio;
    }

    
    public string FormatarUltimosQuatroDigitos(string numeroCartao)
    {
        if (numeroCartao.Length > 4)
        {
            string ultimosQuatro = numeroCartao.Substring(numeroCartao.Length - 4);
            return "**** **** **** " + ultimosQuatro;
        }
        return numeroCartao; // Retorna o número original se tiver menos de 4 dígitos (caso raro/não esperado)
    }

    public async Task CriarCartaoAsync(CartaoViewModel model)
    {
        var cartao = new Cartao
        {
            Id = model.Id,
            ClienteId = model.ClienteId,
            NumeroCartao = model.NumeroCartao,
            CpfTitularCartao = model.CpfTitularCartao,
            TitularDoCartao = model.TitularDoCartao,
            DataValidade = new DateTime(model.AnoValidade, model.MesValidade, DateTime.DaysInMonth(model.AnoValidade, model.MesValidade)),
            CVV = model.CVV,
            TipoCartao = model.TipoCartao,
        };
        _context.Cartoes.Add(cartao);
        await _context.SaveChangesAsync();
    }

    public CartaoViewModel ObterCartaoPorId(int id)
    {
        var cartao = _cartaoRepositorio.GetCartaoPorId(id);
        if(cartao is null)
        {
            return null;
        }

        var cartaoViewModel = new CartaoViewModel
        {
            Id = cartao.Id,
            ClienteId = cartao.ClienteId,
            NumeroCartao = cartao.NumeroCartao,
            CpfTitularCartao = cartao.CpfTitularCartao,
            TitularDoCartao = cartao.TitularDoCartao,
            DataValidade = cartao.DataValidade,
            CVV = cartao.CVV,
            TipoCartao = cartao.TipoCartao,

        };
        return cartaoViewModel;
    }

    public async Task<bool> EditCartao(int id, CartaoViewModel cartaoViewModel)
    {
        var cartao = await _context.Cartoes.FirstOrDefaultAsync(c =>  c.Id == id);

        if(cartao is null)
        {
            return false;
        }

        //atualiza dados de cartao
        cartao.NumeroCartao = cartaoViewModel.NumeroCartao;
        cartao.TitularDoCartao = cartaoViewModel.TitularDoCartao;
        cartao.CpfTitularCartao = cartaoViewModel.TitularDoCartao;
        cartao.DataValidade = new DateTime(cartaoViewModel.AnoValidade, cartaoViewModel.MesValidade,
                   DateTime.DaysInMonth(cartaoViewModel.AnoValidade, cartaoViewModel.MesValidade));
        cartao.CVV = cartaoViewModel.CVV;
        cartao.TipoCartao = cartaoViewModel.TipoCartao;
        

        try
        {
            _context.Update(cartao);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Console.WriteLine($"Ocorreu um erro de concorrência: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
       
    }

    internal async Task DeleteCartao(int id)
    {
        var cartao = _cartaoRepositorio.GetCartaoPorId(id);
        if(cartao == null)
        {
            throw new Exception("Cartão não encontrado");
        }

        _context.Cartoes.Remove(cartao);
        await _context.SaveChangesAsync();
    }
}
