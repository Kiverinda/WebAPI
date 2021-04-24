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
    public class RamMetricsCardViewModel : IRamMetricsCardViewModel, INotifyPropertyChanged
    {
        private readonly IRamMetricsCardModel _model;
        public SeriesCollection RamColumnSeriesValues { get; private set; }
        public int MaxValue { get; private set; }
        public int MinValue { get; private set; }
        private DateTimeOffset _lastTime;
        private DispatcherTimer _timer;
        private KeyValuePair<int, string> _agent;
        private DateTimeOffset fromTime;
        private DateTimeOffset toTime;

        public RamMetricsCardViewModel(IRamMetricsCardModel model)
        {
            _model = model;
            _timer = new DispatcherTimer();
            _timer.Tick += timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 1);

            RamColumnSeriesValues = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<double> {}
                }
            };
        }

        public void UpdateRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var result = _model.GetRamMetrics(request);
            if (result == null || result.Metrics.Count == 0)
            {
                return;
            }
            MinValue = (int)result.Metrics[0].Value;
            foreach (var item in result.Metrics)
            {
                AddToCollection(item.Value);
                MaxValue = Math.Max((int)item.Value, MaxValue);
                MinValue = Math.Min((int)item.Value, MinValue);
            }
            OnPropertyChanged("MaxValue");
            OnPropertyChanged("MinValue");
            _lastTime = result.Metrics[result.Metrics.Count - 1].Time;
        }

        private void AddToCollection(double value)
        {
            RamColumnSeriesValues[0].Values.Add(Math.Truncate(value));
            if (RamColumnSeriesValues[0].Values.Count > 20) RamColumnSeriesValues[0].Values.RemoveAt(0);
        }

        public SeriesCollection GetColumnSeriesValues()
        {
            return RamColumnSeriesValues;
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
            UpdateRamMetrics(new GetAllRamMetricsApiRequest()
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
            UpdateRamMetrics(new GetAllRamMetricsApiRequest()
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
