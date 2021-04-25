using MetricsManagerDesktop.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace MetricsManagerDesktop.UserControls
{
    public sealed partial class CpuMetricsCard : UserControl, INotifyPropertyChanged, ICpuMetricsCard
    {
        private ICpuMetricsCardViewModel _viewModel;
        
        public CpuMetricsCard(ICpuMetricsCardViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartView()
        {
            _viewModel.StartView();
        }

        public void StopView()
        {
            _viewModel.StopView();
        }

        public void SetFromTime(DateTimeOffset fromTime)
        {
            _viewModel.SetFromTime(fromTime);
        }

        public void SetToTime(DateTimeOffset toTime)
        {
            _viewModel.SetToTime(toTime);
        }

        public void SetAgent(KeyValuePair<int, string> agent)
        {
            _viewModel.SetAgent(agent);
        }

        public void ViewRange()
        {
            _viewModel.ViewRange();
        }
    }
}
