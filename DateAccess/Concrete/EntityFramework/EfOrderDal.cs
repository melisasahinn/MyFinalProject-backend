using Core.DataAccess.EntityFramework;
using DateAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateAccess.Concrete.EntityFramework
{
  public class EfOrderDal: EfEntityRepositoryBase<Order ,NortwindContext>,IOrderDal
    {
    }
}
