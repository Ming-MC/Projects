namespace GroceryListSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Delivery
    {
        public int DeliveryID { get; set; }

        public int OrderID { get; set; }

        [Required(ErrorMessage = "Delivery name is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Delivery name is limited to 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Delivery phone is required.")]
        [StringLength(12, MinimumLength = 1, ErrorMessage = "Delivery phone is limited to 12 characters.")]
        public string Phone { get; set; }

        public DateTime ShippedDate { get; set; }
    }
}
