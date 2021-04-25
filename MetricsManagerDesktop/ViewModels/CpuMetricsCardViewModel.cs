using LiveCharts;
using LiveCharts.Wpf;
using MetricsManagerDesktop.Models;
using MetricsManagerDesktop.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Threading;

namespace MetricsManagerDesktop.ViewModels
{
    public class CpuMetricsCardViewModel : ICpuMetricsCardViewModel, INotifyPropertyChanged
    {
        private readonly ICpuMetricsCardModel _model;
        public SeriesCollection ColumnSeriesValues { get; private set; }
        public int MaxValue { get; private set; }
        private DateTimeOffset _lastTime;
        private DispatcherTimer _timer;
        private KeyValuePair<int, string> _agent;
        private DateTimeOffset fromTime;
        private DateTimeOffset toTime;

        public CpuMetricsCardViewModel(ICpuMetricsCardModel model)
        {
            _model = model;
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
        }

        public void UpdateCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var result = _model.GetCpuMetrics(request);
            if (result == null || result.Metrics.Count == 0)
            {
                return;
            }
            foreach (var item in result.Metrics)
            {
                AddToCollection(ColumnSeriesValues, (double)item.Value);
                MaxValue = Math.Max(item.Value, MaxValue);
            }
            OnPropertyChanged("MaxValue");
            _lastTime = result.Metrics[result.Metrics.Count - 1].Time;
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
                Agent = _agent.Key
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
                Agent = _agent.Key
            });
        }

        public void SetToTime(DateTimeOffset toDateTime)
        {
            toTime = toDateTime;
        }

        public void SetAgent(KeyValuePair<int, string> agent)
        {
            _agent = agent;
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
