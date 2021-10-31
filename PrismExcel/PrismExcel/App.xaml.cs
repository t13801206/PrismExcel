using Prism.Ioc;
using Prism.Modularity;
using PrismExcel.Modules.ModuleName;
using PrismExcel.Services;
using PrismExcel.Services.Interfaces;
using PrismExcel.Views;
using System.Windows;

namespace PrismExcel
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
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();

            containerRegistry.RegisterForNavigation<MainTable>();
            containerRegistry.RegisterDialog<FilterDialog, ViewModels.FilterDialogViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }
    }
}
