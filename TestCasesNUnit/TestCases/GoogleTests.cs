using CoreFramework.Features;
using NUnit.Framework;
using TestCases.TestCases.ParentClasses;

namespace TestCasesNUnit
{
    public class GoogleTests : NagarroTest
    {
        [Test, Category("Google")]
        public void SearchSomething()
        {
            Pages.GooglePage
               .Search("El euro hoy");
        }
    }
}
