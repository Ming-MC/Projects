using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryListSystem.ViewModels
{
    public class PickListItem
    {
        public int OrderID { get; set; }
        public int OrderListID { get; set; }
        public int ProductID { get; set; }
        public string Product { get; set; }
        public double QtyOrdered { get; set; }
        public string CustomerComment { get; set; }

        public List<PickedItem> PickedProducts { get; set; }

    }
}
