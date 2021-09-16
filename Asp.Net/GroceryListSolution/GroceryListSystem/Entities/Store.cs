namespace GroceryListSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Store()
        {
            Orders = new HashSet<Order>();
            Pickers = new HashSet<Picker>();
        }

        public int StoreID { get; set; }

        [Required(ErrorMessage = "Store location is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Store location is limited to 50 character.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Store address is required.")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Store address is limited to 30 character.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Store city is required.")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Store address is limited to 30 character.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Store province is required.")]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "Store address is limited to 2 character.")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Store phone is required.")]
        [StringLength(12, MinimumLength = 1, ErrorMessage = "Store address is limited to 12 character.")]
        public string Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Picker> Pickers { get; set; }
    }
}
