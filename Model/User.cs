using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace InternetMarket.Model
{
    [Serializable]


    public class User
    {
        
        public string Name { get; set; }
       
        public string Password { get; set;  }
       
        public DateTime DateOfBirth { get; set;  }
       
        public int Age { get { return DateTime.Now.Year - DateOfBirth.Year; } }
        
        public string Email { get; set; }

        
        public User(string name, string password, DateTime dateOfBirth, string email) 
        {
            #region Check statements
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name canno't be empty.", nameof(name));
            }

            if (password == null)
            {
                throw new ArgumentNullException("Password canno't be empty.", nameof(password));
            }

            if (DateOfBirth < DateTime.Parse("01.01.1900") && DateOfBirth < DateTime.Now)
            {
                throw new ArgumentException("Impossible date of birth.", nameof(DateOfBirth));
            }

            if (Age < 0)
            {
                throw new ArgumentException("Age canno't be less more then 0.", nameof(Age));
            }

            if (Email == null)
            {
                throw new ArgumentException("Height canno't be less more then 130.", nameof(Email));
            }

            #endregion
            Name = name;
            Password = password;
            DateOfBirth = dateOfBirth;
            Email = email;
            
        }
        public User() { }

        public User(string name) 
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Name canno't be empty.", nameof(name));
            }
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
