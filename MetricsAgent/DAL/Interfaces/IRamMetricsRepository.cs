using System;
using System.Collections.Generic;
using MetricsAgent.DAL.Models;


namespace MetricsAgent.DAL.Interfaces
{
    public interface IRamMetricsRepository : IRepository<RamMetricModel>
    {
        IList<RamMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}
