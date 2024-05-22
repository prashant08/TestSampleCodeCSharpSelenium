using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCommon;
using TestCommon.PageObjects;


namespace AutomationTestSampleTest.TestMethods
{
    [TestClass]
    public class OrdersTests : BaseClass
    {
        private TestContext testContext = null;
        public TestContext TestContext
        {
            get
            {
                return this.testContext;
            }
            set
            {
                this.testContext = value;
                base.Context = this.testContext;
            }
        }

        /// <summary>
        /// Scerario 1 :
        /// Navigate to https://localhost:7150/
        /// Click on Orders
        /// Verify Navigated to orders Page and Table
        /// 
        [TestMethod]
        [TestCategory("Orders")]
        public void TestOrderPageTitle()
        {
            var appURL = ConfigurationManager.AppSettings["targetAppUrl"].ToLower();

            _driver.Url = appURL;
            _driver.Navigate();

            //Create object of OrdersPage
            var orders = new OrdersPage(_driver);
        }
    }
}

