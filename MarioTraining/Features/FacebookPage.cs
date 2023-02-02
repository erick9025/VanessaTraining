using CoreFramework.Driver;
using CoreFramework.Features.Parent;
using CoreFramework.Features.ParentClasses;
using CoreFramework.Logger;
using CoreFramework.Utilities;
using Microsoft.VisualBasic.FileIO;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CoreFramework.Features
{
    public class FacebookPage : BasePage,IBasePage  
    {
        private DDLSelectionMethod method = DDLSelectionMethod.ForEach;

        public FacebookPage(Browser browser) : base(browser)
        {
            //Do additional things
            StandardMethods = new StandardMethods(browser);            
        }
      
        #region Web Elements

        [FindsBy(How = How.XPath, Using = "//a[@data-testid='open-registration-form-button']")]
        public IWebElement ButtonCreateAccount { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='firstname']")]
        public IWebElement InputName { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='lastname']")]
        public IWebElement InputLastName { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='reg_email__']")]
        public IWebElement InputRegEmail { get; set; }
        
        [FindsBy(How = How.Name, Using = "reg_email_confirmation__")]
        public IWebElement InputRegEmailConfirm { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='reg_passwd__']")]
        public IWebElement InputRegPass { get; set; }

        [FindsBy(How = How.CssSelector, Using = "select#day")]
        public IWebElement DdlBirthdayDay { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "select#day option")]
        public IList<IWebElement> DdlOptionsBirthdayDay { get; set; }

        [FindsBy(How = How.CssSelector, Using = "select#month")]
        public IWebElement DdlBirthdayMonth { get; set; }

        [FindsBy(How = How.CssSelector, Using = "select#month option")]
        public IList<IWebElement> DdlOptionsBirthdayMonth { get; set; }        

        [FindsBy(How = How.CssSelector, Using = "select#year")]
        public IWebElement DdlBirthdayYear { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "select#year option")]
        public IList<IWebElement> DdlOptionsBirthdayYear { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name='sex'][value='1']")]
        public IWebElement RadioButtonGenderFemale { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "input[name='sex'][value='2']")]
        public IWebElement RadioButtonGenderMale { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "input[name='sex'][value='-1']")]
        public IWebElement RadioButtonGenderCustom { get; set; }

        [FindsBy(How = How.CssSelector, Using = "select[name='preferred_pronoun'] option:not([selected])")]
        public IList<IWebElement> DdlOptionsPronoun { get; set; }
    
        [FindsBy(How = How.CssSelector, Using = "input[id = \"u_3_q_gg\"]")]
        public IWebElement GenderOptional { get; set; }

        #endregion Web Elements

        public void GoTo()
        {                       
            WebBrowser.GoToURL("http://facebook.com");            
        }

        public FacebookPage CreateAccount(FacebookUser user)
        {           
            //Open Facebook main page
            GoTo();

            //Enter basic user info
            StandardMethods
                .Click(ButtonCreateAccount, "Create New Account [Button]")
                .EnterText(InputName, "Name [Input]", user.FirstName)
                .EnterText(InputLastName, "Last name [Input]", user.LastName)
                .EnterText(InputRegEmail, "Email [Input]", user.Email)
                .EnterText(InputRegEmailConfirm, "Re-enter Email [Input]", user.Email)
                .EnterText(InputRegPass, "Password [Input]", user.Password)
                .Wait(1);
          
            //Enter DOB: Month
            StandardMethods               
                .Click(DdlOptionsBirthdayMonth[user.DateOfBirth.Month - 1], "Month [Option]")                
                .Wait(1);

            //Enter DOB: Day
            StandardMethods
                .Click(DdlOptionsBirthdayDay[user.DateOfBirth.Day - 1], "Day [Option]")
                .Wait(1);

            //Enter DOB: Year (Old code left for reference ONLY -> Index is not reliable when selecting Year?
            switch (method)
            {
                case DDLSelectionMethod.ByIndex:
                    StandardMethods
                        .Click(DdlOptionsBirthdayYear[24], "Year [Option]");
                    break;

                case DDLSelectionMethod.Lambda:
                    //Sample loop using lambda function with 'for each' statement
                    DdlOptionsBirthdayYear.ToList().ForEach(ddlOption =>
                    {
                        string attributeValue = ddlOption.GetAttribute("value");
                        if (Convert.ToInt32(attributeValue) == user.DateOfBirth.Year)
                        {
                            StandardMethods
                                .Click(ddlOption, "Year [Option] using Lambda");
                            return; //Terminate foreach when condition is met
                        }
                    });
                    break;

                case DDLSelectionMethod.ForEach:
                    //Sample foreach regular loop
                    foreach (IWebElement ddlOption in DdlOptionsBirthdayYear.ToList())
                    {
                        string attributeValue = ddlOption.GetAttribute("value");
                        if (Convert.ToInt32(attributeValue) == user.DateOfBirth.Year)
                        {
                            StandardMethods
                                .Click(ddlOption, "Year [Option] using Loop");
                            break; //Break foreach when condition is met
                        }
                    }
                    break;

                case DDLSelectionMethod.LINQAttributeString:
                    StandardMethods
                        .Click(DdlOptionsBirthdayYear.ToList().Where(option => option.GetAttribute("value") == user.DateOfBirth.Year.ToString()).FirstOrDefault()
                                , "Year [Option] using LINQ with String");
                    break;

                case DDLSelectionMethod.LINQAttributeInt:
                    var listOfMatches = DdlOptionsBirthdayYear.ToList()
                        .Where(option => Convert.ToInt32(option.GetAttribute("value")) == user.DateOfBirth.Year);
                    IWebElement firstMatch = listOfMatches.FirstOrDefault();
                    StandardMethods
                        .Click(firstMatch, "Year [Option] using LINQ with Int");
                    break;
            }

            //Finally select gender/sex
            switch (user.Gender)
            {
                case Gender.Female:
                    StandardMethods
                        .Click(RadioButtonGenderFemale, "Female [Radio Button]");
                    break;

                case Gender.Male:
                    StandardMethods
                        .Click(RadioButtonGenderMale, "Male [Radio Button]");
                    break;

                case Gender.NonBinary:
                    StandardMethods
                        .Click(RadioButtonGenderCustom, "Custom [Radio Button]");

                    //ToDo select pronoun (HOMEWORK FOR MARIO)                  
                    Log.Print("Selecting Pronoun");
                    foreach(IWebElement ddlOption in DdlOptionsPronoun.ToList())
                    {
                        string attributeValue = ddlOption.GetAttribute("value");
                        if (Convert.ToInt32(attributeValue) == user.PronounId)
                        {
                            StandardMethods
                                .Click(ddlOption, "Pronoun [Option] using Loop");
                            break; //Break foreach when condition is met
                        }
                    }

                    break;

                default:
                    Log.AssertFail("Not handled scenario yet: " + user.Gender);
                    break;
            }          

            Log.Print("End of method");
            return this;
        }    
    }
}
