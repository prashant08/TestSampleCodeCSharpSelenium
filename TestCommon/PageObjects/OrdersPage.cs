using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using SeleniumExtras.PageObjects;
using TestCommon.CommonUtility;

namespace TestCommon.PageObjects
{
    public class OrdersPage
    {
        IWebDriver _driver;
        By _pageLoadedSelector = By.CssSelector("#tableLabel");

        public OrdersPage(IWebDriver webDriver)
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

 
        [FindsBy(How = How.CssSelector, Using = "#tableLabel")]
        public IWebElement BtnCreateNew { get; set; }

        //Click on Order Menu option 
        public NewOrdersPage ClickOnCreateNewButton()
        {
            Console.WriteLine($"Click on Create New button");
            this.BtnCreateNew.ElementSafeClick(_driver);
            this._driver.WaitForAjaxLoad();
            return new NewOrdersPage(_driver);
        }
    }
}
