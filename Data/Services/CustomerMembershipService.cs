using System;
using System.Collections.Generic;
using System.Linq;
using BisleriumCafe.Data.Models;
using BisleriumCafe.Utils;
using Newtonsoft.Json;

namespace BisleriumCafe.Data.Services
{
    public class CustomerMembershipService
    {
        string customerFilePath = BisleriumUtils.CustomerFilePath();

        public List<Customer> RetrieveCustomerData()
        {
            try
            {
                string json = System.IO.File.ReadAllText(customerFilePath);
                return JsonConvert.DeserializeObject<List<Customer>>(json) ?? new List<Customer>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Customer>();
            }
        }

        public void SaveCustomerData(List<Customer> customers)
        {
            try
            {
                string json = JsonConvert.SerializeObject(customers, Formatting.Indented);
                System.IO.File.WriteAllText(customerFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception/Error: {ex.Message}");
            }
        }

        public Customer MakeCustomerMember(string phoneNumber)
        {
            var customers = RetrieveCustomerData();

            // Finding the customer with the provided phone number
            var customer = customers.FirstOrDefault(c => c.PhoneNumber == phoneNumber);

            if (customer != null)
            {                
                customer.IsMember = true;                                
                SaveCustomerData(customers);
            }
            return customer;
        }        
    }
}
