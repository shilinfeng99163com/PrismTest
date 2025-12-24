using System.Collections.Generic;
using System.Linq;

namespace PrismTest
{
    public class CustomerService : ICustomerService
    {
        private readonly List<Customer> _customers;

        public CustomerService()
        {
            // 模拟一些初始数据
            _customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "张三", Email = "zhangsan@example.com" },
                new Customer { Id = 2, Name = "李四", Email = "lisi@example.com" },
                new Customer { Id = 3, Name = "王五", Email = "wangwu@example.com" }
            };
        }

        public List<Customer> GetAllCustomers()
        {
            return _customers.ToList();
        }

        public void AddCustomer(Customer customer)
        {
            if (customer != null)
            {
                customer.Id = _customers.Count > 0 ? _customers.Max(c => c.Id) + 1 : 1;
                _customers.Add(customer);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = _customers.FirstOrDefault(c => c.Id == customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Email = customer.Email;
            }
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == customerId);
            if (customer != null)
            {
                _customers.Remove(customer);
            }
        }
    }
}