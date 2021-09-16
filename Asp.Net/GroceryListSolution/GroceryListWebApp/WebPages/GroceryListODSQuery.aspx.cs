using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using GroceryListSystem.BLL;
using GroceryListSystem.ViewModels;
#endregion

namespace GroceryListWebApp.WebPages
{
    public partial class GroceryListODSQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SelectCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void FetchProducts_Click(object sender, EventArgs e)
        {
            if (CategoryList.SelectedIndex == 0)
            {
                MessageUserControl.ShowInfo("Category Selection", "No Category has been selected.");
            }
            else
            {
                RefreshList();
            }
        }

        protected void ProductsofCategoryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProductsofCategoryList.PageIndex = e.NewPageIndex;
            RefreshList();
        }

        protected void RefreshList()
        {
            MessageUserControl.TryRun(() =>
            {
                ProductController sysmgr = new ProductController();
                
                List<ProductItem> info = sysmgr.Products_GetByCategory(int.Parse(CategoryList.SelectedValue));

                ProductsofCategoryList.DataSource = info;

                ProductsofCategoryList.DataBind();
            }, "Category Products List", "View Category Products");
        }
    }
}