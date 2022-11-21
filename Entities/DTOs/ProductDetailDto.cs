using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductDetailDto:IDtos
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoyName { get; set; }
        public short UnitsInStock { get; set; }
    }
}
