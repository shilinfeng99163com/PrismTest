using Prism.DryIoc;  // 改为 DryIoc
using Prism.Ioc;
using Prism.Modularity;
using PrismTest.Modules;
using PrismTest.ViewModels;
using PrismTest.Views;
using System.Windows;

namespace PrismTest
{
    //    应用启动
    //    ↓
    //ConfigureModuleCatalog() 配置模块目录
    //    ↓
    //Prism 发现并实例化各个 IModule
    //    ↓
    //调用每个模块的 RegisterTypes() 方法
    //    ├── CustomerModule.RegisterTypes()
    //    ├── OrderModule.RegisterTypes()
    //    └── ReportModule.RegisterTypes()
    //    ↓
    //调用每个模块的 OnInitialized() 方法
    //    ├── CustomerModule.OnInitialized()
    //    ├── OrderModule.OnInitialized()
    //    └── ReportModule.OnInitialized()
    //    ↓
    //CreateShell() 创建主窗口
    //    ↓
    //应用程序启动完成




    // 应用程序启动流程：

    //1. App.xaml.cs 构造函数执行
    //2. OnStartup() 方法被调用
    //3. Prism 初始化开始
    //4. RegisterTypes() 自动调用
    //5. ConfigureModuleCatalog() 调用
    //6. CreateShell() 调用
    //7. 主窗口显示
    //8. 应用程序开始运行

    /// <summary>
    /// 应用程序主类，继承自 PrismApplication
    /// 负责应用程序的初始化、依赖注入容器配置和模块管理
    /// </summary>
    public partial class App : PrismApplication
    {
        /// <summary>
        /// 创建并返回应用程序的主窗口
        /// 这个方法在应用程序启动时被 Prism 框架调用
        /// </summary>
        /// <returns>应用程序的主窗口实例</returns>
        protected override Window CreateShell()
        {
            // 从 DI 容器中解析 MainWindow 实例
            // Prism 会自动处理 MainWindow 与 MainViewModel 的绑定
            // Container.Resolve<MainWindow>() 会自动选择"最贪婪"的构造函数
            // 即参数最多的构造函数，因为容器会尽可能满足所有依赖
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// 注册类型到依赖注入容器
        /// 在这里配置所有需要进行依赖注入的服务、视图模型和视图
        /// </summary>
        /// <param name="containerRegistry">容器注册器</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // ==================== 服务注册 ====================

            // 注册消息服务为单例模式
            // 单例模式确保整个应用程序中只有一个 MessageService 实例
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();

            // 注册客户服务为单例模式
            // 通常业务服务类使用单例模式以保持状态一致性
            containerRegistry.RegisterSingleton<ICustomerService, CustomerService>();

            // ==================== 视图模型注册 ====================

            // 注册主视图模型
            // Prism 会自动将 MainViewModel 与 MainWindow 进行关联
            // 每次请求时创建新实例（非单例）
            containerRegistry.Register<MainViewModel>();

            // ==================== 视图注册（用于导航） ====================

            // 注册客户视图用于 Prism 导航系统
            // 这允许通过 IRegionManager.RequestNavigate() 方法导航到此视图
            containerRegistry.RegisterForNavigation<CustomerView>();

            // 注意：如果有更多视图需要导航，可以继续添加：
            // containerRegistry.RegisterForNavigation<OrderView>();
            // containerRegistry.RegisterForNavigation<ProductView>();
        }

        /// <summary>
        /// 配置模块目录
        /// 用于模块化应用程序的模块加载配置
        /// </summary>
        /// <param name="moduleCatalog">模块目录</param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            // ==================== 模块注册 ====================

            // 如果应用程序采用模块化架构，在这里添加模块
            // 模块化可以帮助大型应用程序实现功能分离和按需加载

            // 示例：添加客户管理模块
            moduleCatalog.AddModule<CustomerModule>();

            // 示例：添加订单管理模块
            //moduleCatalog.AddModule<OrderModule>();

            // 注意：如果不使用模块化架构，可以将此方法留空
            // 对于小型应用程序，通常不需要模块化
        }
    }
}