using BisleriumCafe.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumCafe.Utils
{
    public class BisleriumUtils
    {
        public static string ApplicationDirectoryPath()
        {
            string directoryPath = @"D:\final_year\Application Development\JSON";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                return directoryPath;
            }
            else
            {
                return directoryPath;
            }
        }

        public static string ApplicationFilePath()
        {
            string directoryPathCreated = ApplicationDirectoryPath();
            string filePath = Path.Combine(directoryPathCreated, "Data.json");
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                    return filePath;
                }
                else
                {
                    return filePath;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return message;
            }
        }

         public static string AddInsFilePath()
         {
             string directoryPathCreated = ApplicationDirectoryPath();
             string filePath = Path.Combine(directoryPathCreated, "AddIns.json");
             try
             {
                 if (!File.Exists(filePath))
                 {
                     File.Create(filePath).Close();
                     return filePath;
                 }
                 else
                 {
                     return filePath;
                 }
             }
             catch (Exception ex)
             {
                 string ExceptionMessage = ex.Message;
                 return ExceptionMessage;
             }
         }

        public static string CustomerFilePath()
        {
            string directoryPathCreated = ApplicationDirectoryPath();
            string filePath = Path.Combine(directoryPathCreated, "Customer.json");
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                    return filePath;
                }
                else
                {
                    return filePath;
                }
            }
            catch (Exception ex)
            {
                string ExceptionMessage = ex.Message;
                return ExceptionMessage;
            }
        }

        public static string OrderFilePath()
        {
            string directoryPathCreated = ApplicationDirectoryPath();
            string filePath = Path.Combine(directoryPathCreated, "Order.json");
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                    return filePath;
                }
                else
                {
                    return filePath;
                }
            }
            catch (Exception ex)
            {
                string ExceptionMessage = ex.Message;
                return ExceptionMessage;
            }
        }

        /*public static List<AddIns> ReadAddInsFromFile()
        {
            string filePath = AddInsFilePath();

            try
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<AddIns>>(json) ?? new List<AddIns>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading AddIns file: {ex.Message}");
                return new List<AddIns>();
            }

        }
        public static void WriteAddInsToFile(List<AddIns> addInsList)
        {
            string filePath = AddInsFilePath();

            try
            {
                string json = JsonConvert.SerializeObject(addInsList);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error writing AddIns file: {ex.Message}");
            }
        }*/

    }
}
