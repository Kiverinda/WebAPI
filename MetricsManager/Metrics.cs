using System;

namespace MetricsManager
{
    public class Metrics
    {
        public DateTime date { get; set; }
        public int temperature { get; set; }

        public Metrics(DateTime date, int temp)
        {
            this.date = date;
            this.temperature = temp;
        }
    }
}
