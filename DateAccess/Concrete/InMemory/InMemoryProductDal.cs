using DateAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DateAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {

        List<Product> _products;

        public InMemoryProductDal()
        {
            //oracle ,sql , server, postgres,mongodb 'den gelmiş gibi simüle ettik
            _products = new List<Product> 
            { 
                new Product{ ProductId=1, CategoriId=1, ProductName="Bardak",UnitPrice=15,UnitsInStock=15},
                new Product{ ProductId=2, CategoriId=1, ProductName="Kamera",UnitPrice=500,UnitsInStock=3},
                new Product{ ProductId=3, CategoriId=2, ProductName="Telefon",UnitPrice=1500,UnitsInStock=2},
                new Product{ ProductId=4, CategoriId=2, ProductName="Klavye",UnitPrice=150,UnitsInStock=65},
                new Product{ ProductId=5, CategoriId=2, ProductName="Fare",UnitPrice=85,UnitsInStock=1}
            };
        }

        public void Add(Product product) 
        {
            _products.Add(product);
        }
        public void Update(Product product)
        { //göderdiğim ürün id'sine sahip olan listedeki ürünü bul
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoriId = product.CategoriId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
        public void Delete(Product product)
        {
            //LINQ - Language Integrated Queru
           // => lambda her p için 


            //Product productToDelete = null;
            //foreach (var p in _products)
            //{
            //    if (product.ProductId == p.ProductId)
            //    {
            //        productToDelete = p;
            //    }
            //}
            //
             Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);
            _products.Remove(productToDelete);
        }
        public List<Product> GetAll() 
        {
            return _products;
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
           return _products.Where(p=>p.CategoriId== categoryId).ToList();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        List<ProductDetailDto> IProductDal.GetProductDetails()
        {
            throw new NotImplementedException();
        }
    }
}


