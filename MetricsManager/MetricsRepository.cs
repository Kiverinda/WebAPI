using System;
using System.Collections.Generic;

namespace MetricsManager
{
    public class MetricsRepository
    {
        public List<Metrics> metrics { get; set; }
        private static  MetricsRepository instance { get; set; }

        private MetricsRepository() 
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

        internal List<Metrics> Read(DateTime dateTime1, DateTime dateTime2)
        {
            List<Metrics> list = new List<Metrics>();
            for (int i = 0; i < metrics.Count; i++)
            {
                if (metrics[i].date > dateTime1 && metrics[i].date < dateTime2)
                {
                    list.Add(metrics[i]);
                }
            }
            return list;
        }

        public static MetricsRepository getInstance()
        {
            if (instance == null)
                instance = new MetricsRepository();
            return instance;
        }

        public void Add(DateTime date, int temp)
        {
            metrics.Add(new Metrics(date, temp));
        }

        public void Delete(int index)
        {
            for (int i = 0; i < metrics.Count; i++)
            {
                if (metrics[i].index == index)
                {
                    metrics.RemoveAt(i);
                    break;
                }
            }
        }

        public void Delete(DateTime date1, DateTime date2)
        {
            for (int i = 0; i < metrics.Count; i++)
            {
                if (metrics[i].date > date1 && metrics[i].date < date2)
                {
                    metrics.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Update(DateTime date, int temp)
        {
            foreach (Metrics m in this.metrics)
            {
                if (m.date == date) m.temperature = temp;
            }
        }
    }
}