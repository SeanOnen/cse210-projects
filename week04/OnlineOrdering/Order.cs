using System;
using System.Collections.Generic;

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