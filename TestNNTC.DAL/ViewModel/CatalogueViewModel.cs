using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNNTC.DAL.Entities;

namespace TestNNTC.DAL.ViewModel
{
    public class CatalogueViewModel
    {
        public string CategoryName { get; set; }
        public List<CatalogueDataProduct> CatalogueDataProducts { get; set; }
    }
}
