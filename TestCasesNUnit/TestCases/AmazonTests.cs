using CoreFramework.Features;
using NUnit.Framework;
using TestCases.TestCases.ParentClasses;

namespace TestCasesNUnit
{
    public class AmazonTests : NagarroTest
    {
        [Test, Category("Amazon"), Order (2)]
        public void SearchItem()
        {
            Pages.AmazonPage
               .SearchProduct("Samsung Galaxy")
               .SelectResultByOrdinal(3);              
        }

        [Test, Category("Amazon"), Order(1)]
        public void SearchItem2()
        {
            Pages.AmazonPage
               .SearchProduct("Xbox Series X")
               .SelectResultByOrdinal(5);
        }
    }
}
