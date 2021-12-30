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
    public partial class FrmCategoryList : Form
    {
        CategoryDTO dto = new CategoryDTO();
        CategoryBLL bll = new CategoryBLL();

        public FrmCategoryList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmCategory frm = new FrmCategory();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            dto = bll.Select();
            gridCategoryList.DataSource = dto.Categories;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCategoryList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            gridCategoryList.DataSource = dto.Categories;
            gridCategoryList.Columns[0].Visible = false;
            gridCategoryList.Columns[1].HeaderText = "Category Name";
        }

        private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {
            List<CategoryDetailDTO> list = dto.Categories;
            list = list.Where(x => x.CategoryName.Contains(txtCategoryName.Text)).ToList();
            gridCategoryList.DataSource = list;
        }
    }
}
