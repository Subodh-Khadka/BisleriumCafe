using BisleriumCafe.Data.Models;
using BisleriumCafe.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BisleriumCafe.Data.Services
{
    public class OrderService
    {
        private readonly string filePath = BisleriumUtils.OrderFilePath();

        public List<Order> RetrieveOrderData()
        {
            try
            {
                string existingData = File.ReadAllText(filePath);

                if (string.IsNullOrEmpty(existingData))
                {
                    return new List<Order>();
                }

                return JsonConvert.DeserializeObject<List<Order>>(existingData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return new List<Order>();
            }
        }
    }
}
