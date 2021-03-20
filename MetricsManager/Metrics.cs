using System;

namespace MetricsManager
{
    public class Metrics
    {
        public DateTime Date { get; set; }
        public int Temperature { get; set; }

        public Metrics(DateTime date, int temperature)
        {
            Date = date;
            Temperature = temperature;
        }
    }
}
