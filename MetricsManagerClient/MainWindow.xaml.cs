using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;
using MetricsManagerClient.Controllers;


namespace MetricsManagerClient
{
    public partial class MainWindow : Window
    {
        private MaterialCards _hddChart;
        private MaterialCards _ramChart;
        private MaterialCards _dotnetChart;
        private MaterialCards _networkChart;
        private Timer timer; 

        public MainWindow()
        {
            _hddChart = new MaterialCards() { Name = "HddChart" };
            _hddChart.TextBlockName.Text = "Hdd";
            _hddChart.Width = 400;
            _hddChart.Height = 300;
            _hddChart.ColumnSeriesValues = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<double> { 10,20,30,90,10,45,10,50,0,100}
                }
            };
            _ramChart = new MaterialCards() { Name = "RamChart" };
            _ramChart.TextBlockName.Text = "Ram";
            _ramChart.Width = 400;
            _ramChart.Height = 300;
            _ramChart.ColumnSeriesValues = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<double> { 10,20,30,90,10,45,10,50,0,100}
                }
            };
            _networkChart = new MaterialCards() { Name = "NetworkChart" };
            _networkChart.TextBlockName.Text = "Network";
            _networkChart.Width = 400;
            _networkChart.Height = 300;
            _networkChart.ColumnSeriesValues = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<double> { 10,20,30,90,10,45,10,50,0,100}
                }
            };
            _dotnetChart = new MaterialCards() { Name = "DotNetChart" };
            _dotnetChart.TextBlockName.Text = "DotNet";
            _dotnetChart.Width = 400;
            _dotnetChart.Height = 300;
            _dotnetChart.ColumnSeriesValues = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<double> { 10,20,30,90,10,45,10,50,0,100}
                }
            };
            
        }

        private void ButtonClickStart(object sender, RoutedEventArgs e)
        {
            if (timer == null)
            {
                timer = new Timer(new TimerCallback(AddValuesAll), null, 0, 5000);
            }
        }
        private void ButtonClickStop(object sender, RoutedEventArgs e)
        {
            timer = null;
        }

        private void AllMetricsClear()
        {
            _hddChart.ColumnSeriesValues[0].Values.Clear();
            _ramChart.ColumnSeriesValues[0].Values.Clear();
            _networkChart.ColumnSeriesValues[0].Values.Clear();
            _dotnetChart.ColumnSeriesValues[0].Values.Clear();
        }

        private void AddValuesAll(object ob)
        {
            _hddChart.ColumnSeriesValues[0].Values.Add(48d);
            _ramChart.ColumnSeriesValues[0].Values.Add(48d);
            _networkChart.ColumnSeriesValues[0].Values.Add(48d);
            _dotnetChart.ColumnSeriesValues[0].Values.Add(48d);
        }

        private void lst_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is CheckBox)
            {
                listSelectMetrics.SelectedItem = e.OriginalSource;
            }
            Panel.Children.Clear();
            for (var count = 0; count < listSelectMetrics.Items.Count; count++)
            {
                if(((CheckBox)listSelectMetrics.Items[count]).IsChecked == true)
                {
                    switch (count)
                    {
                        case 0:
                        //     Panel.Children.Add(_cpuChart);
                            break;
                        case 1:
                            Panel.Children.Add(_hddChart);
                            break;
                        case 2:
                            Panel.Children.Add(_ramChart);
                            break;
                        case 3:
                            Panel.Children.Add(_networkChart);
                            break;
                        case 4:
                            Panel.Children.Add(_dotnetChart);
                            break;
                    }
                }
            }
        }
        private void CpuChart_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
