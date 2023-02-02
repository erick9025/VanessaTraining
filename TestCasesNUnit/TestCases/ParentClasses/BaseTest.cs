using CoreFramework.Driver;

namespace TestCasesNUnit.TestCases.ParentClasses
{
    public abstract class BaseTest
    {
        protected Browser Browser { get; set; }

        public void InitializeFramework()
        {
            Browser = new Browser();

            Browser.InitializeBrowser(BrowserTarget.Chrome);
            Browser.Wait(1);
        }

        public void CloseBrowser()
        {
            Browser.Wait(2);
            Browser.QuitBrowser();
        }
    }
}
