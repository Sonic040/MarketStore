using MarketStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var users = _context.User1s.Where(x=>x.Roleid==1).Count();
            ViewBag.users = users;
            var stores = _context.Store1s.Count();
            ViewBag.stores = stores;
            var cats = _context.Category1s.Count();
            ViewBag.cats = cats;
            var products = _context.Products.Count();
            ViewBag.products = products;
            var users2 = _context.User1s.Where(x=>x.Roleid==1).ToList();
            return View(users2);
        }
    }
}
