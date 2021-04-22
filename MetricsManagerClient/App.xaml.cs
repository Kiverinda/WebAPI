using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Castle.Windsor;
using Unity;

namespace MetricsManagerClient
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            //container.RegisterType<ICustomerService, CustomerService>();
            //container.RegisterType<IShoppingCartService, ShoppingCartService>();

            MainWindow viewModel = container.Resolve<MainWindow>();
            var mainWindow = new MainWindow();
            mainWindow.DataContext = viewModel;
            mainWindow.Show();
        }
    }
}
