﻿using StockTracking.DAL;
using StockTracking.DAL.DAO;
using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.DAL.DAO
{
    public class CustomerDAO : StockContext, IDAO<CUSTOMER, CustomerDetailDTO>
    {
        public List<CustomerDetailDTO> Select()
        {
            try
            {
                List<CustomerDetailDTO> customers = new List<CustomerDetailDTO>();
                var list = db.CUSTOMERs.Where(x => x.isDeleted == false);
                foreach (var item in list)
                {
                    CustomerDetailDTO dto = new CustomerDetailDTO();
                    dto.CustomerName = item.Customername;
                    dto.Id = item.ID;
                    customers.Add(dto);
                }

                return customers;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<CustomerDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<CustomerDetailDTO> customers = new List<CustomerDetailDTO>();
                var list = db.CUSTOMERs.Where(x => x.isDeleted == isDeleted);
                foreach (var item in list)
                {
                    CustomerDetailDTO dto = new CustomerDetailDTO();
                    dto.CustomerName = item.Customername;
                    dto.Id = item.ID;
                    customers.Add(dto);
                }

                return customers;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool Insert(CUSTOMER entity)
        {
            try
            {
                db.CUSTOMERs.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool Update(CUSTOMER entity)
        {
            try
            {
                CUSTOMER customer = db.CUSTOMERs.First(x => x.ID == entity.ID);
                customer.Customername = entity.Customername;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool Delete(CUSTOMER entity)
        {
            try
            {
                CUSTOMER customer = db.CUSTOMERs.First(x => x.ID == entity.ID);
                customer.isDeleted = true;
                customer.DeletedDate = DateTime.Today;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                CUSTOMER customer = db.CUSTOMERs.First(x => x.ID == ID);
                customer.isDeleted = false;
                customer.DeletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
