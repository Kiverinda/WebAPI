using System;
using System.Collections.Generic;
using MetricsAgent.DAL.Models;


namespace MetricsAgent.DAL.Interfaces
{
    public interface IHddMetricsRepository : IRepository<HddMetricModel>
    {
        IList<HddMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}
