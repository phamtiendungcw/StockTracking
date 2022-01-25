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
        CustomerBLL bll = new CustomerBLL();
        CustomerDTO dto = new CustomerDTO();
        public CustomerDetailDTO detail = new CustomerDetailDTO();



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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.Id == 0)
                MessageBox.Show("Please select a customer from table");
            else
            {
                FrmCustomer frm = new FrmCustomer();
                frm.detail = detail;
                frm.isUpdate = true;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                bll = new CustomerBLL();
                dto = bll.Select();
                gridCustomerList.DataSource = dto.Customers;
            }

        }

        private void gridCustomerList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new CustomerDetailDTO();
            detail.Id = Convert.ToInt32(gridCustomerList.Rows[e.RowIndex].Cells[0].Value);
            detail.CustomerName = gridCustomerList.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(detail.Id == 0)
                MessageBox.Show("Please select a customer from table");
            else
            {
                DialogResult rs = MessageBox.Show("Are you sure?", "Warning!!", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    if (bll.Delete(detail))
                    {
                        MessageBox.Show("Customer was deleted");
                        bll = new CustomerBLL();
                        dto = bll.Select();
                        gridCustomerList.DataSource = dto.Customers;
                        txtCustomerName.Clear();
                    }
                }

            }
        }
    }
}
