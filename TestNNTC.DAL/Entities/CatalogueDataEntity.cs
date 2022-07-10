using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNNTC.DAL.Entities
{
    public class CatalogueDataEntity : BaseEntity
    {
        public string CategoryName { get; set; }
       
        public List<CatalogueDataProduct> CatalogueProducts { get; set; }
    }

    public class CatalogueDataProduct : BaseEntity
    {
        public string ProductName { get; set; }
        public string Description {get;set;}
        public int Cost { get; set; }
    }
    
}
