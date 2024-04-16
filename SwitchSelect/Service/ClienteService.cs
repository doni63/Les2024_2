using SwitchSelect.Data;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Models;
using Microsoft.EntityFrameworkCore;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Service
{
    public class ClienteService
    {
        private readonly SwitchSelectContext _context;
        private readonly IClienteRepositorio _cliRepositorio;
       

        public ClienteService(SwitchSelectContext context, IClienteRepositorio cliRepositorio)
        {
            _context = context;
            _cliRepositorio = cliRepositorio;
            
        }

        //converte cliente para clienteViewModel
        public ClienteCompletoViewModel ConverterParaClienteViewModel(Cliente cliente)
        {
            if (cliente is null) return null;

            var clienteViewModel = new ClienteCompletoViewModel
            {
                //dados cliente
                Id = cliente.Id,
                Nome = cliente.Nome,
                DataDeNascimento = cliente.DataDeNascimento,
                Email = cliente.Email,
                Genero = cliente.Genero,
                Cpf = cliente.Cpf,
                RG = cliente.RG,
                NumeroTelefone = cliente.Telefones.FirstOrDefault()?.NumeroTelefone,
                TipoTelefone = (TipoTelefone)cliente.Telefones.FirstOrDefault()?.TipoTelefone,
                DDD = cliente.Telefones.FirstOrDefault()?.DDD,
                Estado = cliente.Enderecos.FirstOrDefault()?.Bairro.Cidade.Estado.Descricao,
                Cidade = cliente.Enderecos.FirstOrDefault()?.Bairro.Cidade.Descricao,
                Bairro = cliente.Enderecos.FirstOrDefault()?.Bairro.Descricao,
                Logradouro = cliente.Enderecos.FirstOrDefault()?.Logradouro,
                Numero = cliente.Enderecos.FirstOrDefault()?.Numero,
                CEP = cliente.Enderecos.FirstOrDefault()?.CEP,
                TipoEndereco = (TipoEndereco)cliente.Enderecos.FirstOrDefault()?.TipoEndereco,
                TipoLogradouro = (TipoLogradouro)cliente.Enderecos.FirstOrDefault()?.TipoLogradouro,
                TipoResidencia = (TipoResidencia)cliente.Enderecos.FirstOrDefault()?.TipoResidencia,
                Complemento = cliente.Enderecos.FirstOrDefault()?.Complemento,
                TipoCartao = (TipoCartao)cliente.Cartoes.FirstOrDefault()?.TipoCartao,
                NumeroCartao = cliente.Cartoes.FirstOrDefault()?.NumeroCartao,
                TitularDoCartao = cliente.Cartoes.FirstOrDefault()?.TitularDoCartao,
                CpfTitularCartao = cliente.Cartoes.FirstOrDefault()?.CpfTitularCartao,
                DataValidade = (DateTime)(cliente.Cartoes.FirstOrDefault()?.DataValidade),
                CVV = cliente.Cartoes.FirstOrDefault()?.CVV,
            };
            return clienteViewModel;
        }

        //converte clientevpara clienteDadosPessoaisViewModel
        public ClienteDadosPessoaisViewModel ConverterParaClienteDadosPessoaisViewModel(Cliente cliente)
        {
            if (cliente is null)
            {
                return null;
            }
            var clienteViewModel = new ClienteDadosPessoaisViewModel
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                DataDeNascimento = cliente.DataDeNascimento,
                Email = cliente.Email,
                Genero = cliente.Genero,
                Cpf = cliente.Cpf,
                RG = cliente.RG,
                NumeroTelefone = cliente.Telefones.FirstOrDefault()?.NumeroTelefone,
                TipoTelefone = (TipoTelefone)cliente.Telefones.FirstOrDefault()?.TipoTelefone,
                DDD = cliente.Telefones.FirstOrDefault()?.DDD
            };
            return clienteViewModel;
        }

        ////criar cliente
        public async Task CriarClienteAsync(ClienteCompletoViewModel model)
        {


            var cliente = new Cliente
            {
                Nome = model.Nome,
                DataDeNascimento = model.DataDeNascimento,
                Genero = model.Genero,
                Email = model.Email,
                Cpf = model.Cpf,
                RG = model.RG,


            };

            var telefone = new Telefone
            {
                TipoTelefone = model.TipoTelefone,
                DDD = model.DDD,
                NumeroTelefone = model.NumeroTelefone,
                Cliente = cliente
            };

            var cartao = new Cartao
            {
                NumeroCartao = model.NumeroCartao,
                TitularDoCartao = model.TitularDoCartao,
                CpfTitularCartao = model.CpfTitularCartao,
                DataValidade = new DateTime(model.AnoValidade, model.MesValidade, DateTime.DaysInMonth(model.AnoValidade, model.MesValidade)),
                CVV = model.CVV,
                TipoCartao = model.TipoCartao,
                Cliente = cliente
            };

            var estado = new Estado
            {
                Descricao = model.Estado
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
                TipoEndereco = model.TipoEndereco,
                TipoLogradouro = model.TipoLogradouro,
                Logradouro = model.Logradouro,
                Numero = model.Numero,
                CEP = model.CEP,
                TipoResidencia = model.TipoResidencia,
                Complemento = model.Complemento,
                Cliente = cliente,
                Bairro = bairro
            };

            cliente.Enderecos.Add(endereco);
            cliente.Cartoes.Add(cartao);
            cliente.Telefones.Add(telefone);
            _context.Add(cliente);
            await _context.SaveChangesAsync();
        }

        //obter cliente por id para editar ou deletar
        public ClienteCompletoViewModel ObterClientePorId(int id)
        {

            var cliente = _cliRepositorio.GetPorId(id);

            if (cliente == null)
            {
                return null;
            }
            return ConverterParaClienteViewModel(cliente);
        }

        //método para editar cliente obetendo cliente por id e recebendo dados do banco, convertendo objeto cliente para objeto clienteviewmodel
        public async Task<bool> EditarClienteAsync(int id, ClienteCompletoViewModel model)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Telefones)
                .Include(c => c.Cartoes)
                .Include(c => c.Enderecos)
                .ThenInclude(e => e.Bairro)
                .ThenInclude(b => b.Cidade)
                .ThenInclude(c => c.Estado)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null) return false;

            // Atualiza as propriedades do cliente
            cliente.Nome = model.Nome;
            cliente.DataDeNascimento = model.DataDeNascimento;
            cliente.Email = model.Email;
            cliente.Genero = model.Genero;
            cliente.Cpf = model.Cpf;
            cliente.RG = model.RG;

            //Cartoes
            var cartao = cliente.Cartoes.FirstOrDefault();
            if (cartao != null)
            {
                cartao.NumeroCartao = model.NumeroCartao;
                cartao.TitularDoCartao = model.TitularDoCartao;
                cartao.CpfTitularCartao = model.CpfTitularCartao;
                cartao.DataValidade = new DateTime(model.AnoValidade, model.MesValidade,
                    DateTime.DaysInMonth(model.AnoValidade, model.MesValidade));
                cartao.CVV = model.CVV;
                cartao.TipoCartao = model.TipoCartao;
                cartao.Cliente = cliente;
            }

            //Telefones           
            var telefone = cliente.Telefones.FirstOrDefault();
            if (telefone != null)
            {
                telefone.NumeroTelefone = model.NumeroTelefone;
                telefone.TipoTelefone = model.TipoTelefone;
                telefone.DDD = model.DDD;
                telefone.Cliente = cliente;
            }
            else
            {
                // Adiciona um novo telefone
                cliente.Telefones.Add(new Telefone
                {
                    NumeroTelefone = model.NumeroTelefone,
                    DDD = model.DDD,
                    TipoTelefone = model.TipoTelefone
                });
            }
            //Enderecos

            var endereco = cliente.Enderecos.FirstOrDefault();
            if (endereco != null)
            {
                endereco.TipoResidencia = model.TipoResidencia;
                endereco.TipoLogradouro = model.TipoLogradouro;
                endereco.Logradouro = model.Logradouro;
                endereco.Numero = model.Numero;
                endereco.CEP = model.CEP;
                endereco.Cliente = cliente;

                var bairro = endereco.Bairro;
                if (bairro != null)
                {
                    bairro.Descricao = model.Bairro;
                    endereco.Bairro = bairro;

                    var cidade = bairro.Cidade;
                    if (cidade != null)
                    {
                        cidade.Descricao = model.Cidade;
                        bairro.Cidade = cidade;

                        var estado = cidade.Estado;
                        if (estado != null)
                        {
                            estado.Descricao = model.Estado;
                        }
                    }
                }
            }


            try
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id)) return false;
                else throw;
            }
        }

        public async Task<bool> EditarClienteDadosPessoais(int id, ClienteDadosPessoaisViewModel model)
        {
            var cliente = await _context.Clientes
               .Include(c => c.Telefones)
               .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente is null)
            {
                return false;
            }

            //atualiza dados de cliente
            cliente.Nome = model.Nome;
            cliente.DataDeNascimento = model.DataDeNascimento;
            cliente.Email = model.Email;
            cliente.Genero = model.Genero;
            cliente.Cpf = model.Cpf;
            cliente.RG = model.RG;

            //Telefones           
            var telefone = cliente.Telefones.FirstOrDefault();
            if (telefone != null)
            {
                telefone.NumeroTelefone = model.NumeroTelefone;
                telefone.TipoTelefone = model.TipoTelefone;
                telefone.DDD = model.DDD;
                telefone.Cliente = cliente;
            }
            else
            {
                // Adiciona um novo telefone
                cliente.Telefones.Add(new Telefone
                {
                    NumeroTelefone = model.NumeroTelefone,
                    DDD = model.DDD,
                    TipoTelefone = model.TipoTelefone
                });
            }

            try
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Erro de concorrência ao atualizar cliente: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar cliente: {ex.Message}");
                return false;
            }
        }


        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(c => c.Id == id);
        }

        public List<Cliente> ListarClientes()
        {
            return _cliRepositorio.Clientes.ToList();
        }

        public async Task<Cliente> BuscarPorCpf(string cpf)
        {
            var cliente = await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Cpf == cpf);
            return cliente;
        }

        //metodo para deletar cliente recebendo id de cliente com os dados de endereco, telefoen e cartao, mas sem os dados de bairro, cidade e estado, 
        public async Task DeleteClienteAsync(int clienteId)
        {
           
            var cliente = _cliRepositorio.GetPorId(clienteId);
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }
          
            foreach (var endereco in cliente.Enderecos)
            {
                var bairro = endereco.Bairro;
                var cidade = bairro?.Cidade;
                var estado = cidade?.Estado;

                if (estado != null) _context.Estados.Remove(estado);

            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }

        public ClienteDadosPessoaisViewModel ObterClientePorIdDadosPessoais(int id)
        {
            var cliente = _cliRepositorio.GetPorId(id);
            if (cliente == null)
            {
                return null;
            }
            return ConverterParaClienteDadosPessoaisViewModel(cliente);
        }

    }
}
