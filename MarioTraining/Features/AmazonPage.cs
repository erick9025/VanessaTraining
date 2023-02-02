using CoreFramework.Driver;
using CoreFramework.Features.Parent;
using CoreFramework.Features.ParentClasses;
using CoreFramework.Logger;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections;
using System.Collections.Generic;

namespace CoreFramework.Features
{
    public class AmazonPage : BasePage,IBasePage
    {
        
        public AmazonPage(Browser browser) : base(browser)
        {
            //Do additional things
            StandardMethods = new StandardMethods(browser);
        }

        #region Web Elements

        [FindsBy(How = How.XPath, Using = "//*[@id='twotabsearchtextbox']")]
        public IWebElement SearchBarInput { get; set; }

        [FindsBy(How = How.Id, Using = "nav-search-submit-button")]
        public IWebElement SearchButton { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#nav-link-accountList-nav-line-1")]
        public IWebElement HelloUser { get; set; }


        [FindsBy(How = How.XPath, Using = "//img[contains(@data-image-latency,'s-product-image')]")]
        public IList<IWebElement> ResultScreenList { get; set; }

        #endregion Web Elements

        public void GoTo()
        {
            WebBrowser.GoToURL("http://amazon.com.mx");

            string textDisplayed = HelloUser.Text;

            Log.AssertIsTrue(textDisplayed.Contains("Hola"), "Welcome text should say hello. Displayed text: " + textDisplayed);
            
        }

        public AmazonPage SearchProduct(string productWanted)
        {
            GoTo();

            StandardMethods
                .EnterText(SearchBarInput, "Search [Input]", productWanted)
                .Click(SearchButton, "Search [Button]");

            Log.Print("Amazon search executed successfully!");
            return this;
        }
        public AmazonPage SelectResultByOrdinal(int ordinal)
        {          
            StandardMethods
                .Wait(4)
                .Click(ResultScreenList[ordinal - 1], $"Search Result [Image {ordinal}]");
            return this;
        }
    }
}
