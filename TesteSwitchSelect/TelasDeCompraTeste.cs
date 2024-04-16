using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Xunit;


namespace TesteSwitchSelect
{
    public class SuiteTelaCompraTests : IDisposable
    {
        public IWebDriver driver { get; private set; }
        public IDictionary<string, object> vars { get; private set; }
        public IJavaScriptExecutor js { get; private set; }

        public SuiteTelaCompraTests()
        {
            driver = new EdgeDriver();
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<string, object>();
        }

        public void Dispose()
        {
            driver.Quit();
        }

        [Fact]
        public void TelasDeCompraTeste()
        {
            driver.Navigate().GoToUrl("https://localhost:44308/");
            driver.Manage().Window.Size = new System.Drawing.Size(1382, 736);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.CssSelector(".text-decoration-none > .img-fluid")).Click();
            driver.FindElement(By.Id("adicionarCarrinho")).Click();
            driver.FindElement(By.LinkText("Ver Carrinho")).Click();
            driver.FindElement(By.LinkText("Finalizar Pedido")).Click();
            driver.FindElement(By.LinkText("Continuar")).Click();
            driver.FindElement(By.LinkText("Confirmar pedido")).Click();
            driver.FindElement(By.LinkText("Continuar Comprando")).Click();
            driver.Close();
        }

        [Fact]
        public void TelasTroca()
        {
            driver.Navigate().GoToUrl("https://localhost:44308/");
            driver.Manage().Window.Size = new System.Drawing.Size(1382, 736);
            driver.FindElement(By.CssSelector(".bi-person")).Click();
            driver.FindElement(By.Id("Cpf")).Click();
            driver.FindElement(By.Id("Cpf")).SendKeys("11111111111");
            driver.FindElement(By.CssSelector("button:nth-child(4)")).Click();
            driver.FindElement(By.LinkText("Pedidos")).Click();
            driver.FindElement(By.LinkText("Trocar")).Click();
            driver.FindElement(By.LinkText("Status de troca")).Click();
            driver.FindElement(By.LinkText("Continuar navegando")).Click();
            driver.Close();
        }

        [Fact]
        public void TelasDevolucao()
        {
            driver.Navigate().GoToUrl("https://localhost:44308/");
            driver.Manage().Window.Size = new System.Drawing.Size(1382, 736);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.CssSelector(".bi-person")).Click();
            driver.FindElement(By.Id("Cpf")).Click();
            driver.FindElement(By.Id("Cpf")).SendKeys("11111111111");
            driver.FindElement(By.CssSelector("button:nth-child(4)")).Click();
            driver.FindElement(By.LinkText("Pedidos")).Click();
            driver.FindElement(By.LinkText("Devolver")).Click();
            driver.FindElement(By.LinkText("Status de devolução")).Click();
            driver.FindElement(By.LinkText("Continuar navegando")).Click();
            driver.Close();
        }
    }
}
