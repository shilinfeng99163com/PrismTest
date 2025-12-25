using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismTest.Views;

namespace PrismTest.Modules
{
    // 定义客户管理模块类，继承自 IModule 接口
    // IModule 是 Prism 框架要求所有模块必须实现的接口
    public class CustomerModule : IModule
    {
        // OnInitialized 方法：模块初始化完成后自动调用的方法
        // 参数 containerProvider：容器提供者，用于从容器中解析已注册的服务实例
        // 调用时机：在所有模块的 RegisterTypes 方法执行完毕后调用
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // 模块初始化后的逻辑（注释说明这里是初始化逻辑的地方）

            // 从容器中解析 IRegionManager 实例
            // IRegionManager 是 Prism 的区域管理器，负责管理 UI 中的各个显示区域
            var regionManager = containerProvider.Resolve<IRegionManager>();

            // 将 CustomerView 视图注册到名为 "CustomerRegion" 的区域中
            // 第一个参数："CustomerRegion" - 区域名称，对应 XAML 中定义的区域
            // 第二个参数：typeof(CustomerView) - 要显示的视图类型
            // 效果：CustomerView 会立即自动显示在 CustomerRegion 区域中
            regionManager.RegisterViewWithRegion("CustomerRegion", typeof(CustomerView));
        }

        // RegisterTypes 方法：依赖注入注册方法，在应用启动时自动调用
        // 参数 containerRegistry：容器注册器，用于向容器注册类型映射关系
        // 调用时机：在应用启动的早期阶段，所有模块的此方法会被依次调用
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册模块特定的服务和视图（注释说明这里是注册依赖的地方）

            // 注册服务接口和实现类的映射关系
            // 第一个泛型参数：ICustomerService - 接口类型（抽象）
            // 第二个泛型参数：CustomerService - 实现类型（具体实现）
            // 效果：当其他类需要 ICustomerService 时，容器会自动提供 CustomerService 的实例
            containerRegistry.Register<ICustomerService, CustomerService>();
        }
    }

}