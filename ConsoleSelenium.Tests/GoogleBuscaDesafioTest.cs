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

    private IWebElement SearchBox => _driver.FindElement(By.Name("q"));
}
