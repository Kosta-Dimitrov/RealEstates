using RealEstates.Services.Models;
using System.Collections.Generic;

namespace RealEstates.Services
{
    public interface IDistrictsService
    {
        IEnumerable<DistrictInfo> GetMostExpensiveDistricts(int count);
        IEnumerable<DistrictInfo> GetAllDistricts();
        double AverageSize(int districtId);
    }
}
