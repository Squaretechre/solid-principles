namespace SolidPrinciples.Reporting
{
    public class Product
    {
        public int Id { get; }
        public string Brand { get; }
        public decimal Price { get; }
        public int Units { get; }
        public Grade Grade { get; }
        public string Title { get; }

        public Product(int id, string brand, decimal price, int units, Grade grade, string title)
        {
            Id = id;
            Brand = brand;
            Price = price;
            Units = units;
            Grade = grade;
            Title = title;
        }
    }
}