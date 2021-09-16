using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespace
using System.ComponentModel;
using GroceryListSystem.ViewModels;
using GroceryListSystem.DAL;
#endregion

namespace GroceryListSystem.BLL
{
    [DataObject]
    public class CustomerController
    {
        public CustomerPicking Customers_GetInfo(int orderid)
        {
            using (var context = new GroceryListContext())
            {
                CustomerPicking results = (from o in context.Orders
                                           where o.OrderID.Equals(orderid)
                                           select new CustomerPicking
                                           {
                                               CustomerID = o.CustomerID,
                                               CustomerName = o.Customer.FirstName + " " + o.Customer.LastName,
                                               CustomerPhone = o.Customer.Phone
                                           }).FirstOrDefault();
                return results;
            }
        }
    }
}
