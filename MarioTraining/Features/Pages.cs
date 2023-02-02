using CoreFramework.Driver;
using CoreFramework.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCases.Utilities
{
    public class Pages
    {
        private Browser Browser { get; set; }

        public Pages(Browser browser)
        {
            this.Browser = browser;
        }

        private GooglePage _googlePage;

        public GooglePage GooglePage
        {
            get
            {
                return _googlePage ?? (_googlePage = new GooglePage(Browser));
            }
        }

        private AmazonPage _amazonPage;

        public AmazonPage AmazonPage
        {
            get
            {
                return _amazonPage ?? (_amazonPage = new AmazonPage(Browser));
            }
        }

        private FacebookPage _facebookPage;

        public FacebookPage FacebookPage
        {
            get
            {
                return _facebookPage ?? (_facebookPage = new FacebookPage(Browser));
            }
        }

        public void CreateAccount(string v)
        {
            throw new NotImplementedException();
        }
    }
}

