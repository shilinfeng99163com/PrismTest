using Prism.Ioc;
using Prism.Modularity;
using Prism.DryIoc;  // 改为 DryIoc
using PrismTest.Views;
using System.Windows;
using PrismTest.ViewModels;

namespace PrismTest
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
             return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册服务
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();

            // 注册 ViewModel（Prism 会自动关联 MainWindow 和 MainViewModel）
            containerRegistry.Register<MainViewModel>();
            // 注册服务
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
            containerRegistry.RegisterSingleton<ICustomerService, CustomerService>();

            // 注册视图（用于导航）
            containerRegistry.RegisterForNavigation<CustomerView>();
           
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            // 如果不需要模块化，可以注释掉这行
            // moduleCatalog.AddModule<CustomerModule>();
        }
    }
}
