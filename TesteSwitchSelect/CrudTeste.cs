using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace TesteSwitchSelect
{
    public class CrudTeste : IDisposable
    {
        private readonly IWebDriver driver;

        public CrudTeste()
        {
            driver = new EdgeDriver();
        }

        public void Dispose()
        {
            driver.Quit();
        }

        [Fact]
        public void SalvarCliente()
        {
            driver.Navigate().GoToUrl("https://localhost:44308/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
            driver.FindElement(By.CssSelector(".bi-person")).Click();
            driver.FindElement(By.LinkText("Cadastre-se")).Click();

            // Preencher dados pessoais
            driver.FindElement(By.Id("Nome")).SendKeys("Teste01");
            driver.FindElement(By.Id("DataDeNascimento")).SendKeys("25/09/1980");
            driver.FindElement(By.Id("Cpf")).SendKeys("12345678987");
            driver.FindElement(By.Id("RG")).SendKeys("789654123");
            driver.FindElement(By.Id("Email")).SendKeys("teste01@email.com");
            driver.FindElement(By.Id("DDD")).SendKeys("011");
            new SelectElement(driver.FindElement(By.Id("TipoTelefone"))).SelectByText("Residencial");
            driver.FindElement(By.Id("NumeroTelefone")).SendKeys("46457898");

            // Clicar no botão 'Ir para Endereço'
            
            IWebElement btnDadosPessoais = driver.FindElement(By.Id("btnDadosPessoais"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", btnDadosPessoais);

            // Aguarde um curto período de tempo para garantir que a rolagem seja concluída (opcional)
            Thread.Sleep(500);

            // Clique no botão
            btnDadosPessoais.Click();

            // Preencher endereço
            new SelectElement(driver.FindElement(By.Id("TipoLogradouro"))).SelectByText("Rua");
            driver.FindElement(By.Id("Logradouro")).SendKeys("Americo Vespucio");
            driver.FindElement(By.Id("Numero")).SendKeys("999");
            driver.FindElement(By.Id("CEP")).SendKeys("08772001");
            new SelectElement(driver.FindElement(By.Id("TipoResidencia"))).SelectByText("Casa");
            driver.FindElement(By.Id("Bairro")).SendKeys("Vila Augusta");
            driver.FindElement(By.Id("Cidade")).SendKeys("Mogi das Cruzes");
            driver.FindElement(By.Id("Estado")).SendKeys("São Paulo");

            // Clicar no botão 'Ir para Cartão'
            driver.FindElement(By.Id("btnEndereco")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Preencher dados do cartão
            driver.FindElement(By.Id("TitularDoCartao")).SendKeys("Roberto");
            driver.FindElement(By.Id("CpfTitularCartao")).SendKeys("12345678987");
            driver.FindElement(By.Id("NumeroCartao")).SendKeys("1233211233215555");
            driver.FindElement(By.Id("MesValidade")).SendKeys("05");
            driver.FindElement(By.Id("AnoValidade")).SendKeys("2025");
            driver.FindElement(By.Id("CVV")).SendKeys("369");

            // Clicar no botão 'Salvar'
            driver.FindElement(By.Id("salvar")).Click();
           
        }

        [Fact]
        public void EditarCliente()
        {
            driver.Navigate().GoToUrl("https://localhost:44308/");
            driver.Manage().Window.Size = new System.Drawing.Size(1382, 736);
            driver.FindElement(By.CssSelector(".bi-person")).Click();
            driver.FindElement(By.Id("Cpf")).SendKeys("12345678987");
            driver.FindElement(By.CssSelector("button:nth-child(4)")).Click();
            driver.FindElement(By.LinkText("Dados pessoais")).Click();
            driver.FindElement(By.LinkText("Editar")).Click();
            IWebElement campoNome = driver.FindElement(By.Id("Nome"));
            campoNome.Clear();
            campoNome.SendKeys("Roberto da Silva");

            //// Clicar no botão 'editar'
           
            IWebElement editar = driver.FindElement(By.Id("formEditarDadosPessoais"));

            // Envie o formulário
            editar.Submit();
        }

        [Fact]
        public void CadastrarNovoEndereco()
        {
            driver.Navigate().GoToUrl("https://localhost:44308/");
            driver.Manage().Window.Size = new System.Drawing.Size(1382, 736);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.CssSelector(".bi-person")).Click();
            driver.FindElement(By.Id("Cpf")).SendKeys("12345678987");
            driver.FindElement(By.Id("login")).Click();
            driver.FindElement(By.Id("endereco")).Click();
            driver.FindElement(By.LinkText("+ Endereço")).Click();
            new SelectElement(driver.FindElement(By.Id("TipoResidencia"))).SelectByText("Casa");
            new SelectElement(driver.FindElement(By.Id("TipoLogradouro"))).SelectByText("Rua");
            driver.FindElement(By.Id("Logradouro")).SendKeys("Rua Adorno Dias");
            driver.FindElement(By.Id("Numero")).SendKeys("50");
            driver.FindElement(By.Id("Complemento")).SendKeys("Casa B");
            driver.FindElement(By.Id("CEP")).SendKeys("08593194");
            driver.FindElement(By.Id("Bairro")).SendKeys("Piatã");
            driver.FindElement(By.Id("Cidade")).SendKeys("Mogi das Cruzes");
            driver.FindElement(By.Id("Estado")).SendKeys("São Paulo");
          //submit novoEndereco
            IWebElement novoEndereco = driver.FindElement(By.Id("formCreateNovoEndereco"));
            // Envie o formulário
            novoEndereco.Submit();
        }

        [Fact]
        public void ExcluirEndereco()
        {
            driver.Navigate().GoToUrl("https://localhost:44308/");
            driver.Manage().Window.Size = new System.Drawing.Size(1382, 736);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.CssSelector(".bi-person")).Click();
            driver.FindElement(By.Id("Cpf")).SendKeys("12345678987");
            driver.FindElement(By.Id("login")).Click();
            driver.FindElement(By.Id("endereco")).Click();
            driver.FindElement(By.CssSelector(".list-group-item:nth-child(2) .btn-danger")).Click();

            //submit deletarEndereco
            IWebElement deletarEndereco = driver.FindElement(By.Id("formDeletarEndereco"));

            // Envie o formulário
            deletarEndereco.Submit();
        }

        [Fact]
        public void EditarEndereco()
        {
            driver.Navigate().GoToUrl("https://localhost:44308/");
            driver.Manage().Window.Size = new System.Drawing.Size(1382, 736);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.CssSelector(".bi-person")).Click();
            driver.FindElement(By.Id("Cpf")).SendKeys("12345678987");
            driver.FindElement(By.Id("login")).Click();
            driver.FindElement(By.Id("endereco")).Click();
            driver.FindElement(By.LinkText("Editar")).Click();
            IWebElement campoNumero = driver.FindElement(By.Id("Numero"));
            campoNumero.Clear();
            campoNumero.SendKeys("360");
            IWebElement campoComplemento = driver.FindElement(By.Id("Complemento"));
            campoComplemento.Clear();
            campoComplemento.SendKeys("casa B");

            //submit deletarEndereco
            IWebElement editarEndereco = driver.FindElement(By.Id("formEditarEndereco"));

            // Envie o formulário
            editarEndereco.Submit();
        }

        [Fact]
        public void CadastrarNovoCartao()
        {
            driver.Navigate().GoToUrl("https://localhost:44308/");
            driver.Manage().Window.Size = new System.Drawing.Size(1382, 736);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.CssSelector(".bi-person")).Click();
            driver.FindElement(By.Id("Cpf")).SendKeys("12345678987");
            driver.FindElement(By.Id("login")).Click();
            driver.FindElement(By.Id("cartao")).Click();
            driver.FindElement(By.LinkText("+ Cartão")).Click();
            driver.FindElement(By.Id("NumeroCartao")).SendKeys("1234567898765432");
            driver.FindElement(By.Id("TitularDoCartao")).SendKeys("Lucrecia");
            driver.FindElement(By.Id("CpfTitularCartao")).SendKeys("98798798798");
            IWebElement mesValidade = driver.FindElement(By.Id("MesValidade"));
            mesValidade.Clear();
            mesValidade.SendKeys("7");
            //driver.FindElement(By.Id("MesValidade")).SendKeys("7");
            IWebElement anoValidade = driver.FindElement(By.Id("AnoValidade"));
            anoValidade.Clear();
            anoValidade.SendKeys("2027");
           // driver.FindElement(By.Id("AnoValidade")).SendKeys("2027");
            driver.FindElement(By.Id("CVV")).SendKeys("777");

            //submit deletarEndereco
            IWebElement novoCartao = driver.FindElement(By.Id("formNovoCartao"));

            // Envie o formulário
            novoCartao.Submit();
        }

        [Fact]
        public void DeletarCartao()
        {
            driver.Navigate().GoToUrl("https://localhost:44308/");
            driver.Manage().Window.Size = new System.Drawing.Size(1382, 736);
            driver.FindElement(By.CssSelector(".bi-person")).Click();
            driver.FindElement(By.Id("Cpf")).SendKeys("12345678987");
            driver.FindElement(By.Id("login")).Click();
            driver.FindElement(By.Id("cartao")).Click();
            driver.FindElement(By.CssSelector(".list-group-item:nth-child(2) #deletar")).Click();

            //submit deletarEndereco
            IWebElement deletarCartao = driver.FindElement(By.Id("formDeletarCartao"));

            // Envie o formulário
            deletarCartao.Submit();
        }
        [Fact]
        public void DeletarCliente()
        {
            driver.Navigate().GoToUrl("https://localhost:44308/");
            driver.Manage().Window.Size = new System.Drawing.Size(1382, 736);
            driver.FindElement(By.CssSelector(".bi-person")).Click();
            driver.FindElement(By.Id("Cpf")).SendKeys("12345678987");
            driver.FindElement(By.Id("login")).Click();
            driver.FindElement(By.LinkText("Dados pessoais")).Click();
            driver.FindElement(By.LinkText("Excluir minha conta")).Click();

            //submit deletarEndereco
            IWebElement deletarCliente = driver.FindElement(By.Id("formDeletarCliente"));

            // Envie o formulário
            deletarCliente.Submit();
        }
    }
}
