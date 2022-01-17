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
    public partial class FrmAddStock : Form
    {
        ProductBLL bll = new ProductBLL();
        ProductDTO dto = new ProductDTO();
        bool combofull = false;
        ProductDetailDTO detail = new ProductDetailDTO();


        public FrmAddStock()
        {
            InitializeComponent();
        }

        private void txtProductStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text.Trim() == "")
                MessageBox.Show("Please select a product from table");
            if (txtProductStock.Text.Trim() == "")
                MessageBox.Show("Please give a stock amount");
            else
            {
                int sumStock = detail.StockAmount;
                sumStock += Convert.ToInt32(txtProductStock.Text);
                detail.StockAmount = sumStock;
                if (bll.Update(detail))
                {
                    MessageBox.Show("Stock was added");
                    bll = new ProductBLL();
                    dto = bll.Select();
                    gridAddStock.DataSource = dto.Products;
                    txtProductStock.Clear();
                }
            }
        }

        private void FrmAddStock_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            gridAddStock.DataSource = dto.Products;
            gridAddStock.Columns[0].HeaderText = "Product Name";
            gridAddStock.Columns[1].HeaderText = "Category Name";
            gridAddStock.Columns[2].HeaderText = "Stock Amount";
            gridAddStock.Columns[3].HeaderText = "Price";
            gridAddStock.Columns[4].Visible = false;
            gridAddStock.Columns[5].Visible = false;
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
            if (dto.Categories.Count > 0)
                combofull = true;
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull)
            {
                List<ProductDetailDTO> list = dto.Products;
                list = list.Where(x => x.CategoryId == Convert.ToInt32(cmbCategory.SelectedValue)).ToList();
                gridAddStock.DataSource = list;
                if (list.Count == 0)
                {
                    txtProductPrice.Clear();
                    txtProductName.Clear();
                    txtProductStock.Clear();
                }
            }
        }

        private void gridAddStock_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.ProductName = (string)gridAddStock.Rows[e.RowIndex].Cells[0].Value;
            txtProductName.Text = detail.ProductName;
            detail.Price = Convert.ToInt32(gridAddStock.Rows[e.RowIndex].Cells[3].Value);
            txtProductPrice.Text = detail.Price.ToString();
            detail.StockAmount = Convert.ToInt32(gridAddStock.Rows[e.RowIndex].Cells[2].Value);
            detail.ProductId = Convert.ToInt32(gridAddStock.Rows[e.RowIndex].Cells[4].Value);
        }
    }
}
