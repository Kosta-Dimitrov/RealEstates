using RealEstates.Data;

namespace RealEstates.Services
{
    public interface IActionService
    {
        void PropertySearch();
        void MostExpensiveDistricts();
        void AveragePricePerSquareMeter();
        void AddTag();
        void BulkTagToProperties();
        void PropertyFullInfo();
    }
}
