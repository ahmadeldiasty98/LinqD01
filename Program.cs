using LinqDemo.Data;
using LinqDemo.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
namespace LinqDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            //Retrieve all categories from the production.categories table.
            var categories = dbContext.Categories.ToList();
            foreach (var category in categories)
            {
                Console.WriteLine($"ID: {category.CategoryId} , Name: {category.CategoryName}");
            }
            //Retrieve the first product from the production.products table.
            var product = dbContext.Products.FirstOrDefault();
            if (product != null)
            {
                Console.WriteLine($"ID: {product.ProductId} , Name: {product.ProductName} , Model_year: {product.ModelYear} , List_Price: {product.ListPrice} ");
            }
            else
            {
                Console.WriteLine("There is No Items In Table");
            }
            //Retrieve a specific product from the production.products table by ID.
            var product1 = dbContext.Products.Find(5);
            Console.WriteLine($"ID: {product1.ProductId} , Name: {product1.ProductName} , Model_year: {product1.ModelYear} , List_Price: {product1.ListPrice} ");
            //Retrieve all products from the production.products table with a certain model year.
            var product2 = dbContext.Products.ToList().Where(e => e.ModelYear == 2018);
            foreach (var item in product2)
            {
                Console.WriteLine($"Model_year:  {item.ModelYear} , ID: {item.ProductId} , Name: {item.ProductName} , List_Price: {item.ListPrice} ");
            }
            //Retrieve a specific customer from the sales.customers table by ID.
            var customer = dbContext.Customers.Find(15);
            Console.WriteLine($"ID: {customer.CustomerId} , Name: {customer.FirstName} {customer.LastName} , Email: {customer.Email} , state: {customer.State} ");
            //Retrieve a list of product names and their corresponding brand names.
            var product3 = dbContext.Products.Include(p => p.Brand).Select(p => new { p.ProductName, p.Brand.BrandName });
            foreach (var item in product3)
            {
                Console.WriteLine($"ProductName: {item.ProductName} , BrandNamme: {item.BrandName}");
            }
            //Count the number of products in a specific category.
            var product4 = dbContext.Products.Include(p => p.Category).Select(p => new { p.ProductName, p.ListPrice, Categoryname = p.Category.CategoryName }).Where(p => p.Categoryname == "Cyclocross Bicycles");
            Console.WriteLine(product4.Count());
            //Calculate the total list price of all products in a specific category.
            Console.WriteLine(product4.Sum(p => p.ListPrice));
            //var product5 = dbContext.Products.Include(p => p.Category).Select(p => new { p.ListPrice, Categoryname = p.Category.CategoryName }).Where(p => p.Categoryname == "Cyclocross Bicycles").Sum(p=>p.ListPrice);
            //Console.WriteLine(product5);
            /***********************************************************/
            //Calculate the average list price of products.
            Console.WriteLine(product4.Average(p => p.ListPrice));
            //Retrieve orders that are completed.
            var orders = dbContext.Orders.Select(o => new { o.OrderId, o.OrderDate, o.OrderStatus, o.OrderItems }).Where(o => o.OrderStatus == 2);
            foreach (var item in orders)
            {
                Console.WriteLine($"ID: {item.OrderId} , Date: {item.OrderDate} , Status: {item.OrderStatus} , Items: {item.OrderItems}");
            }



        }
    }
}
