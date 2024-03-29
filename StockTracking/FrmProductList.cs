﻿using StockTracking.BLL;
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
    public partial class FrmProductList : Form
    {
        ProductBLL bll = new ProductBLL();
        ProductDTO dto = new ProductDTO();
        ProductDetailDTO detail = new ProductDetailDTO();

        public FrmProductList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtProductPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtProductStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmProduct frm = new FrmProduct();
            frm.dto = dto;
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            dto = bll.Select();
            gridProductList.DataSource = dto.Products;
            CleanFilter();
        }

        private void FrmProductList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
            gridProductList.DataSource = dto.Products;
            gridProductList.Columns[0].HeaderText = "Product Name";
            gridProductList.Columns[1].HeaderText = "Category Name";
            gridProductList.Columns[2].HeaderText = "Stock Amount";
            gridProductList.Columns[3].HeaderText = "Price";
            gridProductList.Columns[4].Visible = false;
            gridProductList.Columns[5].Visible = false;
            gridProductList.Columns[6].Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<ProductDetailDTO> list = dto.Products;
            if (txtProductName.Text.Trim() != "")
                list = list.Where(x => x.ProductName.Contains(txtProductName.Text)).ToList();

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
            if (txtProductStock.Text.Trim() != "")
            {
                if (rbStockEqual.Checked)
                    list = list.Where(x => x.Price == Convert.ToInt32(txtProductStock.Text)).ToList();
                else if (rbStockMore.Checked)
                    list = list.Where(x => x.Price > Convert.ToInt32(txtProductStock.Text)).ToList();
                else if (rbStockLess.Checked)
                    list = list.Where(x => x.Price < Convert.ToInt32(txtProductStock.Text)).ToList();
                else
                    MessageBox.Show("Please select a criterion from stock group");
            }

            gridProductList.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanFilter();
        }

        private void CleanFilter()
        {
            txtProductPrice.Clear();
            txtProductName.Clear();
            txtProductStock.Clear();
            cmbCategory.SelectedValue = -1;
            rbStockLess.Checked = false;
            rbStockMore.Checked = false;
            rbStockEqual.Checked = false;
            rbPriceEqual.Checked = false;
            rbPriceLess.Checked = false;
            rbPriceMore.Checked = false;
            gridProductList.DataSource = dto.Products;
        }

        private void gridProductList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new ProductDetailDTO();
            detail.ProductId = Convert.ToInt32(gridProductList.Rows[e.RowIndex].Cells[4].Value);
            detail.CategoryId = Convert.ToInt32(gridProductList.Rows[e.RowIndex].Cells[5].Value);
            detail.ProductName = gridProductList.Rows[e.RowIndex].Cells[0].Value.ToString();
            detail.Price = Convert.ToInt32(gridProductList.Rows[e.RowIndex].Cells[3].Value);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.ProductId == 0)
                MessageBox.Show("Please select a product from table");
            else
            {
                FrmProduct frm = new FrmProduct();
                frm.isUpdate = true;
                frm.detail = detail;
                frm.dto = dto;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                bll = new ProductBLL();
                dto = bll.Select();
                gridProductList.DataSource = dto.Products;
                CleanFilter();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(detail.ProductId==0)
                MessageBox.Show("Please select a product from table");
            else
            {
                DialogResult rs = MessageBox.Show("Are you sure?", "Warning!!", MessageBoxButtons.YesNo);
                if (rs==DialogResult.Yes)
                {
                    if (bll.Delete(detail))
                    {
                        MessageBox.Show("Product was deleted");
                        bll = new ProductBLL();
                        dto = bll.Select();
                        gridProductList.DataSource = dto.Products;
                        cmbCategory.DataSource = dto.Categories;
                        CleanFilter();
                    }
                }
            }
        }
    }
}
