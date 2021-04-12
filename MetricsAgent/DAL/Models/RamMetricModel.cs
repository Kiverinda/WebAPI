using System;

namespace MetricsAgent.DAL.Models
{
    public class RamMetricModel
    {
        public int Id { get; set; }
        public double Available { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
