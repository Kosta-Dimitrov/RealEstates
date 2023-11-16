using RealEstates.Data;
using RealEstates.Services;
using RealEstates.Services.Models;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace RealEstates.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ApplicationDbContext();
            //context.Database.Migrate();

            IActionService actionService = new ActionService(context);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose an option");
                Console.WriteLine("1. Property search");
                Console.WriteLine("2. Most expensive districts");
                Console.WriteLine("3. Average price per square meter");
                Console.WriteLine("4. Add tag");
                Console.WriteLine("5. Bulk tag to properties");
                Console.WriteLine("6. Property full info");
                bool parsed = int.TryParse(Console.ReadLine(), out int option);

                if (parsed && option >= 1 && option <= 6)
                {
                    switch (option)
                    {
                        case 1: actionService.PropertySearch();break;
                        case 2: actionService.MostExpensiveDistricts(); break;
                        case 3: actionService.AveragePricePerSquareMeter(); break;
                        case 4: actionService.AddTag(); break;
                        case 5: actionService.BulkTagToProperties(); break;
                        case 6: actionService.PropertyFullInfo(); break;
                        default:
                            break;
                    }

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}
