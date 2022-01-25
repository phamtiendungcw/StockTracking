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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace StockTracking
{
    public partial class FrmDeleted : Form
    {
        SalesDTO dto = new SalesDTO();
        SalesBLL bll = new SalesBLL();
        SalesDetailDTO salesDetail = new SalesDetailDTO();
        ProductDetailDTO productDetail = new ProductDetailDTO();
        CustomerDetailDTO customerDetail = new CustomerDetailDTO();
        CategoryDetailDTO categoryDetail = new CategoryDetailDTO();
        CategoryBLL categoryBll = new CategoryBLL();
        ProductBLL productBll = new ProductBLL();
        CustomerBLL customerBll = new CustomerBLL();
        SalesBLL saleBll = new SalesBLL();

        public FrmDeleted()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmDeleted_Load(object sender, EventArgs e)
        {
            cmbDeletedData.Items.Add("Category");
            cmbDeletedData.Items.Add("Product");
            cmbDeletedData.Items.Add("Customer");
            cmbDeletedData.Items.Add("Sales");
            dto = bll.Select(true);
            gridDeleted.DataSource = dto.Sales;
            gridDeleted.Columns[0].HeaderText = "Customer Name";
            gridDeleted.Columns[1].HeaderText = "Product Name";
            gridDeleted.Columns[2].HeaderText = "Category Name";
            gridDeleted.Columns[6].HeaderText = "Sales Amount";
            gridDeleted.Columns[7].HeaderText = "Price";
            gridDeleted.Columns[8].HeaderText = "Sales Date";
            gridDeleted.Columns[3].Visible = false;
            gridDeleted.Columns[4].Visible = false;
            gridDeleted.Columns[5].Visible = false;
            gridDeleted.Columns[9].Visible = false;
            gridDeleted.Columns[10].Visible = false;
        }

        private void cmbDeletedData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDeletedData.SelectedIndex == 0)
            {
                gridDeleted.DataSource = dto.Categories;
                gridDeleted.Columns[0].Visible = false;
                gridDeleted.Columns[1].HeaderText = "Category Name";
            }
            if (cmbDeletedData.SelectedIndex == 1)
            {
                gridDeleted.DataSource = dto.Products;
                gridDeleted.Columns[0].HeaderText = "Product Name";
                gridDeleted.Columns[1].HeaderText = "Category Name";
                gridDeleted.Columns[2].HeaderText = "Stock Amount";
                gridDeleted.Columns[3].HeaderText = "Price";
                gridDeleted.Columns[4].Visible = false;
                gridDeleted.Columns[5].Visible = false;
                gridDeleted.Columns[6].Visible = false;
            }
            if (cmbDeletedData.SelectedIndex == 2)
            {
                gridDeleted.DataSource = dto.Customers;
                gridDeleted.Columns[0].Visible = false;
                gridDeleted.Columns[1].HeaderText = "Customer Name";
            }
            if (cmbDeletedData.SelectedIndex == 3)
            {
                gridDeleted.DataSource = dto.Sales;
                gridDeleted.Columns[0].HeaderText = "Customer Name";
                gridDeleted.Columns[1].HeaderText = "Product Name";
                gridDeleted.Columns[2].HeaderText = "Category Name";
                gridDeleted.Columns[6].HeaderText = "Sales Amount";
                gridDeleted.Columns[7].HeaderText = "Price";
                gridDeleted.Columns[8].HeaderText = "Sales Date";
                gridDeleted.Columns[3].Visible = false;
                gridDeleted.Columns[4].Visible = false;
                gridDeleted.Columns[5].Visible = false;
                gridDeleted.Columns[9].Visible = false;
                gridDeleted.Columns[10].Visible = false;
                gridDeleted.Columns[11].Visible = false;
                gridDeleted.Columns[12].Visible = false;
                gridDeleted.Columns[13].Visible = false;
            }
        }

        private void gridDeleted_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (cmbDeletedData.SelectedIndex == 0)
            {
                categoryDetail = new CategoryDetailDTO();
                categoryDetail.Id = Convert.ToInt32(gridDeleted.Rows[e.RowIndex].Cells[0].Value);
                categoryDetail.CategoryName = gridDeleted.Rows[e.RowIndex].Cells[1].Value.ToString();
            }

            else if (cmbDeletedData.SelectedIndex == 1)
            {
                productDetail = new ProductDetailDTO();
                productDetail.ProductId = Convert.ToInt32(gridDeleted.Rows[e.RowIndex].Cells[4].Value);
                productDetail.CategoryId = Convert.ToInt32(gridDeleted.Rows[e.RowIndex].Cells[5].Value);
                productDetail.ProductName = gridDeleted.Rows[e.RowIndex].Cells[0].Value.ToString();
                productDetail.Price = Convert.ToInt32(gridDeleted.Rows[e.RowIndex].Cells[3].Value);
                productDetail.isCategoryDeleted = Convert.ToBoolean(gridDeleted.Rows[e.RowIndex].Cells[6].Value);
            }

            else if (cmbDeletedData.SelectedIndex == 2)
            {
                customerDetail = new CustomerDetailDTO();
                customerDetail.Id = Convert.ToInt32(gridDeleted.Rows[e.RowIndex].Cells[0].Value);
                customerDetail.CustomerName = gridDeleted.Rows[e.RowIndex].Cells[1].Value.ToString();
            }

            else if (cmbDeletedData.SelectedIndex == 3)
            {
                salesDetail = new SalesDetailDTO();
                salesDetail.SalesId = Convert.ToInt32(gridDeleted.Rows[e.RowIndex].Cells[10].Value);
                salesDetail.ProductId = Convert.ToInt32(gridDeleted.Rows[e.RowIndex].Cells[4].Value);
                salesDetail.CustomerName = gridDeleted.Rows[e.RowIndex].Cells[0].Value.ToString();
                salesDetail.ProductName = gridDeleted.Rows[e.RowIndex].Cells[1].Value.ToString();
                salesDetail.Price = Convert.ToInt32(gridDeleted.Rows[e.RowIndex].Cells[7].Value);
                salesDetail.SalesAmount = Convert.ToInt32(gridDeleted.Rows[e.RowIndex].Cells[6].Value);
                salesDetail.isCategoryDeleted = Convert.ToBoolean(gridDeleted.Rows[e.RowIndex].Cells[11].Value);
                salesDetail.isCustomerDeleted = Convert.ToBoolean(gridDeleted.Rows[e.RowIndex].Cells[12].Value);
                salesDetail.isProductDeleted = Convert.ToBoolean(gridDeleted.Rows[e.RowIndex].Cells[13].Value);
            }
        }

        private void btnGetBack_Click(object sender, EventArgs e)
        {
            if (cmbDeletedData.SelectedIndex == 0)
            {
                if (categoryBll.GetBack(categoryDetail))
                {
                    MessageBox.Show("Category was Get back");
                    dto = bll.Select(true);
                    gridDeleted.DataSource = dto.Categories;
                }
            }

            else if (cmbDeletedData.SelectedIndex == 1)
            {
                if (productDetail.isCategoryDeleted)
                    MessageBox.Show("Category was deleted first get back category");
                if (productBll.GetBack(productDetail))
                {
                    MessageBox.Show("Product was Get back");
                    dto = bll.Select(true);
                    gridDeleted.DataSource = dto.Products;
                }
            }

            else if (cmbDeletedData.SelectedIndex == 2)
            {
                if (customerBll.GetBack(customerDetail))
                {
                    MessageBox.Show("Customer was Get back");
                    dto = bll.Select(true);
                    gridDeleted.DataSource = dto.Customers;
                }
            }

            else if (cmbDeletedData.SelectedIndex == 3)
            {
                if (salesDetail.isCategoryDeleted || salesDetail.isCustomerDeleted || salesDetail.isProductDeleted)
                {
                    if (salesDetail.isCategoryDeleted)
                        MessageBox.Show("Category was deleted first get back category");
                    else if (salesDetail.isCustomerDeleted)
                        MessageBox.Show("Customer was deleted first get back customer");
                    else if (salesDetail.isProductDeleted)
                        MessageBox.Show("Product was deleted first get back product");

                }
                else if (saleBll.GetBack(salesDetail))
                {
                    MessageBox.Show("Sales was Get back");
                    dto = bll.Select(true);
                    gridDeleted.DataSource = dto.Sales;
                }
            }
        }
    }
}
