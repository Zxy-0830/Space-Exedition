using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Expedition
{
    internal class UseMeun
    {
        public static void Run(ref ArtifactInventory[] inventory, ref int count)
        {
            while (true)
            {
                Console.WriteLine("\nWelcome to use the Space Expedition Manage System! ");
                Console.WriteLine("1.Add new artifact. ");
                Console.WriteLine("2.View inventory. ");
                Console.WriteLine("0.Exit ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Files.Adding(ref inventory, ref count);
                        Console.WriteLine("Added.");
                        break;
                    case "2":
                        Files.Displaying(inventory, count);
                        break;
                    case "0":
                        Console.WriteLine("Exit. ");
                        return;
                    default:
                        Console.WriteLine("Invalid enter. ");
                        break;
                }
            }
        }
    }
}
