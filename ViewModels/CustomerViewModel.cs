using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PrismTest.ViewModels
{
    // 客户视图模型类，继承 BindableBase 提供属性变更通知功能
    public class CustomerViewModel : BindableBase
    {
        // 客户服务的只读引用，通过依赖注入获得
        private readonly ICustomerService _customerService;

        // 存储客户集合数据的私有字段
        private ObservableCollection<Customer> _customers;

        // 存储当前选中客户的私有字段
        private Customer _selectedCustomer;

        // 客户集合属性，绑定到界面控件（如 DataGrid）
        public ObservableCollection<Customer> Customers
        {
            get => _customers;  // 返回私有字段值
            set => SetProperty(ref _customers, value);  // 设置值并触发属性变更通知
        }

        // 选中客户属性，绑定到界面的 SelectedItem
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;  // 返回当前选中的客户
            set => SetProperty(ref _selectedCustomer, value);  // 设置选中客户并通知界面更新
        }

        // 加载客户数据的命令，绑定到"刷新"或"加载"按钮
        public ICommand LoadCustomersCommand { get; }

        // 添加新客户的命令，绑定到"添加"按钮
        public ICommand AddCustomerCommand { get; }

        // 删除客户的命令，绑定到"删除"按钮
        public ICommand DeleteCustomerCommand { get; }

        // 构造函数，接收依赖注入的客户服务
        public CustomerViewModel(ICustomerService customerService)
        {
            // 保存注入的客户服务引用
            _customerService = customerService;

            // 初始化客户集合为空集合
            Customers = new ObservableCollection<Customer>();

            // 创建加载客户命令，执行 LoadCustomers 方法
            LoadCustomersCommand = new DelegateCommand(LoadCustomers);

            // 创建添加客户命令，执行 AddCustomer 方法
            AddCustomerCommand = new DelegateCommand(AddCustomer);

            // 创建删除客户命令，执行 DeleteCustomer 方法，可执行条件是 CanDeleteCustomer
            // ObservesProperty 监听 SelectedCustomer 属性变化，自动更新命令的可执行状态
            DeleteCustomerCommand = new DelegateCommand(DeleteCustomer, CanDeleteCustomer)
                .ObservesProperty(() => SelectedCustomer);

            // 构造完成后自动加载客户数据
            LoadCustomers();
        }

        // 从服务加载所有客户数据到界面集合
        private void LoadCustomers()
        {
            // 调用服务获取所有客户数据
            var customers = _customerService.GetAllCustomers();

            // 清空现有的客户集合
            Customers.Clear();

            // 遍历获取的客户数据，逐个添加到界面集合中
            // 使用 foreach 而不是直接赋值，确保 ObservableCollection 正确触发变更通知
            foreach (var customer in customers)
            {
                Customers.Add(customer);
            }
        }

        // 添加新客户的方法
        private void AddCustomer()
        {
            // 创建新的客户对象，自动生成名称和邮箱
            var newCustomer = new Customer
            {
                Name = $"新客户 {Customers.Count + 1}",  // 根据当前客户数量生成名称
                Email = $"customer{Customers.Count + 1}@example.com"  // 根据当前客户数量生成邮箱
            };

            // 通过服务将新客户保存到数据源（数据库、文件等）
            _customerService.AddCustomer(newCustomer);

            // 将新客户添加到界面集合，界面会自动显示新添加的客户
            Customers.Add(newCustomer);
        }

        // 删除选中客户的方法
        private void DeleteCustomer()
        {
            // 检查是否有选中的客户
            if (SelectedCustomer != null)
            {
                // 通过服务从数据源删除选中的客户
                _customerService.DeleteCustomer(SelectedCustomer.Id);

                // 从界面集合中移除选中的客户
                Customers.Remove(SelectedCustomer);

                // 清空选中状态，避免引用已删除的对象
                SelectedCustomer = null;
            }
        }

        // 判断是否可以执行删除操作的方法
        private bool CanDeleteCustomer()
        {
            // 只有当前有选中客户时才允许删除操作
            return SelectedCustomer != null;
        }
    }
}