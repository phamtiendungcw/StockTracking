using StockTracking.DAL;
using StockTracking.DAL.DAO;
using StockTracking.DAL.DTO;
using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.BLL
{
    public class CustomerBLL : IBLL<CustomerDetailDTO, CustomerDTO>
    {
        private CustomerDAO dao = new CustomerDAO();
        public bool Insert(CustomerDetailDTO entity)
        {
            CUSTOMER customer = new CUSTOMER();
            customer.Customername = entity.CustomerName;
            return dao.Insert(customer);
        }

        public bool Update(CustomerDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(CustomerDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public CustomerDTO Select()
        {
            CustomerDTO dto = new CustomerDTO();
            dto.Customers = dao.Select();
            return dto;
        }

        public bool GetBack(CustomerDetailDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
