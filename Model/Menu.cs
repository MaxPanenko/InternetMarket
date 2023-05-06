using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InternetMarket.Model
{
    public class Menu
    {
        public List<User> Emails { get; }

        public User Email { get; set; }

        public User UserName { get; }

        public User Password { get; }

        public bool TakenOrNot { get; } = false;

        public Menu (string email,string name)
        {
            Emails = GetUsersData();
            Email = Emails.FirstOrDefault(x => x.Email == email);
            if (Email == null)
            {
                Email = new User(email);
                Emails.Add(Email);
                TakenOrNot = true;               
                Save();
            }
            UserName = new User(name); 
        }
        public Menu() { }

        public void Interface() 
        {
            Thread.Sleep(1000);
            Console.Clear();
            MenuStart();
            Console.WriteLine();
            Console.Write("Choose a number or if you won't press\"s\":");
   
        }
        public void MenuStart() 
        {
            Console.WriteLine("This is your profile");
            Console.WriteLine("You could change each element.");
            Console.WriteLine();
            Console.WriteLine($"1.Name\t\t2.Password\t  3.Email");
        }
        public void Save()
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream("marketusers.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Emails);
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
