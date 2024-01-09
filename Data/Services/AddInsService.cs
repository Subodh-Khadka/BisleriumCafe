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
            new AddIns() { AddInID = Guid.NewGuid(), AddInName = "Honey", AddInPrice = 50,AddInImageUrl="/AddInsImages/fresh-honeycombs.jpg" },
            new AddIns() { AddInID = Guid.NewGuid(), AddInName = "Cinnamon", AddInPrice = 30,AddInImageUrl = "/AddInsImages/cinnamon-table.jpg" },
            new AddIns() { AddInID = Guid.NewGuid(), AddInName = "Chocolate", AddInPrice = 40,AddInImageUrl = "/AddInsImages/chocolate-bonbons-cocoa-powder-isolated-white-background.jpg" },
            new AddIns() { AddInID = Guid.NewGuid(), AddInName = "Choco", AddInPrice = 40,AddInImageUrl = "/AddInsImages/cinnamon-table.jpg" },
            new AddIns() { AddInID = Guid.NewGuid(), AddInName = "Vanilla", AddInPrice = 35,AddInImageUrl = "/AddInsImages/delicious-banana-milkshake.jpg"  },
            new AddIns() { AddInID = Guid.NewGuid(), AddInName = "Hazelnut", AddInPrice = 45,AddInImageUrl = "/AddInsImages/cashews-isolated-white-background.jpg" },
        };
                SaveAddInsData(addIns);  
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

        
        public static List<AddIns> EditAddIns(AddIns modifiedAddIns)
        {

            List<AddIns> addIns = RetrieveAddInsData();
            
            AddIns addInsToEdit = addIns.FirstOrDefault(a => a.AddInID == modifiedAddIns.AddInID);

            if (addInsToEdit != null)
            {                
                addInsToEdit.AddInName = modifiedAddIns.AddInName;
                addInsToEdit.AddInPrice = modifiedAddIns.AddInPrice;               

                SaveAddInsData(addIns);
                return addIns;
            }
            else
            {
                throw new Exception("AddIn Unavailable");
            }
        }


    }
}

