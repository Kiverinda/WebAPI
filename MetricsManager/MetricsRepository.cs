using System;
using System.Collections.Generic;

namespace MetricsManager
{
    public class MetricsRepository
    {
        public List<Metrics> metrics { get; set; }

        public MetricsRepository() 
        {
            metrics = new List<Metrics>();
            metrics.Add(new Metrics(DateTime.Parse("2021-12-12T12:11:12"), 12));
            metrics.Add(new Metrics(DateTime.Parse("2021-12-13T12:11:12"), 15));
            metrics.Add(new Metrics(DateTime.Parse("2021-12-13T11:11:12"), 3));
            metrics.Add(new Metrics(DateTime.Parse("2021-12-14T15:11:12"), -6));
            metrics.Add(new Metrics(DateTime.Parse("2021-12-14T16:11:12"), 2));
            metrics.Add(new Metrics(DateTime.Parse("2021-12-14T17:11:12"), 8));
            metrics.Add(new Metrics(DateTime.Parse("2021-12-14T18:11:12"), 9));
            metrics.Add(new Metrics(DateTime.Parse("2021-12-15T12:11:12"), -5));
            metrics.Add(new Metrics(DateTime.Parse("2021-12-15T13:11:12"), -13));
        }

        public List<Metrics> Read(DateTime fromDate, DateTime toDate)
        {
            List<Metrics> list = new List<Metrics>();
            foreach(Metrics m in metrics)
            {
                if (m.date >= fromDate && m.date <= toDate) list.Add(m);
            }
            return list;
        }

        public void Add(DateTime date, int temperature)
        {
            metrics.Add(new Metrics(date, temperature));
        }

        public void Delete(DateTime fromDate, DateTime toDate)
        {
            for (int i = 0; i < metrics.Count; i++)
            {
                if (metrics[i].date >= fromDate && metrics[i].date <= toDate)
                {
                    metrics.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Update(DateTime date, int temperature)
        {
            foreach (Metrics m in metrics)
            {
                if (m.date == date) m.temperature = temperature;
            }
        }
    }
}