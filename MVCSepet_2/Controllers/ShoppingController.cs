using MVCSepet_2.CustomTools;
using MVCSepet_2.DesignPatterns.SingletonPattern;
using MVCSepet_2.Models;
using MVCSepet_2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSepet_2.Controllers
{
    public class ShoppingController : Controller
    {
        NorthwindEntities _db;

        public ShoppingController()
        {
            _db = DBTool.DBInstance ;
        }



        // GET: Shopping
        public ActionResult ProductList()
        {
            ShoppingVM svm = new ShoppingVM
            {
                Products = _db.Products.ToList()
            };
            return View(svm);
        }

        public ActionResult AddToCart(int id)
        {
            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;

            Product eklenecekUrun = _db.Products.Find(id);

            CartItem ci = new CartItem();
            ci.ProductName = eklenecekUrun.ProductName;
            ci.ID = eklenecekUrun.ProductID;
            ci.UnitPrice = eklenecekUrun.UnitPrice;

            c.SepeteEkle(ci);

            Session["scart"] = c;

            TempData["mesaj"] = $"{ci.ProductName} isimli Urun sepete eklenmiştir";
            return RedirectToAction("ProductList");



        }

        public ActionResult SepetSayfasi()
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;
                return View(c);
            }
            ViewBag.SepetBos = "Sepetinizde ürün bulunmamaktadır";
            return View();
        }
    }
}