using StockTracking.DAL;
using StockTracking.DAL.DAO;
using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.BLL
{
    public class SalesBLL : IBLL<SalesDetailDTO, SalesDTO>
    {
        SalesDAO dao = new SalesDAO();
        ProductDAO productdao = new ProductDAO();
        CategoryDAO categorydao = new CategoryDAO();
        CustomerDAO customerdao = new CustomerDAO();

        public bool Insert(SalesDetailDTO entity)
        {
            SALE sales = new SALE();
            sales.CategoryID = entity.CategoryId;
            sales.ProductID = entity.ProductId;
            sales.CustomerID = entity.CustomerId;
            sales.ProductSalesPrice = entity.Price;
            sales.ProductSalesAmount = entity.SalesAmount;
            sales.SalesDate = entity.SalesDate;
            dao.Insert(sales);
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductId;
            int temp = entity.StockAmount - entity.SalesAmount;
            product.StockAmount = temp;
            productdao.Update(product);
            return true;
        }

        public bool Update(SalesDetailDTO entity)
        {
            SALE sales = new SALE();
            sales.ID = entity.SalesId;
            sales.ProductSalesAmount = entity.SalesAmount;
            dao.Update(sales);
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductId;
            product.StockAmount = entity.StockAmount;
            productdao.Update(product);
            return true;
        }

        public bool Delete(SalesDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public SalesDTO Select()
        {
            SalesDTO dto = new SalesDTO();
            dto.Products = productdao.Select();
            dto.Customers = customerdao.Select();
            dto.Categories = categorydao.Select();
            dto.Sales = dao.Select();
            return dto;
        }

        public bool GetBack(SalesDetailDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
