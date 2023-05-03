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
        
    }
}
