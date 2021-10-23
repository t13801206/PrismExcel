using Prism.Mvvm;
using Prism.Regions;
using PrismExcel.Core;

namespace PrismExcel.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel(IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion(RegionNames.MainTableRegion, typeof(Views.MainTable));
        }
    }
}
