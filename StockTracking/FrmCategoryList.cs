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
        CategoryDetailDTO detail = new CategoryDetailDTO();

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

        private void gridCategoryList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new CategoryDetailDTO();
            detail.Id = Convert.ToInt32(gridCategoryList.Rows[e.RowIndex].Cells[0].Value);
            detail.CategoryName = gridCategoryList.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.Id == 0)
                MessageBox.Show("Please select a category from table");
            else
            {
                FrmCategory frm = new FrmCategory();
                frm.detail = detail;
                frm.isUpdate = true;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                bll = new CategoryBLL();
                dto = bll.Select();
                gridCategoryList.DataSource = dto.Categories;
            }

        }
    }
}
