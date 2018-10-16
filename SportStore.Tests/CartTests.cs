using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SportStore.Models;
using Xunit;

namespace SportStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            //Przygotowanie - utworzenie produktów testowych
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };

            //Przygotowanie - utworzenie nowego koszyka
            Cart target = new Cart();

            //Działanie
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] result = target.Lines.ToArray();

            //Assercje
            Assert.Equal(2, result.Length);
            Assert.Equal(p1, result[0].Product);
            Assert.Equal(p2, result[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //Przygotowanie - utworzenie produktów testowych
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };

            //Przygotowanie - utworzenie nowego koszyka
            Cart target = new Cart();

            //Działanie
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] result = target.Lines.OrderBy(c => c.Product.ProductId).ToArray();

            //Assercje
            Assert.Equal(2, result.Length);
            Assert.Equal(11, result[0].Quantity);
            Assert.Equal(1, result[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            //Przygotowanie - utworzenie produktów testowych
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };
            Product p3 = new Product { ProductId = 3, Name = "P3" };

            //Przygotowanie - utworzenie nowego koszyka
            Cart target = new Cart();

            //Przygotowanie - dodanie kilku produktów do koszyka
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            //Działanie
            target.RemoveLine(p2);

            //Assercje
            Assert.Empty(target.Lines.Where(c => c.Product == p2));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            //Przygotowanie - utworzenie produktów testowych
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 50M };

            //Przygotowanie - utworzenie nowego koszyka
            Cart target = new Cart();

            //Działanie 
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();

            //Assercje
            Assert.Equal(450M, result);
        }

        [Fact]
        public void Can_Clean_Contents()
        {
            //Przygotowanie - utworzenie produktów testowych
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 50M };

            //Przygotowanie - utworzenie nowego koszyka
            Cart target = new Cart();

            //Przygotowanie - dodanie produktów do koszyka
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            //Działanie
            target.Clear();

            //Assercje
            Assert.Empty(target.Lines);
        }

    }
}

