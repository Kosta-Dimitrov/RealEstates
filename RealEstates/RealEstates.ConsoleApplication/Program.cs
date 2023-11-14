using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services;
using System;

namespace RealEstates.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ApplicationDbContext();
            //context.Database.Migrate();

            while(true)
            {
                Console.Clear();
                Console.WriteLine("Choose an option");
                Console.WriteLine("1. Property search");
                Console.WriteLine("2. Most expensive districts");
                Console.WriteLine("3. Average price per square meter");
                bool parsed = int.TryParse(Console.ReadLine(), out int option);

                if (parsed && option >= 1 && option <= 3)
                {
                    switch (option)
                    {
                        case 1: PropertySearch(context);break;
                        case 2: MostExpensiveDistricts(context); break;
                        case 3: AveragePricePerSquareMeter(context); break;
                        default:
                            break;
                    }

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private static void AveragePricePerSquareMeter(ApplicationDbContext context)
        {
            IPropertiesService propertiesService = new PropertiesService(context);
            Console.WriteLine("Average price per square meter: " + propertiesService.AveragePricePerSquareMeter());
        }

        private static void MostExpensiveDistricts(ApplicationDbContext context)
        {
            Console.Write("District count:");
            int districtsCount = int.Parse(Console.ReadLine());
            IDistrictsService service = new DistrictService(context);
            var districts = service.GetMostExpensiveDistricts(districtsCount);
        }

        private static void PropertySearch(ApplicationDbContext context)
        {
            Console.Write("Min price:");
            int minPrice = int.Parse(Console.ReadLine());
            Console.Write("Max price:");
            int maxPrice = int.Parse(Console.ReadLine());
            Console.Write("Min size:");
            int minSize = int.Parse(Console.ReadLine());
            Console.Write("Max size:");
            int maxSize = int.Parse(Console.ReadLine());

            IPropertiesService service = new PropertiesService(context);
            var properties = service.Search(minPrice, maxPrice, minSize, maxSize);
        }
    }
}
