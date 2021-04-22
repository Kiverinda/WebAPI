using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MetricsManagerDesktop.UserControls
{
    public partial class MaterialCards : UserControl, INotifyPropertyChanged
    {
        public MaterialCards()
        {
            InitializeComponent();
            ColumnSeriesValues = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<double> { 10,20,30,40,50,60,70,80,90,100}
                }
            };
            DataContext = this;
        }

        private SeriesCollection ColumnSeriesValues { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new
                PropertyChangedEventArgs(propertyName));
        }
        private void UpdateOnСlick(object sender, RoutedEventArgs e)
        {
            TimePowerChart.Update(true);
        }
    }
}
