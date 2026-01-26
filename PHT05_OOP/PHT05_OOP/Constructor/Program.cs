using System;

namespace Constructor
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }

        // Constructor
        public Product(string productId, string productName, double price)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
        }

        public void Display()
        {
            Console.WriteLine($"ID: {ProductId} | Name: {ProductName} | Price: {Price}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Product p1 = new Product("P01", "Laptop", 1500);
            Product p2 = new Product("P02", "Mouse", 25);

            p1.Display();
            p2.Display();
        }
    }
}
