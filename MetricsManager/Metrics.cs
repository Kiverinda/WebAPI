using System;

namespace MetricsManager
{
    public class Metrics
    {
        public static int count;
        public int index { get; set; }
        public DateTime date { get; set; }
        public int temperature { get; set; }

        public Metrics(DateTime date, int temp)
        {
            this.date = date;
            this.temperature = temp;
            this.index = ++count;
        }
    }
}
