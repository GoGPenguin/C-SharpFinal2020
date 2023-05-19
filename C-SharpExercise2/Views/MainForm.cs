using C_SharpExercise2.BLL;
using C_SharpExercise2.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_SharpExercise2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SetInitData();
            LoadCBB();
            LoadCBBSort();
        }

        public void LoadCBBSort()
        {
            this.cbbSort.Items.Clear();
            for (int i = 0; i < dgv.Columns.Count; i++)
            {   
                if (dgv.Columns[i].HeaderText.ToString() != "MaHP" && dgv.Columns[i].HeaderText.ToString() != "Gender")
                {
                    this.cbbSort.Items.Add(dgv.Columns[i].HeaderText.ToString());
                }
            }
            this.cbbSort.SelectedIndex = 0;
        }

        public void LoadCBB()
        {   
            this.cbbHP.Items.Clear();
            this.cbbHP.Items.Add(new CBBItems
            {
                Value = "0",
                Text = "All"
            });
            this.cbbHP.Items.AddRange(QLSV_BLL.Instance.GetLopHP().ToArray());
            this.cbbHP.SelectedIndex = 0;
        }

        public void SetInitData()
        {
            var data = QLSV_BLL.Instance.GetAllSV();
            dgv.DataSource = data;
            if (data.Count != 0)
            {
                dgv.Columns["MSSV"].Visible = false;
                dgv.Columns["MaHP"].Visible = false;
                dgv.Columns["Gender"].Visible = false;
            }
        }

        public void ShowData()
        {   
            var data = QLSV_BLL.Instance.GetAllSV(txtSearch.Text, cbbHP.SelectedItem.ToString(), cbbSort.SelectedItem.ToString());
            dgv.DataSource = data;
            if (data.Count != 0 ) 
            {
                dgv.Columns["MSSV"].Visible = false;
                dgv.Columns["MaHP"].Visible = false;
                dgv.Columns["Gender"].Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow i in dgv.SelectedRows)
                {
                    string id = i.Cells["MSSV"].Value.ToString();
                    string id2 = i.Cells["MaHP"].Value.ToString();
                    QLSV_BLL.Instance.DeleteSV(id, id2);
                }
                ShowData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng cần xóa");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 1)
            {
                DetailForm f = new DetailForm(dgv.SelectedRows[0].Cells["MSSV"].Value.ToString(), dgv.SelectedRows[0].Cells["MaHP"].Value.ToString());

                f.ShowData += new DetailForm.DelegateForm(this.ShowData);
                f.Show();
                this.cbbHP.SelectedIndex = 0;
                this.cbbSort.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Chỉ chỉnh sửa mỗi lần 1 sinh viên");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DetailForm f = new DetailForm();
            f.ShowData += new DetailForm.DelegateForm(this.ShowData);
            f.Show();
            this.cbbHP.SelectedIndex = 0;
            this.cbbSort.SelectedIndex = 0;
        }
    }
}
