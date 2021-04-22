using LiveCharts;
using LiveCharts.Wpf;
using MetricsManagerDesktop.Requests;
using MetricsManagerDesktop.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Documents;
using System.Windows.Threading;

namespace MetricsManagerDesktop.ViewModels
{
    public class CpuMetricsCardViewModel : ICpuMetricsCardViewModel, INotifyPropertyChanged
    {
        public SeriesCollection ColumnSeriesValues { get; private set; }
        public int MaxValue { get; private set; }
        private readonly HttpClient _httpClient;
        private DateTimeOffset _lastTime;
        private DispatcherTimer _timer;
        private int _countViewValues;
        private DateTimeOffset fromTime;
        private DateTimeOffset toTime;

        public CpuMetricsCardViewModel()
        {
            _countViewValues = 30;
            _httpClient = new HttpClient();
            _timer = new DispatcherTimer();
            _timer.Tick += timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 1);

            ColumnSeriesValues = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<double> {}
                }
            };

            //UpdateCpuMetrics(new GetAllCpuMetricsApiRequest()
            //{
            //    FromTime = fromTime,
            //    ToTime = toTime,
            //    Agent = 1
            //});
        }

        public void UpdateCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"http://localhost:5050/api/metrics/cpu/agent/{request.Agent}/from/{request.FromTime:o}/to/{request.ToTime:o}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using (var responseStream = response.Content.ReadAsStreamAsync().Result)
                {
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        var content = streamReader.ReadToEnd();
                        var result = JsonSerializer.Deserialize<AllCpuMetricsApiResponse>(content, new JsonSerializerOptions()
                            { PropertyNameCaseInsensitive = true });

                        if (result.Metrics.Count == 0)
                        {
                            return;
                        }
                        foreach (var item in result.Metrics)
                        {
                            AddToCollection(ColumnSeriesValues, (double) item.Value);
                            MaxValue = Math.Max(item.Value, MaxValue);
                        }
                        OnPropertyChanged("MaxValue");
                        _lastTime = result.Metrics[result.Metrics.Count - 1].Time;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void AddToCollection(SeriesCollection collection, double value)
        {
            collection[0].Values.Add(value);
            if (collection[0].Values.Count > 30) collection[0].Values.RemoveAt(0);
        }

        public SeriesCollection GetColumnSeriesValues()
        {
            return ColumnSeriesValues;
        }

        public void StartView()
        {
           _lastTime = fromTime;
            
            if (!_timer.IsEnabled)
            {
                _timer.Start();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateCpuMetrics(new GetAllCpuMetricsApiRequest()
            {
                FromTime = _lastTime,
                ToTime = DateTimeOffset.UtcNow,
                Agent = 1
            });
        }

        public void StopView()
        {
            _timer.Stop();
        }

        public void SetFromTime(DateTimeOffset fromDateTime)
        {
            fromTime = fromDateTime;
        }

        public void ViewRange()
        {
            ResetMaxTime();
            StopView();
            UpdateCpuMetrics(new GetAllCpuMetricsApiRequest()
            {
                FromTime = fromTime,
                ToTime = toTime,
                Agent = 1
            });
        }

        public void SetToTime(DateTimeOffset toDateTime)
        {
            toTime = toDateTime;
        }

        private void ResetMaxTime()
        {
            MaxValue = 0; 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
