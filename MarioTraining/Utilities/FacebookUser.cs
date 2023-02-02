using CoreFramework.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFramework.Utilities
{
    public class FacebookUser : Person
    {
        #region Properties
        public string Email { get; set; }

        public string Password { get; set;}

        private int _pronounId = 0;

        public int PronounId
        {
            get
            {
                return _pronounId;
            }
            set
            {
                List<int> acceptedValues = new List<int> { 1, 2, 6 };
                Log.AssertIsTrue(acceptedValues.Exists(newValue => newValue == value), $"Pronoun value: {value} should be either 1, 2 or 6");
                _pronounId = value;
            }
        }

        public string Pronoun //depends directly on 'PronounId' -> equivalence
        {
            get
            {
                Dictionary<int, string> dictionaryPronouns = new Dictionary<int, string>
                {
                    { 1,  "She" }, //1=She
                    { 2,  "He" }, //2=He
                    { 6,  "They" }, //6=They
                };

                return dictionaryPronouns[PronounId];
            }/*
            private set
            {
                Pronoun = value;
            }
            */
        }

        #endregion Properties

        #region Constructors

        public FacebookUser(string firstName, string lastName, DateTime dob, Gender gender) : base(firstName, lastName, dob, gender)
        {
            //Do nothing, just call parent constructor
        }
        
        public FacebookUser(Person person, string email, string password)
        {
            this.FirstName = person.FirstName; 
            this.LastName = person.LastName;
            this.DateOfBirth = person.DateOfBirth;
            this.Gender = person.Gender;
            this.Email = email;
            this.Password = password;
        }
        #endregion Constructors

        #region Class methods
        public static DateTime GenerateRandomDate(int minYear, int maxYear)
        {
            Random random = new Random();
            int rYear = random.Next(minYear, maxYear);
            int rMonth = random.Next(1, 12);
            int rDay = 0;

            switch (rMonth)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    rDay = random.Next(1, 31);
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    rDay = random.Next(1, 30);
                    break;
                case 2:
                    rDay = random.Next(1, 28);
                    break;
            }

            return new DateTime(rYear, rMonth, rDay);
        }

        public static FacebookUser GenerateUser(Gender gender)
        {
            Dictionary<int, int> dictionaryPronouns = new Dictionary<int, int>
            {
                { 101, 1 }, //1=She
                { 102, 2 }, //2=He
                { 103, 6 }, //6=They
            };

            Dictionary<int, int> emptyDict = new Dictionary<int, int>();
            Random random = new Random();
            List<string> listFirstNames = new List<string>
            {
                "Erick", 
                "Sergio", 
                "Carlos", 
                "Roberto", 
                "Diego",
                "Natalia", 
                "Karina", 
                "Jaqueline", 
                "Vanessa", 
                "Fernanda"
            };
            List<string> listLastNames = new List<string>
            {
                "Alvarez", "Zamora", "Perez", "Gonzalez", "Villalpando",
                "Pickett", "Jackson", "Mahomes", "Brady", "Burrow"
            };
            List<string> emptyList = new List<string>();        
            string randomName = "";
            string randomLast = listLastNames[random.Next(0, listLastNames.Count - 1)];
            int keyPronoun = 0;

            switch (gender)
            {
                case Gender.NonBinary:
                    randomName = listFirstNames[random.Next(0, listFirstNames.Count - 1)];
                    keyPronoun = random.Next(101, 103);
                    break;
                case Gender.Male:
                    randomName = listFirstNames[random.Next(0, 4)];
                    keyPronoun = 102;
                    break;
                case Gender.Female:
                    randomName = listFirstNames[random.Next(5, 9)];
                    keyPronoun = 101;
                    break;
            }

            //finalmente retornar el objeto ya inicializado con los datos de una persona random
            FacebookUser user = 
                new FacebookUser(randomName, 
                randomLast, 
                GenerateRandomDate(1950, DateTime.Today.Year - 12), 
                gender);

            int emailRandom = random.Next(1, 2);


            //Operador ternario
            //condition ? true : false
            user.Email = random.Next(1, 3) == 1
                ? "user" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "@gmail.com"
                : "user" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "@outlook.com";

            user.Password = "P@ssw0rd!951";
            user.PronounId = dictionaryPronouns[keyPronoun]; //Convert value from (101-103) to a value in [1,2,6]
            user.PrintUserDetails();

            return user;
        }
        #endregion Class methods

        #region Object methods
        public void PrintUserDetails()
        {
            Log.Print("------------------ USER DETAILS ------------------");
            Log.Print($"     First Name: {this.FirstName}");
            Log.Print($"     Last Name: {this.LastName}");
            Log.Print($"     DOB: {this.DateOfBirth.ToLongDateString()}");
            Log.Print($"     Gender: {this.Gender}");
            Log.Print($"     Email: {this.Email}");
            Log.Print($"     Password: {this.Password}");
            Log.Print($"     Pronoun Id: {this.PronounId}");
            Log.Print($"     Pronoun: {this.Pronoun}");
            Log.Print("--------------------------------------------------");
        }
        #endregion Object methods
    }
}
