using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using SeleniumExtras.PageObjects;
using TestCommon.CommonUtility;

namespace TestCommon.PageObjects
{
    public class NewOrdersPage
    {
        IWebDriver _driver;
        By _pageLoadedSelector = By.CssSelector("#tableLabel");

        public NewOrdersPage(IWebDriver webDriver)
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

        [FindsBy(How = How.XPath, Using = "//h2[text()='Patient Information']")]
        public IWebElement LblPatientInfo { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#mrn")]
        public IWebElement TxtMRN { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#first-name")]
        public IWebElement TxtFirstName { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#last-name")]
        public IWebElement TxtLastName { get; set; }

      

        //Send text to MRN input
        public NewOrdersPage WriteMRNValue(string text)
        {
            Console.WriteLine($"Send text to MRN input box");
            this.BtnCreateNew.ElementSendText(_driver, text);
            return this;
        }
    }
}
