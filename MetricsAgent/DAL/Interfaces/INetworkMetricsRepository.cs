using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;

namespace MetricsAgent.DAL.Interfaces
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetricModel>
    {
        IList<NetworkMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}
