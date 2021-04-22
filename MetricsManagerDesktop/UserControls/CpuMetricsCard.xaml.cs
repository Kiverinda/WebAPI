using System;
using MetricsManagerDesktop.Interfaces;
using System.ComponentModel;
using System.Windows.Controls;
using MetricsManagerDesktop.ViewModels;

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
            handler?.Invoke(this, new
                PropertyChangedEventArgs(propertyName));
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

        public void ViewRange()
        {
            _viewModel.ViewRange();
        }
    }
}
