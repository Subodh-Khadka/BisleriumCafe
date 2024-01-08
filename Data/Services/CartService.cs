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

        public void AddCoffeeToCart(CoffeeTypes coffee)
        {
            SelectedCoffee = coffee;
            SelectedAddIn.Clear();
        }
        public void AddAddInToCart(AddIns addIns)
        {
            //Checking if any addIn is already present in the cart 
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
                // Loading the existing customer data
                string customerFilePath = BisleriumUtils.CustomerFilePath();
                List<Customer> existingCustomers = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(customerFilePath)) ?? new List<Customer>();

                // Checking if a customer with the same phone number already exists in the CustomerJson
                Customer existingCustomer = existingCustomers.FirstOrDefault(c => c.PhoneNumber == CurrentCustomer.PhoneNumber);

                if (existingCustomer == null)
                {
                    // Assigning a new Id to the current customer
                    CurrentCustomer.Id = Guid.NewGuid();

                    // Adding the current-customer to the list of existing customers if the customer does not exist with the same phoneNumber
                    existingCustomers.Add(CurrentCustomer);
                    existingCustomer = CurrentCustomer; // Set existingCustomer to the newly created customer
                    existingCustomer.PurchaseCount += 1;
                }
                else
                {
                    // Increase the PurchaseCount count by 1 for existing customers
                    existingCustomer.PurchaseCount += 1;
                }

                // Writing the updated customer data back to the Customer JSON file
                File.WriteAllText(customerFilePath, JsonConvert.SerializeObject(existingCustomers, Formatting.Indented));

                // Loading existing order data
                string orderFilePath = BisleriumUtils.OrderFilePath();
                List<Order> existingOrders = JsonConvert.DeserializeObject<List<Order>>(File.ReadAllText(orderFilePath)) ?? new List<Order>();

                // Calculating the total price
                decimal totalPrice = CalculateTotalPrice();
                decimal discountedPrice = totalPrice; // Initialize with the regular total price

                // Check if the current customer is a member
                if (existingCustomer.IsMember)
                {
                    // Check if the purchase count is a multiple of 3 (3rd, 6th, 9th, and so on)
                    if (existingCustomer.PurchaseCount % 3 == 0)
                    {
                        // Apply a 100% discount on the coffee price for every 3rd purchase
                        discountedPrice -= SelectedCoffee.CoffeePrice;
                    }

                    // Apply a 10% discount to the total price for the member
                    discountedPrice *= 0.9m;
                }

                // Create a new Order with the updated details
                Order newOrder = new Order
                {
                    OrderId = Guid.NewGuid(),
                    OrderTimestamp = DateTime.UtcNow,
                    Customer = existingCustomer, // Use existing customer
                    SelectedCoffee = SelectedCoffee != null ? new List<CoffeeTypes> { SelectedCoffee } : new List<CoffeeTypes>(),
                    SelectedAddIns = SelectedAddIn != null ? new List<AddIns>(SelectedAddIn) : new List<AddIns>(),
                    TotalPrice = discountedPrice
                };

                // Add the new order to the list of existing orders
                existingOrders.Add(newOrder);

                SelectedCoffee = null;
                SelectedAddIn.Clear();

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


    }
}

