using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;

namespace PrismTest.ViewModels
{
    public class NavigationViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private string _pageTitle = "导航页面";

        public string PageTitle
        {
            get => _pageTitle;
            set => SetProperty(ref _pageTitle, value);
        }

        public ICommand NavigateCommand { get; }

        public NavigationViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string viewName)
        {
            _regionManager.RequestNavigate("ContentRegion", viewName);
        }

        // INavigationAware 接口实现
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 导航到此页面时的逻辑
            if (navigationContext.Parameters.ContainsKey("title"))
            {
                PageTitle = navigationContext.Parameters.GetValue<string>("title");
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            // 离开此页面时的逻辑
        }
    }
}