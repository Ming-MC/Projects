namespace GroceryListSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderLists = new HashSet<OrderList>();
        }

        public int ProductID { get; set; }

        [Required(ErrorMessage = "Product description is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Product description is limited to 100 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Product price is required.")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product discount is required.")]
        [Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Product unit size is required.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Product unit size is limited to 20 characters.")]
        public string UnitSize { get; set; }

        public int CategoryID { get; set; }

        public bool Taxable { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderList> OrderLists { get; set; }
    }
}
