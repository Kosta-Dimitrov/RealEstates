using AutoMapper.QueryableExtensions;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace RealEstates.Services
{
    public class PropertiesService : BaseService, IPropertiesService
    {
        public ApplicationDbContext context { get; set; }
        public PropertiesService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(string districtName, int floor, int maxFloor, int size, int yardSize, int year, string propertyType, string buildingType, int price)
        {
            var property = new Property()
            {
                Size = size,
                Price = price <= 0 ? null : price,
                Floor = floor <= 0 || floor > 255 ? null : (byte)floor,
                TotalFloors = maxFloor <= 0 || maxFloor > 255 ? null : (byte)maxFloor,
                YardSize = yardSize <= 0 ? null : yardSize,
                Year = year < 1900 ? null : year
            };

            var district = context.Districts.FirstOrDefault(x => x.Name == districtName);
            
            if(district == null)
            {
                district = new District() { Name = districtName };
            }

            property.District = district;

            var typeOfProperty = context.PropertyTypes.FirstOrDefault(x => x.Name == propertyType);
            if(typeOfProperty == null)
            {
                typeOfProperty = new PropertyType() { Name = propertyType };
            }
            property.Type = typeOfProperty;

            var typeOfBuilding = context.BuildingTypes.FirstOrDefault(x => x.Name == buildingType);

            if(typeOfBuilding == null)
            {
                typeOfBuilding = new BuildingType() { Name = buildingType };
            }
            property.BuildingType = typeOfBuilding;

            context.Properties.Add(property);
            context.SaveChanges();

        }

        public IEnumerable<PropertyInfo> Search(int minPrice, int maxPrice, int minSize, int maxSize)
            => context.Properties
                .Where(x => x.Price >= minPrice && x.Price <= maxPrice && x.Size >= minSize && x.Size <= maxSize)
                .ProjectTo<PropertyInfo>(this.Mapper.ConfigurationProvider)
                .ToList();

        public decimal AveragePricePerSquareMeter()
            =>  context.Properties
                    .Where(x => x.Price.HasValue)
                    .Average(x => x.Price/(decimal)x.Size) ?? 0;

        public decimal AveragePricePerSquareMeter(string districtName)
            => context.Properties
                .Where(x => x.Price.HasValue && x.District.Name == districtName)
                .Average(x => x.Price / (decimal)x.Size) ?? 0;
    }
}
