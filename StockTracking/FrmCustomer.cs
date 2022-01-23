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
        CustomerBLL bll = new CustomerBLL();
        public CustomerDetailDTO detail = new CustomerDetailDTO();
        public bool isUpdate = false;

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
                if (!isUpdate)
                {
                    CustomerDetailDTO customer = new CustomerDetailDTO();
                    customer.CustomerName = txtCustomerName.Text;
                    if (bll.Insert(customer))
                    {
                        MessageBox.Show("Customer was added");
                        txtCustomerName.Clear();
                    }
                }
                else
                {
                    if (detail.CustomerName == txtCustomerName.Text)
                        MessageBox.Show("There is No change");
                    else
                    {
                        detail.CustomerName = txtCustomerName.Text;
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Customer was updated");
                            this.Close();
                        }
                    }

                }

            }
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                txtCustomerName.Text = detail.CustomerName;
            }
        }
    }
}
