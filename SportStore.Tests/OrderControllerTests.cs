using Microsoft.AspNetCore.Mvc;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using Xunit;

namespace SportStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Przygotowanie - utworzenie imitacji repo
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            //Przygotowanie utworzenie pustego koszyka
            Cart cart = new Cart();
            //Przygotowanie - utworzenie zamówiena
            Order order = new Order();
            //Przygotowanie - utworzenie egzemplarza kontrolera
            OrderController target = new OrderController(mock.Object, cart);

            //Działanie 
            ViewResult result = target.Checkout(order) as ViewResult;

            //Asercje - sprawdzanie czy zamówienie zostało umieszczone w repo
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            //Asercje - sprawdzanie czy metoda zwraca domyślny widok
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            //Asercje - sprawdzenie czy przekazujemy prawidłowy model do widoku
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            //Przygotowanie - utworzenie imitacji repo
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            //Przygotowanie utworzenie koszyka z produktem
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            //Przygotowanie - utworzenie egzemplarza kontrolera
            OrderController target = new OrderController(mock.Object, cart);
            //Przygotowanie - dodanie błędu do modelu
            target.ModelState.AddModelError("error", "error");

            //Działanie 
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            //Asercje - sprawdzanie czy zamówienie nie zostało przekazane do repo
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            //Asercje - sprawdzanie czy metoda zwraca domyślny widok
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            //Asercje - sprawdzenie czy przekazujemy nieprawidłowy model do widoku
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            //Przygotowanie - utworzenie imitacji repo
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            //Przygotowanie utworzenie koszyka z produktem
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            //Przygotowanie - utworzenie egzemplarza kontrolera
            OrderController target = new OrderController(mock.Object, cart);
            
            //Działanie 
            RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;

            //Asercje - sprawdzanie czy zamówienie zostało przkazane do repo
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            //Asercje - sprawdzenie czy metoda przekierowuje do metody akcji Completed()
            Assert.Equal("Completed", result.ActionName);
        }
    }
}
