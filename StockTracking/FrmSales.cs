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
    public partial class FrmSales : Form
    {
        public SalesDTO dto = new SalesDTO();
        bool combofull = false;
        public SalesDetailDTO detail = new SalesDetailDTO();
        public SalesBLL bll = new SalesBLL();
        public bool isUpdate = false;

        public FrmSales()
        {
            InitializeComponent();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSales_Load(object sender, EventArgs e)
        {
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
            if (!isUpdate)
            {
                gridProducts.DataSource = dto.Products;
                gridProducts.Columns[0].HeaderText = "Product Name";
                gridProducts.Columns[1].HeaderText = "Category Name";
                gridProducts.Columns[2].HeaderText = "Stock Amount";
                gridProducts.Columns[3].HeaderText = "Price";
                gridProducts.Columns[4].Visible = false;
                gridProducts.Columns[5].Visible = false;
                gridCustomers.DataSource = dto.Customers;
                gridCustomers.Columns[0].Visible = false;
                gridCustomers.Columns[1].HeaderText = "Customer Name";
                if (dto.Categories.Count > 0)
                    combofull = true;
            }

            else
            {
                panel1.Hide();
                txtCustomerName.Text = detail.CustomerName;
                txtProductName.Text = detail.ProductName;
                txtPrice.Text = detail.Price.ToString();
                txtProductSalesAmount.Text = detail.SalesAmount.ToString();
                ProductDetailDTO product = dto.Products.First(x => x.ProductId == detail.ProductId);
                detail.StockAmount = product.StockAmount;
                txtProductStock.Text = detail.StockAmount.ToString();
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull)
            {
                List<ProductDetailDTO> list = dto.Products;
                list = list.Where(x => x.CategoryId == Convert.ToInt32(cmbCategory.SelectedValue)).ToList();
                gridProducts.DataSource = list;
                if (list.Count == 0)
                {
                    txtPrice.Clear();
                    txtProductName.Clear();
                    txtProductStock.Clear();
                }
            }
        }

        private void txtCustomers_CustomerName_TextChanged(object sender, EventArgs e)
        {
            List<CustomerDetailDTO> list = dto.Customers;
            list = list.Where(x => x.CustomerName.Contains(txtCustomers_CustomerName.Text)).ToList();
            gridCustomers.DataSource = list;
            if (list.Count == 0)
                txtCustomerName.Clear();
        }

        private void gridProducts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.ProductName = gridProducts.Rows[e.RowIndex].Cells[0].Value.ToString();
            detail.Price = Convert.ToInt32(gridProducts.Rows[e.RowIndex].Cells[3].Value);
            detail.StockAmount = Convert.ToInt32(gridProducts.Rows[e.RowIndex].Cells[2].Value);
            detail.ProductId = Convert.ToInt32(gridProducts.Rows[e.RowIndex].Cells[4].Value);
            detail.CategoryId = Convert.ToInt32(gridProducts.Rows[e.RowIndex].Cells[5].Value);
            txtProductName.Text = detail.ProductName;
            txtPrice.Text = detail.Price.ToString();
            txtProductStock.Text = detail.StockAmount.ToString();
        }

        private void gridCustomers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.CustomerName = gridCustomers.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.CustomerId = Convert.ToInt32(gridCustomers.Rows[e.RowIndex].Cells[0].Value);
            txtCustomerName.Text = detail.CustomerName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductSalesAmount.Text.Trim() == "")
                MessageBox.Show("Please fill the sales amount area");

            else
            {
                if (!isUpdate)
                {
                    if (detail.ProductId == 0)
                        MessageBox.Show("Please select a product from product table");
                    else if (detail.CustomerId == 0)
                        MessageBox.Show("Please select a customer from customer table");
                    else if (detail.StockAmount < Convert.ToInt32(txtProductSalesAmount.Text))
                        MessageBox.Show("You have bot enough product for sale");
                    else
                    {

                        detail.SalesAmount = Convert.ToInt32(txtProductSalesAmount.Text);
                        detail.SalesDate = DateTime.Today;
                        if (bll.Insert(detail))
                        {
                            MessageBox.Show("Sales was added");
                            bll = new SalesBLL();
                            dto = bll.Select();
                            gridProducts.DataSource = dto.Products;
                            dto.Customers = dto.Customers;
                            combofull = false;
                            cmbCategory.DataSource = dto.Categories;
                            if (dto.Products.Count > 0)
                                combofull = true;
                            txtProductSalesAmount.Clear();
                        }
                    }
                }
                else //Update
                {
                    if (detail.SalesAmount == Convert.ToInt32(txtProductSalesAmount.Text))
                        MessageBox.Show("There is No Change");
                    else
                    {
                        int temp = detail.StockAmount + detail.SalesAmount;
                        if (temp < Convert.ToInt32(txtProductSalesAmount.Text))
                            MessageBox.Show("You have not enough product for table");
                        else
                        {
                            detail.SalesAmount = Convert.ToInt32(txtProductSalesAmount.Text);
                            detail.StockAmount = temp - detail.SalesAmount;
                            if (bll.Update(detail))
                            {
                                MessageBox.Show("Sales was Updated");
                                this.Close();
                            }
                        }
                    }
                }
            }
        }
    }
}
