using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Addtional Namespaces
using System.ComponentModel;
using GroceryListSystem.ViewModels;
using GroceryListSystem.DAL;
using GroceryListSystem.Entities;
using FreeCode.Exceptions;
#endregion

namespace GroceryListSystem.BLL
{
    [DataObject]
    public class OrderListController
    {
        List<Exception> brokenRules = new List<Exception>();

        public List<PickListItem> OrderList_GetOrderListForOrder(int orderid)
        {
            using (var context = new GroceryListContext())
            {
                Order exists = context.Orders
                                .Where(x => x.OrderID == orderid
                                        && !x.PickedDate.HasValue)
                                .Select(x => x)
                                .FirstOrDefault();

                if (exists == null)
                {
                    throw new Exception("The order has been picked.");
                }

                IEnumerable<PickListItem> results = from ol in context.OrderLists
                                                    where ol.OrderID == orderid
                                                    select new PickListItem
                                                    {
                                                        OrderID = orderid,
                                                        OrderListID = ol.OrderListID,
                                                        ProductID = ol.ProductID,
                                                        Product = ol.Product.Description,
                                                        QtyOrdered = ol.QtyOrdered,
                                                        CustomerComment = ol.CustomerComment
                                                    };
                return results.ToList();
            }
        }

        public void Save_OrderListPiking(int orderid, int pickerid, List<PickedItem> pickedorderlist)
        {
            using (var context = new GroceryListContext())
            {
                Order order = context.Orders
                               .Where(x => x.OrderID == orderid)
                               .FirstOrDefault();

                Product product = null;
                OrderList orderList = null;
                decimal subtotal = 0;
                decimal gst = 0.0m;

                foreach (PickedItem item in pickedorderlist)
                {
                    product = context.Products
                                .Where(x => x.ProductID == item.ProductID)
                                .FirstOrDefault();

                    orderList = context.OrderLists
                                  .Where(x => x.OrderListID == item.OrderListID)
                                  .FirstOrDefault();

                    if (item.QtyPicked < 0)
                    {
                        brokenRules.Add(new BusinessRuleException<string>("Picked quantity is required more than 0.", "Product", product.Description));
                    }
                    else
                    {
                        orderList.QtyPicked = item.QtyPicked;
                        orderList.PickIssue = item.PickerComment;
                        orderList.Price = product.Price;
                        orderList.Discount = product.Discount;

                        context.Entry(orderList).Property(nameof(OrderList.QtyPicked)).IsModified = true;
                        context.Entry(orderList).Property(nameof(OrderList.PickIssue)).IsModified = true;
                        context.Entry(orderList).Property(nameof(OrderList.Price)).IsModified = true;
                        context.Entry(orderList).Property(nameof(OrderList.Discount)).IsModified = true;

                        subtotal += (orderList.Price - orderList.Discount) * (decimal)(orderList.QtyPicked ?? 0);

                        if (product.Taxable)
                        {
                            gst += 0.05m * ((orderList.Price - orderList.Discount) * (decimal)(orderList.QtyPicked ?? 0));
                        }
                    }
                }

                if (order.PickedDate.HasValue)
                {
                    brokenRules.Add(new BusinessRuleException<string>("The order has been picked", "Order Picked", string.Format("{0:MM dd, yyyy}", order.PickedDate)));
                }
                else
                {
                    order.PickerID = pickerid;
                    order.PickedDate = DateTime.Today;
                    order.SubTotal = subtotal;
                    order.GST = gst;

                    context.Entry(order).Property(nameof(Order.PickerID)).IsModified = true;
                    context.Entry(order).Property(nameof(Order.PickedDate)).IsModified = true;
                    context.Entry(order).Property(nameof(Order.SubTotal)).IsModified = true;
                    context.Entry(order).Property(nameof(Order.GST)).IsModified = true;
                }

                if (brokenRules.Count() > 0)
                {
                    throw new BusinessRuleCollectionException("Add concerns: ", brokenRules);
                }
                else
                {
                    context.SaveChanges();
                }
            }
        }
    }
}
