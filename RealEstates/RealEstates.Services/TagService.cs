using RealEstates.Data;
using RealEstates.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstates.Services
{
    public class TagService : BaseService, ITagService
    {
        private readonly ApplicationDbContext context;
        private readonly IPropertiesService propertiesService;
        private readonly IDistrictsService districtsService;
        public TagService(ApplicationDbContext context, IPropertiesService propertiesService, IDistrictsService districtsService)
        {
            this.context = context;
            this.propertiesService = propertiesService;
            this.districtsService = districtsService;
        }

        public void Add(string name, int? importance = null)
        {
            if (context.Tags.FirstOrDefault(x => x.Name == name) == null)
            {
                var tag = new Tag()
                {
                    Name = name,
                    Importance = (int)importance
                };
                context.Tags.Add(tag);
                context.SaveChanges();
            }
        }

        public void BulkTagProperties()
        {
            var properties = context.Properties.ToList();
            //TODO Maybe better name shoud be given
            var nameTagsDictionary = GetAllTags().ToDictionary(x => x.Name);
            var averagePricePerDistrictDictionary = this.districtsService.GetAllDistricts()
                .ToDictionary(x => x.Name, x => x.AveragePricePerSquareMeter);


            foreach (var property in properties)
            {
                var averagePriceForDistrict = averagePricePerDistrictDictionary[property.District.Name];

                if( property.Price >= averagePriceForDistrict)
                {
                    property.Tags.Add(nameTagsDictionary["Expensive"]);
                }
                else if(property.Price < averagePriceForDistrict)
                {
                    property.Tags.Add(nameTagsDictionary["Cheap"]);
                }

                var newPropertyYear = DateTime.UtcNow.AddYears(-15).Year;

                if(property.Year > newPropertyYear)
                {
                    property.Tags.Add(nameTagsDictionary["New Property"]);
                }
                else
                {
                    property.Tags.Add(nameTagsDictionary["Old Property"]);
                }

                var averageSizePerDistrict = this.districtsService.AverageSize(property.DistrictId);

                if(property.Size >= averageSizePerDistrict)
                {
                    property.Tags.Add(nameTagsDictionary["Big Property"]);
                }
                else
                {
                    property.Tags.Add(nameTagsDictionary["Small Property"]);
                }

                if(property.Floor.HasValue)
                {
                    if (property.Floor.Value == 1)
                    {
                        property.Tags.Add(nameTagsDictionary["First floor"]);
                    }
                    else if(property.Floor.Value == property.TotalFloors)
                    {
                        property.Tags.Add(nameTagsDictionary["Last floor"]);
                    }
                }
                context.SaveChanges();
            }
        }

        private IEnumerable<Tag> GetAllTags()
            => context.Tags.ToList();
    }
}
