using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FreeCode.Exceptions;

#region Additional Namespace
using GroceryListSystem.BLL;
using GroceryListSystem.ViewModels;
#endregion

namespace GroceryListWebApp.WebPages
{
    public partial class CustomerPickingOrderOLTP : System.Web.UI.Page
    {
        List<Exception> brokenRules = new List<Exception>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            List<PickedItem> orderedlist = new List<PickedItem>();
            PickedItem item = null;

            foreach (GridViewRow lineitem in OrderedItemList.Rows)
            {
                string orderlistid = (lineitem.FindControl("OrderListID") as Label).Text;
                string productid = (lineitem.FindControl("ProductID") as Label).Text;
                string product = (lineitem.FindControl("Product") as Label).Text;
                string qtyordered = (lineitem.FindControl("QtyOrdered") as Label).Text;
                string pickedqty = (lineitem.FindControl("QtyPicked") as TextBox).Text;
                string pickedissue = (lineitem.FindControl("PickIssue") as TextBox).Text;                      

                if (string.IsNullOrEmpty(pickedqty))
                {
                    brokenRules.Add(new BusinessRuleException<string>("Picked quantity is required.", "Product", product));
                }
                else
                {
                    if (double.Parse(pickedqty) == 0.0 )
                    {
                        brokenRules.Add(new BusinessRuleException<string>("Picked quantity is 0 and no concern entered.", "Product", product));
                    }
                    else
                    {
                        item = new PickedItem()
                        {
                            OrderListID = int.Parse(orderlistid),
                            ProductID = int.Parse(productid),
                            QtyPicked = double.Parse(pickedqty),
                            PickerComment = pickedissue
                        };
                        orderedlist.Add(item);
                    }
                }
            }

            MessageUserControl.TryRun(() =>
            {
                if (brokenRules.Count > 0)
                {
                    throw new BusinessRuleCollectionException("Order Picking concerns:", brokenRules);
                }
                else
                {
                    OrderListController sysmgr = new OrderListController();
                    sysmgr.Save_OrderListPiking(int.Parse(OrderId.Text), int.Parse(PickerId.Text), orderedlist);
                }
            }, "Order Picked", "The data of picked order has been saved.");
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            PickerName.Text = "Picker:";
            CustomerName.Text = "";
            OrderId.Text = "";
            PickerId.Text = "";
            icon.Visible = false;
            CustomerPhone.Text = "";
            SaveBtn.Visible = false;
            ClearBtn.Visible = false;
            OrderGridView.Visible = false;
        }

        protected void FetchBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(OrderId.Text))
            {
                brokenRules.Add(new BusinessRuleException<string>("Please enter order number.", "Order No", "Missing"));
            }

            if (string.IsNullOrEmpty(PickerId.Text))
            {
                brokenRules.Add(new BusinessRuleException<string>("Please enter picker ID.", "Picker ID", "Missing"));
            }

            MessageUserControl.TryRun(() =>
            {
                if (brokenRules.Count > 0)
                {
                    throw new BusinessRuleCollectionException("Order Picking Concerns:", brokenRules);
                }
                else
                {
                    SaveBtn.Visible = true;
                    ClearBtn.Visible = true;
                    OrderGridView.Visible = true;
                    icon.Visible = true;

                    PickerController pickerSysmgr = new PickerController();
                    Picker pickerInfo = pickerSysmgr.Pickers_GetName(int.Parse(PickerId.Text));
                    PickerName.Text = "Picker: " + pickerInfo.PickerName;

                    CustomerController customerSysmgr = new CustomerController();
                    CustomerPicking customerInfo = customerSysmgr.Customers_GetInfo(int.Parse(OrderId.Text));
                    CustomerName.Text = "Customer: " + " " + customerInfo.CustomerName;
                    CustomerPhone.Text = customerInfo.CustomerPhone;

                    OrderListController orderListItemSysmgr = new OrderListController();
                    List<PickListItem> orderedProductItem = orderListItemSysmgr.OrderList_GetOrderListForOrder(int.Parse(OrderId.Text));
                    OrderedItemList.DataSource = orderedProductItem;
                    OrderedItemList.DataBind();
                }
            });

        }
    }
}