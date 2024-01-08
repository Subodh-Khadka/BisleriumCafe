using BisleriumCafe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BisleriumCafe.Utils;

namespace BisleriumCafe.Data.Services
{
    public class CartService
    {
        /*public List<CoffeeTypes> SelectedCoffee { get; set; } = new List<CoffeeTypes>();*/
        public CoffeeTypes? SelectedCoffee { get; set; }
        public List<AddIns> SelectedAddIn { get; set; } = new List<AddIns>();
        public Customer CurrentCustomer { get; set; } = new Customer();
        public Order CurrentOrder { get; set; } = new Order();

        /*public bool OrderSubmissionSuccessful { get; set; } */
            
        public void AddCoffeeToCart(CoffeeTypes coffee)
        {
            SelectedCoffee = coffee;
            SelectedAddIn.Clear(); // Clear any previously selected add-ins
        }
        public void AddAddInToCart(AddIns addIns)
        {
            //Checkin if any addIn is already present in the cart 
            if (!SelectedAddIn.Any(existingAddIn => existingAddIn.AddInID == addIns.AddInID))
            {
                SelectedAddIn.Add(addIns);
            }
        }

        public Decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;

            if (SelectedCoffee != null)
            {
                totalPrice += SelectedCoffee.CoffeePrice;
            }
            totalPrice += SelectedAddIn.Sum(a => a.AddInPrice);
            return totalPrice;
        }

        public void SubmitOrder()
        {
            try
            {
                // Load existing customer data
                string customerFilePath = BisleriumUtils.CustomerFilePath();
                List<Customer> existingCustomers = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(customerFilePath)) ?? new List<Customer>();

                // Check if a customer with the same phone number already exists
                Customer existingCustomer = existingCustomers.FirstOrDefault(c => c.PhoneNumber == CurrentCustomer.PhoneNumber);

                if (existingCustomer == null)
                {
                    // Assign a new Id to the current customer
                    CurrentCustomer.Id = Guid.NewGuid();

                    // Add the current customer to the list of existing customers
                    existingCustomers.Add(CurrentCustomer);

                    // Write the updated customer data back to the JSON file
                    File.WriteAllText(customerFilePath, JsonConvert.SerializeObject(existingCustomers, Formatting.Indented));
                }

                // Load existing order data
                string orderFilePath = BisleriumUtils.OrderFilePath();
                List<Order> existingOrders = JsonConvert.DeserializeObject<List<Order>>(File.ReadAllText(orderFilePath)) ?? new List<Order>();

                // Calculate the total price
                decimal totalPrice = CalculateTotalPrice();

                // Check if the current customer is a member
                if (existingCustomer != null && existingCustomer.IsMember)
                {
                    // Apply a 10% discount to the total price
                    decimal discountedPrice = totalPrice * 0.9m;

                    // Create a new Order with the updated details
                    Order newOrder = new Order
                    {
                        OrderId = Guid.NewGuid(),
                        OrderTimestamp = DateTime.UtcNow,
                        Customer = existingCustomer, // Use existing customer if it exists
                        SelectedCoffee = SelectedCoffee != null ? new List<CoffeeTypes> { SelectedCoffee } : new List<CoffeeTypes>(),
                        SelectedAddIns = SelectedAddIn != null ? new List<AddIns>(SelectedAddIn) : new List<AddIns>(),
                        TotalPrice = discountedPrice
                    };

                    // Add the new order to the list of existing orders
                    existingOrders.Add(newOrder);

                    SelectedCoffee = null;
                    SelectedAddIn.Clear();
                }
                else
                {
                    // Create a new Order with the regular total price
                    Order newOrder = new Order
                    {
                        OrderId = Guid.NewGuid(),
                        OrderTimestamp = DateTime.UtcNow,
                        Customer = existingCustomer ?? CurrentCustomer, // Use existing customer if it exists, otherwise use current customer
                        SelectedCoffee = SelectedCoffee != null ? new List<CoffeeTypes> { SelectedCoffee } : new List<CoffeeTypes>(),
                        SelectedAddIns = SelectedAddIn != null ? new List<AddIns>(SelectedAddIn) : new List<AddIns>(),
                        TotalPrice = totalPrice
                    };

                    // Add the new order to the list of existing orders
                    existingOrders.Add(newOrder);

                    SelectedCoffee = null;
                    SelectedAddIn.Clear();
                }


                // Write the updated order data back to the JSON file
                File.WriteAllText(orderFilePath, JsonConvert.SerializeObject(existingOrders, Formatting.Indented));

                // Optionally: Show a success message or navigate to a confirmation page
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while submitting the order: {ex.Message}");
                // Optionally: Show an error message
            }
        }


        /*public void SubmitOrder()
        {
           
            {
                // Load existing customer data
                string customerFilePath = BisleriumUtils.CustomerFilePath();
                List<Customer> existingCustomers = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(customerFilePath)) ?? new List<Customer>();

                // Assign a new Id to the current customer
                CurrentCustomer.Id = Guid.NewGuid();

                // Add the current customer to the list of existing customers
                existingCustomers.Add(CurrentCustomer);

                // Write the updated customer data back to the JSON file
                File.WriteAllText(customerFilePath, JsonConvert.SerializeObject(existingCustomers, Formatting.Indented));

                // Load existing order data
                string orderFilePath = BisleriumUtils.OrderFilePath();
                List<Order> existingOrders = JsonConvert.DeserializeObject<List<Order>>(File.ReadAllText(orderFilePath)) ?? new List<Order>();

                // Calculate the total price
                
                decimal totalPrice = CalculateTotalPrice();

                // Apply a 10% discount to the total price
                decimal discountedPrice = totalPrice * 0.9m;

                // Create a new Order with the updated details
                Order newOrder = new Order
                {
                    OrderId = Guid.NewGuid(),
                    OrderTimestamp = DateTime.UtcNow,
                    Customer = CurrentCustomer,
                    SelectedCoffee = SelectedCoffee != null ? new List<CoffeeTypes> { SelectedCoffee } : new List<CoffeeTypes>(),
                    SelectedAddIns = SelectedAddIn != null ? new List<AddIns>(SelectedAddIn) : new List<AddIns>(),
                    TotalPrice = discountedPrice
                };

                // Add the new order to the list of existing orders
                existingOrders.Add(newOrder);

                // Write the updated order data back to the JSON file
                File.WriteAllText(orderFilePath, JsonConvert.SerializeObject(existingOrders, Formatting.Indented));

                // Reset the selected items in the cart
                SelectedCoffee = null;
                SelectedAddIn.Clear();

                // Optionally: Show a success message or navigate to a confirmation page
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while submitting the order: {ex.Message}");
                // Optionally: Show an error message
            }
        }
*/

        /* public void SubmitOrder()
         {
             try
             {
                 // Load existing customer data
                 string customerFilePath = BisleriumUtils.CustomerFilePath();
                 List<Customer> existingCustomers = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(customerFilePath)) ?? new List<Customer>();

                 // Assign a new Id to the current customer
                 CurrentCustomer.Id = Guid.NewGuid();

                 // Add the current customer to the list of existing customers
                 existingCustomers.Add(CurrentCustomer);

                 // Write the updated customer data back to the JSON file
                 File.WriteAllText(customerFilePath, JsonConvert.SerializeObject(existingCustomers, Formatting.Indented));

                 // Load existing order data
                 string orderFilePath = BisleriumUtils.OrderFilePath();
                 List<Order> existingOrders = JsonConvert.DeserializeObject<List<Order>>(File.ReadAllText(orderFilePath)) ?? new List<Order>();

                 // Create a new Order with the updated details
                 // Create a new Order with the updated details
                 Order newOrder = new Order
                 {
                     OrderId = Guid.NewGuid(),
                     OrderTimestamp = DateTime.UtcNow,
                     Customer = CurrentCustomer,
                     SelectedCoffee = SelectedCoffee != null ? new List<CoffeeTypes> { SelectedCoffee } : new List<CoffeeTypes>(),
                     SelectedAddIns = SelectedAddIn != null ? new List<AddIns>(SelectedAddIn) : new List<AddIns>(),
                     TotalPrice = CalculateTotalPrice()
                 };

                 // Add the new order to the list of existing orders
                 existingOrders.Add(newOrder);

                 // Write the updated order data back to the JSON file
                 File.WriteAllText(orderFilePath, JsonConvert.SerializeObject(existingOrders, Formatting.Indented));

                 // Reset the selected items in the cart
                 SelectedCoffee = null;
                SelectedAddIn.Clear();

                 // Optionally: Show a success message or navigate to a confirmation page
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"An error occurred while submitting the order: {ex.Message}");
                 // Optionally: Show an error message
             }
         }*/


        /*public void SubmitOrder()
        {
            try
            {

                // Store customer details in a JSON file
                string customerFilePath = BisleriumUtils.CustomerFilePath();
                List<Customer> existingCustomers = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(customerFilePath)) ?? new List<Customer>();
                CurrentCustomer.Id = Guid.NewGuid(); // Assign a new Id to the customer
                existingCustomers.Add(CurrentCustomer);
                File.WriteAllText(customerFilePath, JsonConvert.SerializeObject(existingCustomers, Formatting.Indented));

                // Store order details in a JSON file
                string orderFilePath = BisleriumUtils.OrderFilePath();
                List<Order> existingOrders = JsonConvert.DeserializeObject<List<Order>>(File.ReadAllText(orderFilePath)) ?? new List<Order>();

                // Create a new Order with the updated details
                Order newOrder = new Order
                {
                    OrderId = Guid.NewGuid(),
                    OrderTimestamp = DateTime.UtcNow,
                    Customer = CurrentCustomer,
                    SelectedCoffee = SelectedCoffee != null ? new List<CoffeeTypes> { SelectedCoffee } : new List<CoffeeTypes>(),
                    SelectedAddIns = SelectedAddIn.ToList(), // Copy the list to ensure it's not modified externally
                    TotalPrice = CalculateTotalPrice() // Adjust based on your logic for calculating the total order price
                };

                existingOrders.Add(newOrder);
                File.WriteAllText(orderFilePath, JsonConvert.SerializeObject(existingOrders, Formatting.Indented));


            }
            catch (Exception ex)
            {                
                Console.WriteLine($"An error occurred while submitting the order: {ex.Message}");                                
            }
        }*/
    }
}

