using System.Collections.Generic;

namespace PrismTest
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();

        void AddCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        void DeleteCustomer(int customerId);
    }
}