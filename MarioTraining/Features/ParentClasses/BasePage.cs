using CoreFramework.Driver;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFramework.Features.Parent
{
    public abstract class BasePage
    {
        protected Browser WebBrowser { get; set; }
        protected StandardMethods StandardMethods { get; set; }

        public BasePage(Browser webBrowser)
        {
            WebBrowser = webBrowser;
            

            PageFactory.InitElements(WebBrowser.WebBrowser, this);
        }
    }
}
