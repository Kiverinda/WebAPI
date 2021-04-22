using MetricsManagerDesktop.Interfaces;
using MetricsManagerDesktop.UserControls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MetricsManagerDesktop
{
    public partial class MainWindow : Window
    {
        private ICpuMetricsCard _cpu;
        

        public MainWindow(ICpuMetricsCard cpu)
        {
            InitializeComponent();
            FromDateTime.Value = DateTime.Now.AddDays(-1);
            _cpu = cpu;
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
                if (((CheckBox)listSelectMetrics.Items[count]).IsChecked == true)
                {
                    switch (count)
                    {
                        case 0:
                            _cpu.SetFromTime((DateTimeOffset)FromDateTime.Value);
                            _cpu.SetToTime((DateTimeOffset)ToDateTime.Value);

                            Panel.Children.Add(_cpu as UIElement);
                            break;
                        //case 1:
                        //    Panel.Children.Add(_hddChart);
                        //    break;
                        //case 2:
                        //    Panel.Children.Add(_ramChart);
                        //    break;
                        //case 3:
                        //    Panel.Children.Add(_networkChart);
                        //    break;
                        //case 4:
                        //    Panel.Children.Add(_dotnetChart);
                        //    break;
                    }
                }
            }
        }

        private void ButtonClickStartRealTime(object sender, RoutedEventArgs e)
        {
            _cpu.SetFromTime(DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 20));
            _cpu.SetToTime(DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
            _cpu.StartView();
        }

        private void ButtonClickStoptRealTime(object sender, RoutedEventArgs e)
        {
            _cpu.StopView();
        }

        private void ButtonClickViewRange(object sender, RoutedEventArgs e)
        {
            _cpu.SetFromTime((DateTimeOffset)FromDateTime.Value);
            _cpu.SetToTime((DateTimeOffset)ToDateTime.Value);
            _cpu.ViewRange();
        }
    }
}
