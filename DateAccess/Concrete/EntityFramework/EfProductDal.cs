using Core.DataAccess.EntityFramework;
using DateAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DateAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product,NortwindContext>,IProductDal

    {
        //NuGet ortak kullanım
      public List<ProductDetailDto> GetProductDetailDtos() 
        {
            //using (NortwindContext context =new NortwindContext())
            //{
            //    var result = from p in context.Products
            //                 join c in context.Categories
            //                 on p.CategoriId equals c.CategoryID
            //                 select new ProductDetailDto
            //                 {
            //                     ProductId = p.ProductId,
            //                     ProductName = p.Product.Name,
            //                     CategoryName = c.CategoryName,

            //                 };
            //    return result.ToList();
            //}
            throw new NotImplementedException();
        }

        List<Product> IProductDal.GetAll()
        {
            throw new NotImplementedException();
        }

        List<ProductDetailDto> IProductDal.GetProductDetails()
        {
            throw new NotImplementedException();
        }
    }
}
