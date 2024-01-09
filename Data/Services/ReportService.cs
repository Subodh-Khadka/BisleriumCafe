using BisleriumCafe.Data.Models;
using BisleriumCafe.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronPdf;

namespace BisleriumCafe.Data.Services
{
    public class ReportService
    {
        private readonly string orderFilePath = BisleriumUtils.OrderFilePath();

        public byte[] GenerateDailySalesReport(DateTime date)
        {
            var orders = RetrieveOrderData();
            var dailyOrders = orders.Where(order => order.OrderTimestamp.Date == date.Date);

            return GenerateReport(dailyOrders);
        }

        public byte[] GenerateMonthlySalesReport(int year, int month)
        {
            var orders = RetrieveOrderData();
            var monthlyOrders = orders.Where(order => order.OrderTimestamp.Year == year && order.OrderTimestamp.Month == month);

            return GenerateReport(monthlyOrders);
        }

        private List<Order> RetrieveOrderData()
        {
            try
            {
                string existingData = File.ReadAllText(orderFilePath);

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

        private byte[] GenerateReport(IEnumerable<Order> orders)
        {                      
            var topCoffeeTypes = orders
                .SelectMany(order => order.SelectedCoffee)
                .GroupBy(coffee => coffee.CoffeeName)
                .OrderByDescending(group => group.Count())
                .Take(5)
                .Select(group => group.Key);

            var totalRevenue = orders.Sum(order => order.TotalPrice);

            
            var reportContent = $@"
                <h1>Sales Report</h1>
                <h2>Top 5 Coffee Types</h2>
                <ul>{string.Join("", topCoffeeTypes.Select(type => $"<li>{type}</li>"))}</ul>
                <h2>Total Revenue: {totalRevenue}</h2>
            ";
            
            //finally converting the contents of report to pdf
            var pdf = new ChromePdfRenderer();
            var pdfStream = pdf.RenderHtmlAsPdf(reportContent).Stream;

            return pdfStream.ToArray();
        }
    }
}
