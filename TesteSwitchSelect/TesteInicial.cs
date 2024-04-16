using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TesteSwitchSelect
{
    public class NavegandoPelaTelaHomeTest : IDisposable
    {
        public IWebDriver driver { get; private set; }
        public IDictionary<String, Object> vars { get; private set; }
        public IJavaScriptExecutor js { get; private set; }
        public void Dispose()
        {
            driver.Quit();
        }
        [Fact]
        public void NavegandoNaHomeEVerificandoTitulo()
        {
            driver = new EdgeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            driver.Navigate().GoToUrl("https://localhost:44308/");
            Assert.Contains("SwitchSelect", driver.Title);
        }

    }
}
