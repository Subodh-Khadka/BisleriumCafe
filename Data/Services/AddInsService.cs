using BisleriumCafe.Data.Models;
using BisleriumCafe.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BisleriumCafe.Data.Services
{
    public class AddInsService
    {
        private static readonly string filePath = BisleriumUtils.AddInsFilePath();

        public static List<AddIns> LoadAddInsData()
        {
            try
            {
                string existingData = File.ReadAllText(filePath);

                if (string.IsNullOrEmpty(existingData))
                {
                    // If the file is empty or doesn't exist, inject sample data
                    InjectSampleAddInsData();
                }

                return JsonConvert.DeserializeObject<List<AddIns>>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return new List<AddIns>();
            }
        }

        public static void SaveAddInsData(List<AddIns> addIns)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(addIns, Formatting.Indented);
                File.WriteAllText(filePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing JSON file: {ex.Message}");
            }
        }

        public static void InjectSampleAddInsData()
        {
            string filePath = BisleriumUtils.AddInsFilePath();

            var existingData = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(existingData))
            {
                List<AddIns> addIns = new List<AddIns>
        {
            new AddIns() { AddInID = Guid.NewGuid(), AddInName = "Honey", AddInPrice = 50 },
            new AddIns() { AddInID = Guid.NewGuid(), AddInName = "Cinnamon", AddInPrice = 30 },
            new AddIns() { AddInID = Guid.NewGuid(), AddInName = "Chocolate", AddInPrice = 40 },
            new AddIns() { AddInID = Guid.NewGuid(), AddInName = "Vanilla", AddInPrice = 35 },
            new AddIns() { AddInID = Guid.NewGuid(), AddInName = "Hazelnut", AddInPrice = 45 },
        };
                SaveAddInsData(addIns);  // Corrected method call
            }
        }

        public static List<AddIns> RetrieveAddInsData()
        {
            string filePath = BisleriumUtils.AddInsFilePath();
            try
            {
                string existingData = File.ReadAllText(filePath);

                if (string.IsNullOrEmpty(existingData))
                {
                    return new List<AddIns>();
                }
                return JsonConvert.DeserializeObject<List<AddIns>>(existingData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return new List<AddIns>();
            }
        }
    }
}

