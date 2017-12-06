using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cs460Final.Models;
using cs460Final.DAL;


namespace cs460Final.Models.ViewModel
{
    public class AuctionViewModel
    {
        public AuctionContext db = new AuctionContext();
        public IEnumerable<Bid> Bids { get; set; }
        public Bid Bid { get; set; }
        public IEnumerable<Item> ItemList { get; set; }
        public Item Item { get; set; }
    }
}