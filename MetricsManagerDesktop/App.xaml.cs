using MetricsManagerDesktop.Interfaces;
using MetricsManagerDesktop.UserControls;
using MetricsManagerDesktop.ViewModels;
using System.Windows;
using Unity;

namespace MetricsManagerDesktop
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ICpuMetricsCardViewModel, CpuMetricsCardViewModel>();
            container.RegisterType<ICpuMetricsCard, CpuMetricsCard>();
            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
