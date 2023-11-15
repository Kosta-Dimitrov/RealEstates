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
                        case 1: PropertySearch(context);break;
                        case 2: MostExpensiveDistricts(context); break;
                        case 3: AveragePricePerSquareMeter(context); break;
                        case 4: AddTag(context); break;
                        case 5: BulkTagToProperties(context); break;
                        case 6: PropertyFullInfo(context); break;
                        default:
                            break;
                    }

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private static void PropertyFullInfo(ApplicationDbContext context)
        {
            Console.WriteLine("Count of properties:");
            int count = int.Parse(Console.ReadLine());
            IPropertiesService propertiesService = new PropertiesService(context);

            var result = propertiesService.GetFullData(count).ToArray();

            var xmlSerializer = new XmlSerializer(typeof(PropertyInfoFullData[]));
            var stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, result);

            Console.WriteLine(stringWriter.ToString().TrimEnd());
        }

        private static void BulkTagToProperties(ApplicationDbContext context)
        {
            Console.WriteLine("Bulk started.");
            ITagService tagService = new TagService(context);
            tagService.BulkTagProperties();
            Console.WriteLine("Bulk finished.");
        }

        private static void AddTag(ApplicationDbContext context)
        {
            ITagService tagService = new TagService(context);
            Console.WriteLine("Tag name: ");
            string tagName = Console.ReadLine();
            Console.WriteLine("Importance(optional): ");
            bool isParsed = int.TryParse(Console.ReadLine(), out int tagImportance);

            int? importance = isParsed ? tagImportance : null; 
            tagService.Add(tagName, importance);
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
