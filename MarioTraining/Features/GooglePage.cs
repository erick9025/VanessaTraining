using CoreFramework.Driver;
using CoreFramework.Features.Parent;
using CoreFramework.Features.ParentClasses;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace CoreFramework.Features
{
    public class GooglePage : BasePage, IBasePage
    {
        public GooglePage(Browser browser) : base(browser)
        {
            //Do additional things
            StandardMethods = new StandardMethods(browser);
        }

        #region Web Elements

        [FindsBy(How = How.Name, Using = "q")]
        public IWebElement SearchBarInput { get; set; }

        #endregion Web Elements

        public void GoTo()
        {
            WebBrowser.GoToURL("http://google.com.mx");
        }

        public GooglePage Search(string text)
        {
            GoTo();
            SearchBarInput.SendKeys(text);

            return this;
        }
    }
}
