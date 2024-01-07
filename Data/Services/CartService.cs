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
                    CustomerId = CurrentCustomer.Id,
                    SelectedCoffee = SelectedCoffee != null ? new List<CoffeeTypes> { SelectedCoffee } : new List<CoffeeTypes>(),
                    SelcectedAddIns = SelectedAddIn.ToList(), // Copy the list to ensure it's not modified externally
                    TotalPrice = CalculateTotalPrice() // Adjust based on your logic for calculating the total order price
                };

                existingOrders.Add(newOrder);
                File.WriteAllText(orderFilePath, JsonConvert.SerializeObject(existingOrders, Formatting.Indented));

                /*OrderSubmissionSuccessful = true; //Setting the value true for successful Order*/
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"An error occurred while submitting the order: {ex.Message}");               
                /*OrderSubmissionSuccessful = false;*/
            }
        }              
    }
}

