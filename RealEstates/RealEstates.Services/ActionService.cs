using RealEstates.Data;
using RealEstates.Services.Models;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace RealEstates.Services
{
    public class ActionService : IActionService
    {
        private readonly ApplicationDbContext context;

        public ActionService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddTag()
        {
            IDistrictsService districtsService = new DistrictService(context);
            ITagService tagService = new TagService(context, districtsService);
            Console.WriteLine("Tag name: ");
            string tagName = Console.ReadLine();
            Console.WriteLine("Importance(optional): ");
            bool isParsed = int.TryParse(Console.ReadLine(), out int tagImportance);

            int? importance = isParsed ? tagImportance : null;
            tagService.Add(tagName, importance);
        }

        public void AveragePricePerSquareMeter()
        {
            IPropertiesService propertiesService = new PropertiesService(context);
            Console.WriteLine("Average price per square meter: " + propertiesService.AveragePricePerSquareMeter());
        }

        public void BulkTagToProperties()
        {
            Console.WriteLine("Bulk started.");
            IDistrictsService districtsService = new DistrictService(context);
            ITagService tagService = new TagService(context, districtsService);
            tagService.BulkTagProperties();
            Console.WriteLine("Bulk finished.");
        }

        public void MostExpensiveDistricts()
        {
            Console.Write("District count:");
            int districtsCount = int.Parse(Console.ReadLine());
            IDistrictsService service = new DistrictService(context);
            var districts = service.GetMostExpensiveDistricts(districtsCount);
        }

        public void PropertyFullInfo()
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

        public void PropertySearch()
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
