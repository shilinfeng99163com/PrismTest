using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PrismTest.ViewModels
{
    public class CustomerViewModel : BindableBase
    {
        private readonly ICustomerService _customerService;
        private ObservableCollection<Customer> _customers;
        private Customer _selectedCustomer;

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetProperty(ref _selectedCustomer, value);
        }

        public ICommand LoadCustomersCommand { get; }
        public ICommand AddCustomerCommand { get; }
        public ICommand DeleteCustomerCommand { get; }

        public CustomerViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
            Customers = new ObservableCollection<Customer>();

            LoadCustomersCommand = new DelegateCommand(LoadCustomers);
            AddCustomerCommand = new DelegateCommand(AddCustomer);
            DeleteCustomerCommand = new DelegateCommand(DeleteCustomer, CanDeleteCustomer)
                .ObservesProperty(() => SelectedCustomer);

            // 自动加载数据
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            Customers.Clear();
            foreach (var customer in customers)
            {
                Customers.Add(customer);
            }
        }

        private void AddCustomer()
        {
            var newCustomer = new Customer
            {
                Name = $"新客户 {Customers.Count + 1}",
                Email = $"customer{Customers.Count + 1}@example.com"
            };

            _customerService.AddCustomer(newCustomer);
            Customers.Add(newCustomer);
        }

        private void DeleteCustomer()
        {
            if (SelectedCustomer != null)
            {
                _customerService.DeleteCustomer(SelectedCustomer.Id);
                Customers.Remove(SelectedCustomer);
                SelectedCustomer = null;
            }
        }

        private bool CanDeleteCustomer()
        {
            return SelectedCustomer != null;
        }
    }
}