using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFramework.Utilities
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        protected Person()
        {

        }

        public Person(string firstName, string lastName, DateTime dob, Gender gender)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dob;
            this.Gender = gender;
        }
        
    }
}
