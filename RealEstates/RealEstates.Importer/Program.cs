using RealEstates.Data;
using RealEstates.Services;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RealEstates.Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new ApplicationDbContext();
            IPropertiesService propertiesService = new PropertiesService(dbContext);
            ImportJsonFile("imot.bg-houses-Sofia-raw-data-2021-03-18.json", propertiesService);
            ImportJsonFile("imot.bg-raw-data-2021-03-18.json", propertiesService);
        }

        public static void ImportJsonFile(string path, IPropertiesService propertiesService)
        {
            var properties =
                 JsonSerializer.Deserialize<IEnumerable<PropertyAsJson>>(File.ReadAllText(path));

            foreach (var item in properties)
            {
                propertiesService.Add(
                    item.District,
                    item.Floor,
                    item.TotalFloors,
                    item.Size,
                    item.YardSize,
                    item.Year,
                    item.Type,
                    item.BuildingType,
                    item.Price);
            }
        }
    }
}
