using CoreFramework.Driver;
using CoreFramework.Features;
using CoreFramework.Logger;
using NUnit.Framework;
using OpenQA.Selenium;

namespace TestCasesNUnit.OldStuff
{
    public class MyTestsLesson2
    {
        /*
        Antes de todo
        Antes de cada test
        Despues de cada test
        Despues de todo
        */

        private Browser Browser;
        private GooglePage GooglePage;
        private FacebookPage FacebookPage;
        private AmazonPage AmazonPage;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Log.Print("This gets executed one time per run BEFORE");

            Browser = new Browser();

            Browser.InitializeBrowser(BrowserTarget.Chrome);
            Browser.Wait(1);

            GooglePage = new GooglePage(Browser);
            FacebookPage = new FacebookPage(Browser);
            AmazonPage = new AmazonPage(Browser);
        }

        [SetUp]
        public void Setup()
        {
            Log.Print("This gets executed BEFORE EVERY test");
            Browser.Wait(1);
        }
        
        [TearDown]
        public void TearDown()
        {
            Log.Print("This gets executed AFTER EVERY test");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Log.Print("This gets executed one time per run AFTER");

            Browser.Wait(2);
            Browser.QuitBrowser();
        }

        //------------------------------------------------------------------------------------------------

        [Test, Category("OldAmazon")]
        public void AmazonSearch()
        {
            Browser.GoToURL("http://amazon.com.mx");

            Browser.WebBrowser.FindElement(By.Id("twotabsearchtextbox")).SendKeys("Xbox Series X");
            Browser.WebBrowser.FindElement(By.Id("nav-search-submit-button")).Click();
        }

        [Test, Category("OldAmazon")]
        public void AmazonSearchWithPageFactory()
        {
            AmazonPage              
                .SearchProduct("Samsung Galaxy S22");
        }
    }
}