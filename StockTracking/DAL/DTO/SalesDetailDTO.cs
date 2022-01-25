using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.DAL.DTO
{
    public class SalesDetailDTO
    {
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int SalesAmount { get; set; }
        public int Price { get; set; }
        public DateTime SalesDate { get; set; }
        public int StockAmount { get; set; }
        public int SalesId { get; set; }
        public bool isCategoryDeleted { get; set; }
        public bool isCustomerDeleted { get; set; }
        public bool isProductDeleted { get; set; }
    }
}
