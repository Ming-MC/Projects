using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using GroceryListSystem.ViewModels;
using GroceryListSystem.DAL;
using System.ComponentModel;
using GroceryListSystem.Entities;
#endregion

namespace GroceryListSystem.BLL
{
    [DataObject]
    public class ProductController
    {
        #region This is for Query
        public List<ProductItem> Products_GetByCategory(int categoryid)
        {
            using (var context = new GroceryListContext())
            {
                IEnumerable<ProductItem> results = from x in context.Products
                                                   where x.CategoryID == categoryid
                                                   select new ProductItem
                                                   {
                                                       ProductID = x.ProductID,
                                                       Description = x.Description,
                                                       Price = x.Price,
                                                       Discount = x.Discount,
                                                       UnitSize = x.UnitSize,
                                                       CategoryID = categoryid,
                                                       Taxable = x.Taxable
                                                   };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<ProductItem> Products_List()
        {
            using (var context = new GroceryListContext())
            {
                IEnumerable<ProductItem> results = from x in context.Products
                                                   select new ProductItem
                                                   {
                                                       ProductID = x.ProductID,
                                                       Description = x.Description,
                                                       Price = x.Price,
                                                       Discount = x.Discount,
                                                       UnitSize = x.UnitSize,
                                                       CategoryID = x.CategoryID,
                                                       Taxable = x.Taxable
                                                   };
                return results.ToList();
            }
        }
        #endregion

        #region This is for CRUD (Add, Update and Delete)
        [DataObjectMethod(DataObjectMethodType.Insert,false)]
        public int Products_Add(ProductItem product)
        {
            using (var context = new GroceryListContext())
            {
                Product addProduct = new Product()
                {
                    Description = product.Description,
                    Price = product.Price,
                    Discount = product.Discount,
                    UnitSize = product.UnitSize,
                    CategoryID = product.CategoryID,
                    Taxable = product.Taxable
                };
                context.Products.Add(addProduct);

                context.SaveChanges();

                return addProduct.ProductID;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update,false)]
        public void Products_Update(ProductItem product)
        {
            using (var context = new GroceryListContext())
            {
                Product updateProduct = new Product()
                {
                    ProductID = product.ProductID,
                    Description = product.Description,
                    Price = product.Price,
                    Discount = product.Discount,
                    UnitSize = product.UnitSize,
                    CategoryID = product.CategoryID,
                    Taxable = product.Taxable
                };
                context.Entry(updateProduct).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            } 

        }

        [DataObjectMethod(DataObjectMethodType.Delete,false)]
        public void Products_Delete(ProductItem product)
        {
            Products_Delete(product.ProductID);
        }

        public void Products_Delete(int productid)
        {
            using (var context = new GroceryListContext())
            {
                var exists = context.Products.Find(productid);

                if (exists == null)
                {
                    throw new Exception($"No product by the id of ({productid}) exists on file.");
                }

                context.Products.Remove(exists);

                context.SaveChanges();
            }
        }
        #endregion
    }
}
