using System;
using System.Collections.Generic;

public class Address
{
    private string streetAddress;
    private string city;
    private string state;
    private string country;

    public Address(string streetAddress, string city, string state, string country)
    {
        this.streetAddress = streetAddress;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public string StreetAddress
    {
        get { return streetAddress; }
        set { streetAddress = value; }
    }

    public string City
    {
        get { return city; }
        set { city = value; }
    }

    public string State
    {
        get { return state; }
        set { state = value; }
    }

    public string Country
    {
        get { return country; }
        set { country = value; }
    }

    public bool IsInUSA()
    {
        return country.Equals("USA", StringComparison.OrdinalIgnoreCase) || country.Equals("United States", StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return $"{streetAddress}\n{city}, {state}\n{country}";
    }
}

public class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public Address Address
    {
        get { return address; }
        set { address = value; }
    }

    public bool LivesInUSA()
    {
        return address.IsInUSA();
    }
}

public class Product
{
    private string name;
    private string productId;
    private double price;
    private int quantity;

    public Product(string name, string productId, double price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string ProductId
    {
        get { return productId; }
        set { productId = value; }
    }

    public double Price
    {
        get { return price; }
        set { price = value; }
    }

    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    public double GetTotalCost()
    {
        return price * quantity;
    }
}

public class Order
{
    private List<Product> products;
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
        this.products = new List<Product>();
    }

    public List<Product> Products
    {
        get { return products; }
    }

    public Customer Customer
    {
        get { return customer; }
        set { customer = value; }
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public double GetTotalPrice()
    {
        double total = 0.0;
        foreach (Product product in products)
        {
            total += product.GetTotalCost();
        }
        double shippingCost = customer.LivesInUSA() ? 5.0 : 35.0;
        return total + shippingCost;
    }

    public string GetPackingLabel()
    {
        string label = "";
        foreach (Product product in products)
        {
            label += $"{product.Name} ({product.ProductId})\n";
        }
        return label.TrimEnd('\n');
    }

    public string GetShippingLabel()
    {
        return $"{customer.Name}\n{customer.Address}";
    }
}

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