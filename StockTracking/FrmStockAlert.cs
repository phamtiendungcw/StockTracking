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
    public partial class FrmStockAlert : Form
    {
        ProductBLL bll = new ProductBLL();
        ProductDTO dto = new ProductDTO();

        public FrmStockAlert()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            FrmMain frm = new FrmMain();
            this.Hide();
            frm.ShowDialog();
        }

        private void FrmStockAlert_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            dto.Products = dto.Products.Where(x => x.StockAmount <= 10).ToList();
            gridStockAlert.DataSource = dto.Products;
            gridStockAlert.Columns[0].HeaderText = "Product Name";
            gridStockAlert.Columns[1].HeaderText = "Category Name";
            gridStockAlert.Columns[2].HeaderText = "Stock Amount";
            gridStockAlert.Columns[3].HeaderText = "Price";
            gridStockAlert.Columns[4].Visible = false;
            gridStockAlert.Columns[5].Visible = false;
            if (dto.Products.Count == 0)
            {
                FrmMain frm = new FrmMain();
                this.Hide();
                frm.ShowDialog();
            }

        }
    }
}
