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
    public partial class FrmSalesList : Form
    {
        SalesBLL bll = new SalesBLL();
        SalesDTO dto = new SalesDTO();
        SalesDetailDTO detail = new SalesDetailDTO();

        public FrmSalesList()
        {
            InitializeComponent();
        }

        private void txtProductPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtSalesAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmSales frm = new FrmSales();
            frm.dto = dto;
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            dto = bll.Select();
            gridSalesList.DataSource = dto.Sales;
            CleanFilters();
        }

        private void FrmSalesList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            gridSalesList.DataSource = dto.Sales;
            gridSalesList.Columns[0].HeaderText = "Customer Name";
            gridSalesList.Columns[1].HeaderText = "Product Name";
            gridSalesList.Columns[2].HeaderText = "Category Name";
            gridSalesList.Columns[6].HeaderText = "Sales Amount";
            gridSalesList.Columns[7].HeaderText = "Price";
            gridSalesList.Columns[8].HeaderText = "Sales Date";
            gridSalesList.Columns[3].Visible = false;
            gridSalesList.Columns[4].Visible = false;
            gridSalesList.Columns[5].Visible = false;
            gridSalesList.Columns[9].Visible = false;
            gridSalesList.Columns[10].Visible = false;
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<SalesDetailDTO> list = dto.Sales;
            if (txtProductName.Text.Trim() != "")
                list = list.Where(x => x.ProductName.Contains(txtProductName.Text)).ToList();
            if (txtCustomerName.Text.Trim() != "")
                list = list.Where(x => x.CustomerName.Contains(txtCustomerName.Text)).ToList();
            if (cmbCategory.SelectedIndex != -1)
                list = list.Where(x => x.CategoryId == Convert.ToInt32(cmbCategory.SelectedValue)).ToList();
            if (txtProductPrice.Text.Trim() != "")
            {
                if (rbPriceEqual.Checked)
                    list = list.Where(x => x.Price == Convert.ToInt32(txtProductPrice.Text)).ToList();
                else if (rbPriceMore.Checked)
                    list = list.Where(x => x.Price > Convert.ToInt32(txtProductPrice.Text)).ToList();
                else if (rbPriceLess.Checked)
                    list = list.Where(x => x.Price < Convert.ToInt32(txtProductPrice.Text)).ToList();
                else
                    MessageBox.Show("Please select a criterion from price group");
            }
            if (txtSalesAmount.Text.Trim() != "")
            {
                if (rbSalesAmountEqual.Checked)
                    list = list.Where(x => x.SalesAmount == Convert.ToInt32(txtSalesAmount.Text)).ToList();
                else if (rbSalesAmountMore.Checked)
                    list = list.Where(x => x.SalesAmount > Convert.ToInt32(txtSalesAmount.Text)).ToList();
                else if (rbSalesAmountLess.Checked)
                    list = list.Where(x => x.SalesAmount < Convert.ToInt32(txtSalesAmount.Text)).ToList();
                else
                    MessageBox.Show("Please select a criterion from Sale amount group");
            }

            if (chDate.Checked)
                list = list.Where(x => x.SalesDate > dpStart.Value && x.SalesDate < dpEnd.Value).ToList();
            gridSalesList.DataSource = list;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanFilters();
        }

        private void CleanFilters()
        {
            txtProductPrice.Clear();
            txtCustomerName.Clear();
            txtProductName.Clear();
            txtSalesAmount.Clear();
            rbPriceEqual.Checked = false;
            rbPriceLess.Checked = false;
            rbPriceMore.Checked = false;
            rbSalesAmountEqual.Checked = false;
            rbSalesAmountLess.Checked = false;
            rbSalesAmountMore.Checked = false;
            dpEnd.Value = DateTime.Today;
            dpStart.Value = DateTime.Today;
            chDate.Checked = false;
            cmbCategory.SelectedIndex = -1;
            gridSalesList.DataSource = dto.Sales;
        }

        private void gridSalesList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new SalesDetailDTO();
            detail.SalesId = Convert.ToInt32(gridSalesList.Rows[e.RowIndex].Cells[10].Value);
            detail.ProductId = Convert.ToInt32(gridSalesList.Rows[e.RowIndex].Cells[4].Value);
            detail.CustomerName = gridSalesList.Rows[e.RowIndex].Cells[0].Value.ToString();
            detail.ProductName = gridSalesList.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.Price = Convert.ToInt32(gridSalesList.Rows[e.RowIndex].Cells[7].Value);
            detail.SalesAmount = Convert.ToInt32(gridSalesList.Rows[e.RowIndex].Cells[6].Value);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.SalesId == 0)
                MessageBox.Show("Please select sales from table");
            else
            {
                FrmSales frm = new FrmSales();
                frm.isUpdate = true;
                frm.detail = detail;
                frm.dto = dto;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                bll = new SalesBLL();
                dto = bll.Select();
                gridSalesList.DataSource = dto.Sales;
                CleanFilters();
            }
        }
    }
}
