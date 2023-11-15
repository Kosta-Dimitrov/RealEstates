using AutoMapper;
using RealEstates.Services.Profiler;

namespace RealEstates.Services
{
    public abstract class BaseService
    {
        public BaseService()
        {
            InitializeAutomapper();
        }

        protected IMapper Mapper { get; private set; }
        private void InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<RealEstateProfiler>();
            });
            this.Mapper = config.CreateMapper();
        }
    }
}
