using InternetMarket.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InternetMarket.Controller 
{
    public class Profile
    {
        public List<User> Users { get; }

        public User User { get; }

        public User CurrentUser { get; }

        public bool IsNewUser { get; }

        public Profile(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("Name canno't be empty", nameof(userName));
            }
            Users = GetUsersData();

            CurrentUser = Users.FirstOrDefault(u => u.Name == userName);




            if (CurrentUser == null)
            {
                CurrentUser = new User(userName);
                Users.Add(CurrentUser);
                IsNewUser = true;
                Save();
            }

        }



        public void PasswordForLogin(string name, string password)
        {
            if (CurrentUser.Password == password)
            {
                Console.WriteLine("You are Log In System");
                CurrentUser.Password = password;
            }
            try
            {
                while (CurrentUser.Password != password)
                {
                    if (password.Length < 6 || CurrentUser.Password != password)
                    {
                        Console.Clear();
                        Console.WriteLine("Your password is incorrect");
                        Console.Write("Try Again:");
                        password = Console.ReadLine();
                    }                  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

        }

        public void EmailUpdate(string email)
        {          
            CurrentUser.Email = email;
            Save();
        }
        public void PasswordUpdate(string password) 
        {
            CurrentUser.Password = password;
            Save();
        }
        public void NameUpdate(string name) 
        {
            CurrentUser.Name = name;
            Save();
        }


        

        public void SetNewUserData(string user, DateTime birthDate, string password, string email)
        {
            CurrentUser.Name = user;
            CurrentUser.DateOfBirth = birthDate;
            CurrentUser.Email = email;
            CurrentUser.Password = password;
            Save();
        }
        

        public void Save()
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream("marketusers.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Users);
            };
        }
        private List<User> GetUsersData()
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream("MarketUsers.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length > 0 && formatter.Deserialize(fs) is List<User> user)
                {
                    return user;
                }
                else
                {
                    return new List<User>();
                }

            };
        }



    }
}
