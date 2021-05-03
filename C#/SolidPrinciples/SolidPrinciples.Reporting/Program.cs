using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SolidPrinciples.Reporting
{
    internal class Program
    {
        private static void Main()
        {
            var productRows = File.ReadAllLines(@"Data\products.tsv");
            var salesRows = File.ReadAllLines(@"Data\sales.csv");

            var products = new List<Product>();
            var sales = new List<Sale>();

            foreach (var productRow in productRows.Skip(1))
            {
                var productColumns = productRow.Split("\t");
                var id = int.Parse(productColumns[0]);
                var brand = productColumns[1];
                var price = decimal.Parse(productColumns[2]);
                var units = int.Parse(productColumns[3]);
                var grade = Enum.Parse<Grade>(productColumns[4]);
                var title = productColumns[5];

                products.Add(new Product(id, brand, price, units, grade, title));
            }

            foreach (var salesRow in salesRows.Skip(1))
            {
                var salesColumns = salesRow.Split(",");
                var date = DateTime.Parse(salesColumns[0]);
                var productId = int.Parse(salesColumns[1]);
                var quantity = int.Parse(salesColumns[2]);
                var customerId = int.Parse(salesColumns[3]);

                sales.Add(new Sale(date, productId, quantity, customerId));
            }

            var productsToIncludeInReport = products.Where(product => product.Grade.Equals(Grade.Ceremonial));

            var reportStartDate = new DateTime(2021, 01, 01);
            var reportEndDate = new DateTime(2021, 03, 31);

            var salesToIncludeInReport = sales
                .Where(sale => sale.Date >= reportStartDate && sale.Date <= reportEndDate)
                .ToList();

            var reportRows = new StringBuilder();

            foreach (var product in productsToIncludeInReport)
            {
                var totalSalesForProduct = salesToIncludeInReport
                    .Count(sale => sale.ProductId.Equals(product.Id));

                var revenueForProduct = totalSalesForProduct * product.Price;

                var reportRow = $@"<tr>
                                <td>{product.Id}</td>
                                <td>{product.Title}</td>
                                <td>{totalSalesForProduct}</td>
                                <td>£{revenueForProduct}</td>
                            </tr>";

                reportRows.AppendLine(reportRow);
            }

            var reportTemplate = File.ReadAllText(@"ReportTemplate\template.html");

            var report = reportTemplate
                .Replace("{startDate}", reportStartDate.ToString("dd/MM/yyyy"))
                .Replace("{endDate}", reportEndDate.ToString("dd/MM/yyyy"))
                .Replace("{rows}", reportRows.ToString());

            var reportDate = DateTime.Now.ToString("dd-MM-yyyy-ddTHHmmss");

            var fileName = $"sales-report-{reportDate}.html";

            File.WriteAllText(fileName, report);
        }
    }
}
