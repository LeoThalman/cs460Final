using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cs460Final.DAL;
using cs460Final.Models;
using cs460Final.Models.ViewModel;
using System.Data.Entity;
using Newtonsoft.Json;

namespace cs460Final.Controllers
{
    public class AuctionController : Controller
    {
        public AuctionViewModel vm = new AuctionViewModel();

        private void LoadVM()
        {
            vm.db = new AuctionContext();
            vm.ItemList = vm.db.Items.ToList();
        }

        // GET: Auction
        public ActionResult Index()
        {
            LoadVM();
            ViewBag.Title = "Reginald's Ancient Antiquities Auction House";
            return View(vm);
        }

        [HttpGet]
        public ActionResult ListItems()
        {
            LoadVM();
            return View(vm.ItemList);
        }

        [HttpGet]
        public ActionResult AddItem()
        {
            LoadVM();
            vm.Item = new Item();
            return View(vm.Item);
        }

        [HttpPost]
        public ActionResult AddItem([Bind(Include = "Name,Description,Seller")] Item Item)
        {
            LoadVM();
            vm.Item = new Item();
            if (ModelState.IsValid)
            {
                vm.db.Items.Add(Item);
                vm.db.SaveChanges();
                return RedirectToAction("ListItems");
            }
            return View(vm.Item);
        }

        public ActionResult ItemDetails(int id)
        {
            LoadVM();
            vm.Item = vm.db.Items.Where(i => i.ID == id).FirstOrDefault();
            return View(vm.Item);
        }

        [HttpGet]
        public ActionResult EditItem(int id)
        {
            LoadVM();
            vm.Item = vm.db.Items.Where(i => i.ID == id).FirstOrDefault();
            ViewBag.Title = "Edit " + vm.Item.Name;
            return View(vm.Item);
        }

        [HttpPost]
        public ActionResult EditItem([Bind(Include = "ID,Name,Description,Seller,Seller1")] Item Item)
        {
            LoadVM();
            if (ModelState.IsValid)
            {
                vm.db.Entry(Item).State = EntityState.Modified;
                vm.db.SaveChanges();
                return RedirectToAction("ListItems");
            }
            return View(vm.db.Items.Where(i => i.ID == Item.ID).FirstOrDefault());
        }

        [HttpGet]
        public ActionResult DeleteItem(int id)
        {
            LoadVM();
            vm.Item = vm.db.Items.Where(i => i.ID == id).FirstOrDefault();
            return View(vm.Item);
        }

        [HttpPost]
        public ActionResult DeleteItem([Bind(Include = "Name,Description,Seller")] Item Item)
        {
            vm.Item = vm.db.Items.Where(i => i.Name == Item.Name).FirstOrDefault();
            if(ModelState.IsValid)
            {
                vm.db.Items.Remove(vm.Item);
                vm.db.SaveChanges();
                return RedirectToAction("ListItems");
            }
            return RedirectToAction("ListItems");
        }

        [HttpGet]
        public ActionResult CreateBid()
        {
            LoadVM();
            vm.Bid = new Bid();
            return View(vm.Bid);
        }

        [HttpPost]
        public ActionResult CreateBid([Bind(Include = "ItemID,Buyer,Price")] Bid Bid)
        {
            Bid.TimeStamp = DateTime.Now;
            if (ModelState.IsValid)
            {
                vm.Bid = Bid;
                vm.Bid.TimeStamp = DateTime.Now;
                vm.db.Bids.Add(vm.Bid);
                vm.db.SaveChanges();
                return RedirectToAction("ListItems");

            }
            return RedirectToAction("CreateBid");
        }

        public JsonResult GetBids(int id)
        {
            LoadVM();
            vm.Bids = vm.db.Bids.Where(b => b.ItemID == id).OrderBy(i => i.Price).Reverse().ToList();
            List<Bid> tBids = new List<Bid>();
            Bid temp;
            foreach(Bid bid in vm.Bids)
            {
                temp = new Bid
                {
                    Buyer = bid.Buyer,
                    Price = bid.Price
                };
                tBids.Add(temp);
            }

            string rjson = JsonConvert.SerializeObject(tBids, Formatting.Indented);
            return Json(rjson, JsonRequestBehavior.AllowGet);
        }
    }
}