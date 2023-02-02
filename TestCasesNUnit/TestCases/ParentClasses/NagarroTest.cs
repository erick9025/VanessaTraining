using CoreFramework.Driver;
using CoreFramework.Logger;
using NUnit.Framework;
using TestCases.Utilities;
using TestCasesNUnit.TestCases.ParentClasses;

namespace TestCases.TestCases.ParentClasses
{
    public abstract class NagarroTest : BaseTest
    {
        protected Pages Pages { get; set; }

        #region test annotations
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Log.Print("This gets executed one time per run BEFORE");

            InitializeFramework();
            InitializePages();
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
            Browser.Wait(3);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Log.Print("This gets executed one time per run AFTER");

            CloseBrowser();
        }
        #endregion test annotations

        public void InitializePages()
        {
            Pages = new Pages(Browser);
        }
    }
}
