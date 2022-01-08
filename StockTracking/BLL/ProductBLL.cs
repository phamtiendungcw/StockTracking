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
    public class ProductBLL : IBLL<ProductDetailDTO, ProductDTO>
    {
        public CategoryDAO categorydao = new CategoryDAO();
        public ProductDAO dao = new ProductDAO();

        public bool Insert(ProductDetailDTO entity)
        {
            PRODUCT product = new PRODUCT();
            product.ProductName = entity.ProductName;
            product.CategoryID = entity.CategoryId;
            product.Price = entity.Price;
            return dao.Insert(product);
        }

        public bool Update(ProductDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ProductDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public ProductDTO Select()
        {
            ProductDTO dto = new ProductDTO();
            dto.Categories = categorydao.Select();
            dto.Products = dao.Select();
            return dto;
        }

        public bool GetBack(ProductDetailDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
