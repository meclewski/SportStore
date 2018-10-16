using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportStore.Components;
using SportStore.Models;
using Xunit;
using System.Text;

namespace SportStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            //Przygotowanie
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1, Name = "P1", Category="Jabłka"},
                new Product {ProductId = 2, Name = "P2", Category="Jabłka"},
                new Product {ProductId = 3, Name = "P3", Category="Śliwki"},
                new Product {ProductId = 4, Name = "P4", Category="Pomarańcze"},
            }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            //Działanie - pobieranie zbioru kategorii
            string[] result = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

            //Asercje
            Assert.True(Enumerable.SequenceEqual(new string[] { "Jabłka", "Pomarańcze", "Śliwki" }, result));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            //Przygotowanie
            string categoryToSelect = "Jabłka";
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1, Name = "P1", Category="Jabłka"},
                new Product {ProductId = 4, Name = "P4", Category="Pomarańcze"},
            }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext { RouteData = new RouteData() }
            };
            target.RouteData.Values["category"] = categoryToSelect;

            //Działanie - pobieranie zbioru kategorii
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            //Asercje
            Assert.Equal(categoryToSelect, result);
        }
    }
}
