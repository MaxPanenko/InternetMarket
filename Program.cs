using InternetMarket.Controller;
using MongoDB.Driver;
using InternetMarket.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CMD
{
    public class Program
    {
        static void Main(string[] args)
        {
            Greetings();
            
                var name = Console.ReadLine();
                var profile = new Profile(name);

                if (profile.CurrentUser.Email != null)
                {
                    Console.WriteLine($"{profile.CurrentUser}");
                    Console.Write("Enter your password:");
                    var pass = Console.ReadLine();
                    profile.PasswordForLogin(name, pass);
                }
                else
                {
                    Console.WriteLine($"{profile.CurrentUser}");
                }

                if (profile.IsNewUser)
                {
                    var datebirth = ParseDateTime();
                    Console.Write("Enter new password: ");
                    var password = Console.ReadLine();
                    Console.Write("Enter your Email: ");
                    var email = Console.ReadLine();

                    profile.SetNewUserData(name, datebirth, password, email);
                }
                var menu = new Menu();
                menu.Interface();
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.D3)
                {
                    Console.WriteLine();
                    Console.Write("Enter new email:");                   
                    var emailButton = Console.ReadLine();
                    var takenOrNot = new Menu(emailButton,profile.CurrentUser.Name);
                    if (takenOrNot.TakenOrNot)
                    {
                        Console.WriteLine("Your Email has been change");
                    }
                    else 
                    {
                        Console.WriteLine("This email is already taken");
                        Console.WriteLine();
                    }
                        profile.EmailUpdate(emailButton);
                    Console.WriteLine($"This is your new email - {profile.CurrentUser.Email}");

                }
                else if (key.Key == ConsoleKey.D2)
                {
                    Console.WriteLine();
                    Console.Write("Enter new password:");
                    var passwordButton = Console.ReadLine();
                    profile.PasswordUpdate(passwordButton);
                    Console.WriteLine($"This is your new password - {profile.CurrentUser.Password}");
                }
                else if (key.Key == ConsoleKey.D1)
                {
                    Console.WriteLine();
                    Console.Write("Enter new name:");
                    var nameButton = Console.ReadLine();
                    profile.NameUpdate(nameButton);
                    Console.WriteLine($"This is your new name - {profile.CurrentUser.Name}");
                }
                else if (key.Key == ConsoleKey.S)
                {
                    Console.WriteLine();
                    Console.WriteLine("Thank you for visit");
                    
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("This option isn't available");
                }




                MongoClient dbClient = new MongoClient("mongodb://localhost:27017");

                
                var database = dbClient.GetDatabase("InternetMarket");
                var collection = database.GetCollection<BsonDocument>("Users");
                var document = new BsonDocument { { "user_id", 1 }, {
                        "profile",
                        new BsonArray {
                        new BsonDocument { { "type", "name" }, { "UserName", $"{profile.CurrentUser.Name}" } },
                        new BsonDocument { { "type", "password" }, { "UserPassword", $"{profile.CurrentUser.Password}" } },
                        new BsonDocument { { "type", "dateOfBirth" }, { "UserDateOfBirth", $"{profile.CurrentUser.DateOfBirth}" } },
                        new BsonDocument { { "type", "email" }, { "UserEmail", $"{profile.CurrentUser.Email}" } }
                        }
                        }, { "class_id", 240 }
                };
                collection.InsertOne(document);
            

        }

        public static void Greetings() 
        {
            Console.WriteLine("Welcome to the MaksymShop");
            Console.Write("Please,enter your name: ");
            
        }

       

        public static DateTime ParseDateTime() 
        {
            DateTime birthDate;
            while (true) 
            {
                Console.Write("Enter your BirthDate (dd.MM.yyyy):");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    break;
                }
                else 
                {
                    Console.WriteLine("Incorrect date format");
                }

            }
            return birthDate;
        }
        

        
    }
}
