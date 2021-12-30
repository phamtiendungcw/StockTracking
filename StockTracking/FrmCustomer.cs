using StockTracking.BLL;
using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockTracking
{
    public partial class FrmCustomer : Form
    {
        private CustomerBLL bll = new CustomerBLL();

        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text.Trim() == "")
                MessageBox.Show("Customer Name is empty");
            else
            {
                CustomerDetailDTO customer = new CustomerDetailDTO();
                customer.CustomerName = txtCustomerName.Text;
                if (bll.Insert(customer))
                {
                    MessageBox.Show("Customer was added");
                    txtCustomerName.Clear();
                }

            }
        }
    }
}
