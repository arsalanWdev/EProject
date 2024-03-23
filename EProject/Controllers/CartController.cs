using EProject.Data;
using EProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EProject.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cart _cart;

        public CartController(Cart cart,ApplicationDbContext context)
        {
            _cart = cart;
            _context = context;
        }   

        public IActionResult Index()
        {
            var items = _cart.GetAllCartItems();
            _cart.CartItems = items;
            return View(_cart);
        }

        public IActionResult AddToCart(int id)
        {
            var  selectedBook =  GetBookById(id);

            if(selectedBook != null)
            {
                _cart.AddToCart(selectedBook, 1);
            }
            return RedirectToAction("Index","Store");
        }




        public IActionResult RemoveFromCart(int id)
        {
            var selectedBook = GetBookById(id);
            if(selectedBook != null)
            {
                _cart.RemoveFromCart(selectedBook);
            }
            return RedirectToAction("Index");
        }


        public IActionResult ReduceQuantity(int id)
        {
            var selectedBook = GetBookById(id);
            if (selectedBook != null)
            {
                _cart.ReduceQuantity(selectedBook);
            }
            return RedirectToAction("Index");
        }


        public IActionResult IncreaseQuantity(int id)
        {
            var selectedBook = GetBookById(id);
            if (selectedBook != null)
            {
                _cart.IncreaseQuantity(selectedBook);
            }
            return RedirectToAction("Index");
        }



        public IActionResult ClearCart()
        {
            _cart.ClearCart();

            return RedirectToAction("Index");
        }


        public Books GetBookById(int id)
        {
            return _context.Bookss.FirstOrDefault(byid => byid.Id == id);
        }
    }
}
