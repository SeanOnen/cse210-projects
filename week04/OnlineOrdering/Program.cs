using System;

class Program
{
    static void Main(string[] args)
    {
        // Create addresses
        Address usaAddress = new Address("123 Main St", "New York", "NY", "USA");
        Address internationalAddress = new Address("456 Elm St", "Toronto", "ON", "Canada");

        // Create customers
        Customer customer1 = new Customer("John Doe", usaAddress);
        Customer customer2 = new Customer("Jane Smith", internationalAddress);

        // Create order 1 (USA customer)
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "LAP001", 999.99, 1));
        order1.AddProduct(new Product("Mouse", "MOU001", 29.99, 2));
        order1.AddProduct(new Product("Keyboard", "KEY001", 79.99, 1));

        // Create order 2 (International customer)
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Tablet", "TAB001", 499.99, 1));
        order2.AddProduct(new Product("Case", "CAS001", 19.99, 1));

        // Display Order 1
        Console.WriteLine("Order 1:");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine("\nShipping Label:");
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"\nTotal Price: ${order1.GetTotalPrice():F2}");
        Console.WriteLine();

        // Display Order 2
        Console.WriteLine("Order 2:");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine("\nShipping Label:");
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"\nTotal Price: ${order2.GetTotalPrice():F2}");
    }
}