using UsePrismApp.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using UsePrismApp.ViewModels;

namespace UsePrismApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewControlFirst>();

            containerRegistry.RegisterForNavigation<ViewControlThird>();

            containerRegistry.RegisterDialog<ViewControlSecond, ViewControlSecondViewModel>();
        }
    }
}
