using System;

namespace SolidPrinciples.Reporting
{
    public class Sale
    {
        public DateTime Date { get; }
        public int ProductId { get; }
        public int Quantity { get; }
        public int CustomerId { get; }

        public Sale(DateTime date, int productId, int quantity, int customerId)
        {
            Date = date;
            ProductId = productId;
            Quantity = quantity;
            CustomerId = customerId;
        }
    }
}