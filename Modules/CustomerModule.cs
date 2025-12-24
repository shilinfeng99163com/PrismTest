using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismTest.Views;

namespace PrismTest.Modules
{
    public class CustomerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // 模块初始化后的逻辑
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("CustomerRegion", typeof(CustomerView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册模块特定的服务和视图
            containerRegistry.Register<ICustomerService, CustomerService>();
        }
    }
}