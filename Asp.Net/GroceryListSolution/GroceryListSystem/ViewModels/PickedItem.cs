using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryListSystem.ViewModels
{
    public class PickedItem
    {
        public int OrderListID { get; set; }
        public int ProductID { get; set; }
        public double QtyPicked { get; set; }
        public string PickerComment { get; set; }
    }
}
