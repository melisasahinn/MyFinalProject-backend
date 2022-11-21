using Core.Entities;
using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//core katmanı diğer katları referans almaz.
namespace Core.DateAccess.Abstract
{
    //<> sınırlama -->generic constraint
    //class:referans tip olabilir
    //Ientity olabilir veya Ientity implemente eden nesne olabilir
    //newlenebilir olmalı çünkü interfave newlenemez kendi sınıflarımızı yazabiliriz <category>
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        
        //Expression<Func<T,bool>> ----> filtreler yazmamızı sağlar
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        //List<T> GetAllByCategory(int categoryId);

    }
}
