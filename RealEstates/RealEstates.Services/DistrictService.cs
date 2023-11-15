using AutoMapper.QueryableExtensions;
using RealEstates.Data;
using RealEstates.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace RealEstates.Services
{
    public class DistrictService : BaseService, IDistrictsService
    {
        private readonly ApplicationDbContext context;
        public DistrictService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public double AverageSize(int districtId)
            => context.Properties
                      .Where(x => x.DistrictId == districtId)
                      .Average(x => x.Size);

        public IEnumerable<DistrictInfo> GetAllDistricts()
            => this.context
                    .Districts
                    .ProjectTo<DistrictInfo>(this.Mapper.ConfigurationProvider);

        public IEnumerable<DistrictInfo> GetMostExpensiveDistricts(int count)
            => GetAllDistricts()
                   .OrderBy(x => x.AveragePricePerSquareMeter)
                   .Take(count)
                   .ToList();
        }
    }
}
