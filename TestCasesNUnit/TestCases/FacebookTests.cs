using CoreFramework.Logger;
using CoreFramework.Utilities;
using NUnit.Framework;
using TestCases.TestCases.ParentClasses;

namespace TestCasesNUnit
{
    public class FacebookTests : NagarroTest
    {
        [Test, Category("Facebook"), Order(1)]
        public void CreateAccount_Man()
        {
            Pages.FacebookPage
               .CreateAccount(FacebookUser.GenerateUser(Gender.Male));
        }

        [Test, Category("Facebook"), Order(2)]
        public void CreateAccount_Woman()
        {
            Pages.FacebookPage
               .CreateAccount(FacebookUser.GenerateUser(Gender.Female));
        }

        [Test, Category("Facebook"), Order(3)]
        public void CreateAccount_NonBinary()
        {
            Pages.FacebookPage
               .CreateAccount(FacebookUser.GenerateUser(Gender.NonBinary));
        }
    }
}
