using AutoMapper;
using RealEstates.Models;
using RealEstates.Services.Models;
using System.Linq;

namespace RealEstates.Services.Profiler
{
    public class RealEstateProfiler : Profile
    {
        public RealEstateProfiler()
        {
            this.CreateMap<Property, PropertyInfo>()
                    .ForMember(x => x.BuildingType, y => y.MapFrom(s => s.BuildingType.Name));
            this.CreateMap<District, DistrictInfo>()
                    .ForMember(x => x.AveragePricePerSquareMeter,
                                    y => y.MapFrom(s => s.Properties
                                                            .Where(p => p.Price.HasValue)
                                                            .Average(p => p.Price / (decimal)p.Size) ?? 0));

        }
    }
}
