using System;
using Docker.DotNet.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace TesteSwitchSelect
{
    public class TesteAdmin : IDisposable
    {
        private readonly IWebDriver driver;

        public TesteAdmin()
        {
            driver = new EdgeDriver();
        }

        public void Dispose()
        {
            driver.Quit();
        }

        [Fact]
        public void TelaListaDeCliente()
        {
            driver.Navigate().GoToUrl("https://localhost:44308/");
            driver.Manage().Window.Size = new System.Drawing.Size(1382, 736);
            driver.FindElement(By.CssSelector(".bi-list")).Click();
            driver.FindElement(By.LinkText("Área do administrador")).Click();
            driver.FindElement(By.LinkText("Gerenciar Clientes")).Click();
            
        }
    }
}
