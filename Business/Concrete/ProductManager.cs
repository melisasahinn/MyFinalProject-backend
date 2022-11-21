using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
//AOP -----> metot loglama(başında sonunda ya da hata verdiğinde kullanılır.) Aop ile hatayı düzenleyebiliriz.
namespace Business.Concrete
{
    //*********Product Manager IProduct dal dışında enjekte edemez.
    public class ProductManager:IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        ICacheManager _cacheManager;

       //Cross Custting Concerns:doğrulama(validation ,log ,cache ,transaction ,auth)

        public ProductManager(IProductDal productDal ,ICategoryService categoryService,ICacheManager cacheManager) 
        {
            _productDal = productDal;
            _categoryService= categoryService;
            _cacheManager = cacheManager;
          
        }
        //LOgger: yapılan organizasyonların kaydını tutmak (kim ne zaman nerede ürün ekledi)
        [SecuredOperation("product.add,admin")]//korunan oprarasyon
        //product.add,admine anahtar vermek : Claim
        //[SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        //[CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {

            //validation yapıldı.


            //business codes
            //Bir kategoride en fazla 10 ürün olabilir.
            //Eğer mevcut kategori sayısı 15'i geçtiyse yeni ürün eklenemez.
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryLimitExceded());
                
            if(result !=null)//kurala uymayan result varsa
            {
                return result;
            }


            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        

        }

        [CacheAspect]  //sürekli veritabanına gitmemesi için ---key,value
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            // iş kodları ----> iş sınıfları başka sınıfları newlemez
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);
        }
        [CacheAspect]
        public IDataResult<List<Product>> GetAllByCategory(int productId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p =>p.CategoryId == productId ));
        }
        [CacheAspect]
        public IDataResult<Product> GetById(int id)
        {

            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            throw new NotImplementedException();
            //return new SuccessDataResult<Product>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetailDtos()
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            throw new NotImplementedException();
            // return new SuccessDataResult<Product>(_productDal.GetProductDetails());
        }
        [ValidationAspect(typeof(ProductValidator))]
        //[CacheRemoveAspect("IProductService.Get")]//içinden geçeni siler.
        public IResult Update(Product product)
        {
            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int CategoriId)
        {//Select count (*) from products where categoryId=1
            var result = _productDal.GetAll(p => p.CategoryId == CategoriId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.CheckIfProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        public IResult AddTransactionalTest(Product product)
        {
            using (TransactionScope scope=new TransactionScope())
            {

            }
            Add(product);
            if (product.UnitPrice < 10) 
            {
                throw new Exception(""); 
            }
            
            Add(product);
            return null;
        }

        public object GetByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }//Any:bunun gibi kayıt var mı
}
// ***** İş Kurallarını bu şekilde yazarsak spagetti kod olur.
/* var result = _productDal.GetAll(p => p.CategoriId == product.CategoriId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }*/