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
    public class PickerController
    {
        public Picker Pickers_GetName(int pickerid)
        {
            using (var context = new GroceryListContext())
            {
                Picker results = (from p in context.Pickers
                                  where p.PickerID == pickerid
                                  select new Picker
                                  {
                                      PickerID = pickerid,
                                      PickerName = p.FirstName + " " + p.LastName
                                  }).FirstOrDefault();
                return results;
            }
        }
    }
}
