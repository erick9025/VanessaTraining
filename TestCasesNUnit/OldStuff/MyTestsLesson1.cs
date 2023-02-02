using CoreFramework.Logger;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace TestCasesNUnit.OldStuff
{
    public class MyTestsLesson1
    {
        int waitFactor = 1;

        [SetUp]
        public void Setup()
        {
        }

        [Test, Category("Basic")]
        public void HolaMundo ()
        {
           Log.Print("Hola Mundo!");
        }

        [Test]
        [Category("Basic")]
        public void HelloWorld()
        {
           Log.Print("Hello World");
        }

        [Test, Category("OldGoogle")]
        public void GoogleSearch()
        {
            WebDriver driver = null; //declare object equal to null

            try
            {
                driver = new ChromeDriver(); //instance object

                //Move to 2nd screen (in my case 1st screen is Right, moving to 2nd (left)
                driver.Manage().Window.Position = new System.Drawing.Point(-1000, 0); //open in 2nd screen (when 2nd is on left position)
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15); //Wait for the page to load at least 15 seconds before throwing an Exception

                //Go to URL and perform search
                driver.Navigate().GoToUrl("http://google.com");
                Thread.Sleep(500 * waitFactor);

                //Find GOOGLE Search bar (input) Searching same elements using 4 different ways
                driver.FindElement(By.XPath("//input[@name='q']")).SendKeys("XPath 1");
                driver.FindElement(By.XPath("//*[@name='q']")).SendKeys("XPath 2");
                driver.FindElement(By.CssSelector("input[name='q']")).SendKeys("CSS Selector");
                driver.FindElement(By.Name("q")).SendKeys("Name");

                Thread.Sleep(1500 * waitFactor);              
            }
            catch (Exception e)
            {
                //Do nothing
                throw e;
            }
            finally
            {
                driver.Quit(); //close both window and kills process
                //driver.Close(); //close the chrome window but process is alive
            }
        }
    }
}