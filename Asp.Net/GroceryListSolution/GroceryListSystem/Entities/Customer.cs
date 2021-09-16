namespace GroceryListSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Customer last name is required.")]
        [StringLength(35, MinimumLength = 1, ErrorMessage = "Last name is limited to 35 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Customer first name is required.")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = " First name is limited to 25 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Customer address is required.")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Address is limited to 30 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Customer city is required.")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "City is limited to 30 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Customer province is required.")]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "Province is limited to 2 characters.")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Customer phone is required.")]
        [StringLength(12, MinimumLength = 1, ErrorMessage = "Phone number is limited to 12 characters.")]
        public string Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
