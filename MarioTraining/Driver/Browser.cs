using CoreFramework.Logger;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CoreFramework.Driver
{ 
    public class Browser
    {
        #region Variables

        /*
        public
        internal
        protected 
        private
        */

        /*
        private WebDriver driver;
        public WebDriver Driver { get => driver; set => driver = value; } //Way 1: Use property*
        */

        public WebDriver WebBrowser { get; set; } //Way 2: Shorter All in one

        #endregion Variables

        

        #region Methods

        public void InitializeBrowser(BrowserTarget targetBrowser)
        {
            switch (targetBrowser)
            {
                case BrowserTarget.Chrome:
                    WebBrowser = new ChromeDriver(); //instance object
                    break;

                case BrowserTarget.Firefox:
                    WebBrowser = new FirefoxDriver(); //instance object
                    break;
            }

            //Move to 2nd screen (in my case 1st screen is Right, moving to 2nd (left)
            WebBrowser.Manage().Window.Position = new System.Drawing.Point(-1000, 0); //open in 2nd screen (when 2nd is on left position)
            WebBrowser.Manage().Window.Maximize();
            WebBrowser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15); //Wait for the page to load at least 15 seconds before throwing an Exception

            PageFactory.InitElements(WebBrowser, this);
        }

        public void QuitBrowser()
        {
            Log.Print("Killing browser and process");
            //WebBrowser.Close();
            WebBrowser.Quit();
        }

        public void GoToURL(String url)
        {
            WebBrowser.Navigate().GoToUrl(url);
        }

        public void Wait(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        public void Click(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(WebBrowser, TimeSpan.FromSeconds(15));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));

            element.Click();
        }
        
        public void EnterText(IWebElement inputElement, string text)
        {           
            inputElement.SendKeys(text);
        }

        #endregion Methods
    }
}
