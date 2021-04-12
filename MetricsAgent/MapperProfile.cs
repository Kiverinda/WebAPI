using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.Responses;
namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetricModel, CpuMetricDto>();
            CreateMap<DotNetMetricModel, DotNetMetricDto>();
            CreateMap<NetworkMetricModel,NetworkMetricDto>();
            CreateMap<HddMetricModel, HddMetricDto>();
            CreateMap<RamMetricModel, RamMetricDto>();
        }
    }
}
