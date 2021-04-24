﻿using MetricsManagerDesktop.UserControls;
using MetricsManagerDesktop.ViewModels;
using System.Windows;
using MetricsManagerDesktop.Models;
using Unity;

namespace MetricsManagerDesktop
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ICpuMetricsCardViewModel, CpuMetricsCardViewModel>();
            container.RegisterType<ICpuMetricsCardModel, CpuMetricsCardModel>();
            container.RegisterType<ICpuMetricsCard, CpuMetricsCard>();
            container.RegisterType<IHddMetricsCardViewModel, HddMetricsCardViewModel>();
            container.RegisterType<IHddMetricsCardModel, HddMetricsCardModel>();
            container.RegisterType<IHddMetricsCard, HddMetricsCard>();
            container.RegisterType<IAgentViewModel, AgentViewModel>();
            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
