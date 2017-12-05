namespace cs460Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bid")]
    public partial class Bid
    {
        public int ID { get; set; }

        public int ItemID { get; set; }

        [Required]
        [StringLength(50)]
        public string Buyer { get; set; }

        public double Price { get; set; }

        public DateTime TimeStamp { get; set; }

        public virtual Item Item { get; set; }
    }
}
