using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Configuration;
using OpenQA.Selenium.Support.Extensions;
using TestCommon.CommonUtility;

namespace TestCommon
{
    [TestClass]
    public class BaseClass
    {
        protected IWebDriver _driver;
        private TestContext _context = null;

        public TestContext Context
        {
            get
            {
                return this._context;
            }
            set
            {
                this._context = value;
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = GetDriver();
            Console.WriteLine($"START TEST: {Context.FullyQualifiedTestClassName}.{Context.TestName}");
            Console.WriteLine("=================================================================");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            string ssFolder = Path.Combine(Context.TestLogsDir, "Screenshots");
            var screenshotFileName = $"{Path.Combine(ssFolder, Context.TestName)}.png";

            if (Context.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                // prep folder if needed
                if (Directory.Exists(ssFolder) == false)
                {
                    Directory.CreateDirectory(ssFolder);
                }

                // Take a screenshot
                _driver.TakeScreenshot(screenshotFileName);
                Console.WriteLine($"**SCREENSHOT @ '{screenshotFileName}'**");
            }
            Console.WriteLine($"END TEST: {Context.FullyQualifiedTestClassName}.{Context.TestName}");
            Console.WriteLine("=================================================================");
            Kill(_driver);
        }

        /// <summary>
        /// Get driver object
        /// </summary>
        /// <returns></returns>

        public IWebDriver GetDriver()
        {
            var targetBrowser = ConfigurationManager.AppSettings["targetbrowser"].ToLower();

            if (string.IsNullOrEmpty(targetBrowser))
            {
                try
                {
                    targetBrowser = "chrome";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }

            IWebDriver driver = null;

            switch (targetBrowser)
            {
                case "chrome":
                    driver = this.GetChromeDriver();
                    break;
                case "firefox":
                    driver = this.GetFirefoxDriver();
                    break;
                case "edge":
                    driver = this.GetEdgeDriver();
                    break;

                default:
                    driver = GetChromeDriver();
                    break;
            }
            return driver;
        }

        /// <summary>
        /// Get Chrome Driver object
        /// </summary>
        /// <returns></returns>
        private IWebDriver GetChromeDriver()
        {
            Console.WriteLine("** LAUNCHING BROWSER: Chrome **");
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddArgument("disable-infobars");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-cache");
            return new ChromeDriver(chromeOptions);
        }

        /// <summary>
        /// Get Firefox Driver object
        /// </summary>
        /// <returns></returns>
        private IWebDriver GetFirefoxDriver()
        {
            Console.WriteLine("** LAUNCHING BROWSER: Firefox **");
            return new FirefoxDriver();
        }

        /// <summary>
        /// Get Edge Driver object
        /// </summary>
        /// <returns></returns>
        private IWebDriver GetEdgeDriver()
        {
            Console.WriteLine("** LAUNCHING BROWSER: Firefox **");
            return new EdgeDriver();
        }

        /// <summary>
        /// For Quit Open browsers
        /// </summary>
        /// <param name="_driver"></param>
        public void Kill(IWebDriver _driver)
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }
    }
}

