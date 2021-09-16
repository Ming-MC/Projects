using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using GroceryListSystem.DAL;
using GroceryListSystem.ViewModels;
using System.ComponentModel;
#endregion

namespace GroceryListSystem.BLL
{
    [DataObject]
    public class CategoryController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SelectionList> Categories_List()
        {
            using (var context = new GroceryListContext())
            {
                IEnumerable<SelectionList> results = context.Categories
                                                     .Select(row => new SelectionList
                                                     {
                                                         ValueField = row.CategoryID,
                                                         DisplayField = row.Description
                                                     });
                return results.OrderBy(x => x.DisplayField).ToList();
            }
        }

    }
}
