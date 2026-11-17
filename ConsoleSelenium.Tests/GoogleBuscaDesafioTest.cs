using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ConsoleSelenium.Tests;

[TestClass]
public sealed class GoogleBuscaDesafioTest
{
    private const string BaseUrl = "https://duckduckgo.com";
    private const string UserAgent =
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) " +
        "Chrome/131.0.0.0 Safari/537.36";

    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(15);

    private IWebDriver _driver = null!;
    private WebDriverWait _wait = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-blink-features=AutomationControlled");
        options.AddArgument("--disable-notifications");
        options.AddArgument($"--user-agent={UserAgent}");

        _driver = new ChromeDriver(options);
        _driver.Manage().Timeouts().PageLoad = DefaultTimeout;
        _wait = new WebDriverWait(_driver, DefaultTimeout);

        _driver.Navigate().GoToUrl(BaseUrl);
    }

    [TestCleanup]
    public void TearDown()
    {
        _driver?.Quit();
        _driver?.Dispose();
    }

    [TestMethod]
    public void Exercicio1_PesquisaBasica_DeveValidarResultados()
    {
        const string termo = "Selenium WebDriver C#";

        SearchBox.SendKeys(termo + Keys.Enter);

        _wait.Until(driver =>
            driver.Url.Contains("q=", StringComparison.OrdinalIgnoreCase) ||
            driver.Title.Contains(termo, StringComparison.OrdinalIgnoreCase));

        var urlAtual = _driver.Url;
        var tituloAtual = _driver.Title;

        Assert.IsTrue(
            urlAtual.Contains("q=", StringComparison.OrdinalIgnoreCase) ||
            tituloAtual.Contains(termo, StringComparison.OrdinalIgnoreCase),
            $"A pagina de resultados nao foi carregada. URL: {urlAtual} | Titulo: {tituloAtual}");
    }

    [TestMethod]
    public void Exercicio2_Autocompletar_DeveSelecionarTerceiraSugestao()
    {
        const string termo = "C# unit testing";
        const string seletorSugestoes =
            ".search-suggestions-list li, .sbox-suggestions li, [role='option']";

        SearchBox.SendKeys(termo);

        var terceiraSugestao = _wait.Until(driver =>
        {
            var sugestoes = driver.FindElements(By.CssSelector(seletorSugestoes));
            return sugestoes.Count >= 3 ? sugestoes[2] : null;
        });

        Assert.IsNotNull(terceiraSugestao, "A lista de sugestoes nao exibiu ao menos tres opcoes.");

        var textoSugestao = terceiraSugestao.Text;
        terceiraSugestao.Click();

        _wait.Until(driver => driver.Url.Contains("q=", StringComparison.OrdinalIgnoreCase));

        Assert.IsTrue(
            _driver.Url.Contains("q=", StringComparison.OrdinalIgnoreCase),
            $"A navegacao para os resultados de '{textoSugestao}' nao ocorreu. URL: {_driver.Url}");
    }

    private IWebElement SearchBox => _driver.FindElement(By.Name("q"));
}
