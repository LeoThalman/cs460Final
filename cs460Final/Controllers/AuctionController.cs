using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cs460Final.DAL;
using cs460Final.Models;
using cs460Final.Models.ViewModel;

namespace cs460Final.Controllers
{
    public class AuctionController : Controller
    {
        public AuctionViewModel vm = new AuctionViewModel();

        private void LoadVM()
        {
            vm.db = new AuctionContext();
        }

        // GET: Auction
        public ActionResult Index()
        {
            ViewBag.Title = "Reginald's Ancient Antiquities Auction House";
            return View();
        }
    }
}