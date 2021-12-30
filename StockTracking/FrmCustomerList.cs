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
    public partial class FrmCustomerList : Form
    {
        private CustomerBLL bll = new CustomerBLL();
        private CustomerDTO dto = new CustomerDTO();

        public FrmCustomerList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmCustomer frm = new FrmCustomer();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            dto = bll.Select();
            gridCustomerList.DataSource = dto.Customers;
        }

        private void FrmCustomerList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            gridCustomerList.DataSource = dto.Customers;
            gridCustomerList.Columns[0].Visible = false;
            gridCustomerList.Columns[1].HeaderText = "Customer Name";

        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            List<CustomerDetailDTO> list = dto.Customers;
            list = list.Where(x => x.CustomerName.Contains(txtCustomerName.Text)).ToList();
            gridCustomerList.DataSource = list;
        }
    }
}
