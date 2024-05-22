using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using SeleniumExtras.PageObjects;
using TestCommon.CommonUtility;
using TestCommon.PageObjects;

namespace TestCommon.PageObjectsHomePage
{
    public class HomePage
    {
        IWebDriver _driver;
        By _pageLoadedSelector = By.CssSelector("a.navbar-brand");

        public HomePage(IWebDriver webDriver)
        {
            _driver = webDriver;
            this.WaitForLoad();
            PageFactory.InitElements(webDriver, this);
        }

        public void WaitForLoad()
        {
            this._driver.WaitForAjaxLoad();
            CommonUtils.WaitForElementIsVisible(this._driver, _pageLoadedSelector);
        }

        [FindsBy(How = How.CssSelector, Using = "a.navbar-brand")]
        public IWebElement TitleBrand { get; set; }

        [FindsBy(How = How.LinkText, Using = "AutomationTestSample")]
        public IWebElement BrandTitle { get; set; }

        [FindsBy(How = How.LinkText, Using = "Home")]
        public IWebElement LinkHome { get; set; }

        [FindsBy(How = How.LinkText, Using = "Orders")]
        public IWebElement LinkOrders { get; set; }


        /// <summary>
        /// Click on 'Orders' menu option
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>      
        public OrdersPage ClickOnOrdersMenu()
        {
            Console.WriteLine($"Click on Orders button");
            this.LinkOrders.ElementSafeClick(_driver);
            this._driver.WaitForAjaxLoad();
            return new OrdersPage(_driver);
        }

    }
}
