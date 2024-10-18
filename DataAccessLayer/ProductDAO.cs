using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessLayer
{
    public class ProductDAO : SingletonBase<ProductDAO>
    {
        List<Product> list;
        public ProductDAO()
        {
            list = new List<Product>{
                new Product { ProductId = 1, ProductName = "Chai", UnitsInStock = 39, UnitPrice = 18, CategoryId = 1 },
                new Product { ProductId = 2, ProductName = "Chang", UnitsInStock = 17, UnitPrice = 19, CategoryId = 1 },
                new Product { ProductId = 3, ProductName = "Aniseed Syrup", UnitsInStock = 13, UnitPrice = 10, CategoryId = 2 }
            };
        }
        public List<Product> GetProducts() {
            //list.AddRange( new List<Product> {
                
            //});
            return list; 
        }

        public int GetMaxProductId()
        {
            if (list.Count > 0)
            {
                return list.Max(p=>p.ProductId) + 1;
            }
            else
            {
                return 0;
            }
        }

        public void SaveProduct(Product p)
        {
            list.Add(p);
        }

        public void UpdateProduct(Product p)
        {
            foreach (var item in list.ToList())
            {
                if (item.ProductId == p.ProductId)
                {
                    item.ProductName = p.ProductName;
                    item.UnitsInStock = p.UnitsInStock;
                    item.UnitPrice = p.UnitPrice;
                    item.CategoryId = p.CategoryId;
                }
            }
        }
        public void DeleteProduct(Product p)
        {
            foreach (var item in list.ToList())
            {
                if (item.ProductId == p.ProductId)
                {
                    list.Remove(p);
                }
            }
        }

        public Product GetProductById(int id)
        {
            foreach (var item in list.ToList())
            {
                if (item.ProductId == id)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
