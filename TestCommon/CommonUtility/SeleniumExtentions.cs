using System;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static System.Net.Mime.MediaTypeNames;

namespace TestCommon.CommonUtility
{
    public static class SeleniumExtentions
    {
        /// <summary>
        /// Sends a "test" to the target element , clear the exiting value & send text
        /// For all other browsers it uses the standard Element.Click()
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="driver">IWebDriver</param>
        /// <param name="text">string</param>
        public static void ElementSendText(this IWebElement element, IWebDriver driver, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }


        /// <summary>
        /// Sends a "Click" to the target element via JavaScript when running via IE browser
        /// For all other browsers it uses the standard Element.Click()
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="driver">IWebDriver</param>
        public static void ElementSafeClick(this IWebElement element, IWebDriver driver)
        {
            if (driver is OpenQA.Selenium.Firefox.FirefoxDriver || driver is OpenQA.Selenium.Edge.EdgeDriver)
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click();", element);
            }
            else
            {
                element.Click();
            }
        }

        /// <summary>
        /// Select dropdown value by Value
        /// </summary>
        /// <param name="element"></param>
        /// <param name="driver"></param>
        /// <param name="value"></param>
        public static void ElementSelectByValue(this IWebElement element, IWebDriver driver, string value)
        {
            SelectElement dropDown = new SelectElement(element);
            dropDown.SelectByValue(value);
        }

        /// <summary>
        /// Select dropdown value by Index
        /// </summary>
        /// <param name="element"></param>
        /// <param name="driver"></param>
        /// <param name="value"></param>
        public static void ElementSelectByIndex(this IWebElement element, IWebDriver driver, int index)
        {
            SelectElement dropDown = new SelectElement(element);
            dropDown.SelectByIndex(index);
        }

        /// <summary>
        /// Select dropdown value by Text
        /// </summary>
        /// <param name="element"></param>
        /// <param name="driver"></param>
        /// <param name="value"></param>
        public static void ElementSelectByText(this IWebElement element, IWebDriver driver, string text)
        {
            SelectElement dropDown = new SelectElement(element);
            dropDown.SelectByText(text);
        }

        /// <summary>
        /// Take a screenshot of the current page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="FileLocationName">Full directory plus the file name and format, e.g C:\Temp\Image.jpeg</param>
        /// <param name="imageFormat"Screenshot Image Format</param>
        public static void TakeScreenshot(this IWebDriver driver, string FileLocationName)
        {
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(FileLocationName);
        }

        /// <summary>
        /// Scroll down page horizontally
        /// </summary>
        public static void ScrollHorizontally(this IWebDriver webDriver, int msToWaitForAjax)
        {
            ((IJavaScriptExecutor)webDriver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
        }

        /// <summary>
        /// If ajax available on Page, it Waits for AJAX calls to be Complete Load. 
        /// Also handles conditions : If Ajax call already loaded or not available on page.
        /// </summary>
        /// <param name="webDriver">The IWebDriver</param>
        /// <param name="timeoutMs">Max number of seconds to wait (default=5000)</param>
        /// <returns></returns>
        public static bool WaitForAjaxLoad(this IWebDriver webDriver, int timeoutMs = 5000)
        {
            var startTime = DateTime.Now;
            bool statuswaitForAjax = false;
            try
            {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(timeoutMs));
                statuswaitForAjax = wait.Until(d => (bool)((IJavaScriptExecutor)d).ExecuteScript("return jQuery.active == 0"));
                var endTime = DateTime.Now;
                var elapsedTime = endTime - startTime;
                return statuswaitForAjax;
            }
            catch (Exception)
            {
                // Some time jQuery is not available on page or already loaded so exception occured in this case and TestScript get fail.
                // If no jQuery present or already loaded -return true
                return true;
            }
        }
    }
}