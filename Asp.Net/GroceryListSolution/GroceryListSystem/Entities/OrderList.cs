namespace GroceryListSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderList")]
    internal partial class OrderList
    {
        public int OrderListID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public double QtyOrdered { get; set; }

        public double? QtyPicked { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Order list customer comment is limited to 100 character.")]
        public string CustomerComment { get; set; }

        [StringLength(100, MinimumLength = 0, ErrorMessage = "Order list pick issue is limited to 100 character.")]
        public string PickIssue { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
