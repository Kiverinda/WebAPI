using MetricsManagerDesktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MetricsManagerDesktop.UserControls;

namespace MetricsManagerDesktop
{
    public partial class MainWindow : Window
    {
        private ICpuMetricsCard _cpu;
        private IHddMetricsCard _hdd;
        private IRamMetricsCard _ram;
        private IAgentViewModel _agent;

        public MainWindow(ICpuMetricsCard cpu, IAgentViewModel agent, IHddMetricsCard hdd, IRamMetricsCard ram)
        {
            InitializeComponent();
            FromDateTime.Value = DateTime.Now.AddDays(-1);
            _ram = ram;
            _cpu = cpu;
            _hdd = hdd;
            _agent = agent;
            DataContext = agent;
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
                        case 1:
                            _hdd.SetFromTime((DateTimeOffset)FromDateTime.Value);
                            _hdd.SetToTime((DateTimeOffset)ToDateTime.Value);
                            Panel.Children.Add(_hdd as UIElement);
                            break;
                        case 2:
                            _ram.SetFromTime((DateTimeOffset)FromDateTime.Value);
                            _ram.SetToTime((DateTimeOffset)ToDateTime.Value);
                            Panel.Children.Add(_ram as UIElement);
                            break;
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

        private void ComboBox_AgentSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;
            _cpu.SetAgent((KeyValuePair<int, string>)e.AddedItems[0]);
            _hdd.SetAgent((KeyValuePair<int, string>)e.AddedItems[0]);
            _ram.SetAgent((KeyValuePair<int, string>)e.AddedItems[0]);
        }


        private void ButtonClickStartRealTime(object sender, RoutedEventArgs e)
        {
            foreach (UIElement itemChild in Panel.Children)
            {
                if (itemChild == _cpu)
                {
                    _cpu.SetFromTime((DateTimeOffset)FromDateTime.Value);
                    _cpu.SetToTime((DateTimeOffset)ToDateTime.Value);
                    _cpu.StartView();
                }

                if (itemChild == _hdd)
                {
                    _hdd.SetFromTime((DateTimeOffset)FromDateTime.Value);
                    _hdd.SetToTime((DateTimeOffset)ToDateTime.Value);
                    _hdd.StartView();
                }

                if (itemChild == _ram)
                {
                    _ram.SetFromTime((DateTimeOffset)FromDateTime.Value);
                    _ram.SetToTime((DateTimeOffset)ToDateTime.Value);
                    _ram.StartView();
                }
            }
            //_cpu.SetFromTime(DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 20));
            //_cpu.SetToTime(DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
            //_cpu.StartView();
            //_hdd.SetFromTime(DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 20));
            //_hdd.SetToTime(DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
            //_hdd.StartView();
            //_ram.SetFromTime(DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 20));
            //_ram.SetToTime(DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
            //_ram.StartView();
        }

        private void ButtonClickStoptRealTime(object sender, RoutedEventArgs e)
        {
            _cpu.StopView();
            _hdd.StopView();
            _ram.StopView();
        }

        private void ButtonClickViewRange(object sender, RoutedEventArgs e)
        {
            foreach (UIElement itemChild in Panel.Children)
            {
                if (itemChild == _cpu)
                {
                    _cpu.SetFromTime((DateTimeOffset)FromDateTime.Value);
                    _cpu.SetToTime((DateTimeOffset)ToDateTime.Value);
                    _cpu.ViewRange();
                }

                if (itemChild == _hdd)
                {
                    _hdd.SetFromTime((DateTimeOffset)FromDateTime.Value);
                    _hdd.SetToTime((DateTimeOffset)ToDateTime.Value);
                    _hdd.ViewRange();
                }

                if (itemChild == _ram)
                {
                    _ram.SetFromTime((DateTimeOffset)FromDateTime.Value);
                    _ram.SetToTime((DateTimeOffset)ToDateTime.Value);
                    _ram.ViewRange();
                }
            }
            //_cpu.SetFromTime((DateTimeOffset)FromDateTime.Value);
            //_cpu.SetToTime((DateTimeOffset)ToDateTime.Value);
            //_cpu.ViewRange();
            //_hdd.SetFromTime((DateTimeOffset)FromDateTime.Value);
            //_hdd.SetToTime((DateTimeOffset)ToDateTime.Value);
            //_hdd.ViewRange();
        }
    }
}
