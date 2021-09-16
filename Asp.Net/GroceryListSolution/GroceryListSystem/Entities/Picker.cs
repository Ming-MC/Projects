namespace GroceryListSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Picker
    {
        public int PickerID { get; set; }

        [Required(ErrorMessage = "Picker last name is required.")]
        [StringLength(35, MinimumLength = 1, ErrorMessage = "Picker las name is limited to 35 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Picker first name is required.")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Picker first name is limited to 25 characters.")]
        public string FirstName { get; set; }

        public bool Active { get; set; }

        public int StoreID { get; set; }

        public virtual Store Store { get; set; }
    }
}
