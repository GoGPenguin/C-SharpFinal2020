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
using System.Windows.Forms.VisualStyles;

namespace C_SharpExercise2
{
    public partial class DetailForm : Form
    {
        public delegate void DelegateForm();

        public DelegateForm ShowData { get; set; }

        string MSSV, MaHP;

        public DetailForm(string mssv = null, string mahp = null)
        {
            InitializeComponent();
            this.MSSV = mssv;
            this.MaHP = mahp;
            LoadCBBLopHP();
            LoadCBBLopSH();
            LoadDetail();
        }

        private void LoadDetail()
        {
            if (MSSV == null)
            {
                this.Text = "ADD";
                rBtnMale.Checked = true;
            }
            else
            {
                this.Text = "Edit";
                var sv = QLSV_BLL.Instance.GetSVByID(MSSV, MaHP);
                txtIdSV.Text = sv.MSSV;
                txtIdSV.Enabled = false;
                txtName.Text = sv.TenSV;
                cbbHP.SelectedItem = sv.HocPhan.TenHP;
                cbbLSH.SelectedItem= sv.LopSH;
                if (sv.Gender) rBtnFemale.Checked = true;
                else rBtnMale.Checked = true;
                dtpkNgayThi.Value = sv.NgayThi;
                txtDiemBT.Text = sv.DiemBT.ToString();
                txtDiemGK.Text = sv.DiemGK.ToString();
                txtDiemCK.Text = sv.DiemCK.ToString();
                SetTongKet();
            }
        }

        private void LoadCBBLopHP()
        {   
            this.cbbHP.Items.Clear();
            this.cbbHP.Items.AddRange(QLSV_BLL.Instance.GetLopHP().ToArray());
            this.cbbHP.SelectedIndex = 0;
        }

        private void LoadCBBLopSH()
        {
            this.cbbLSH.Items.Clear();
            this.cbbLSH.Items.AddRange(QLSV_BLL.Instance.GetLopSH().ToArray());
            this.cbbLSH.SelectedIndex = 0;
        }



        private void TongKetChange(object sender, EventArgs e)
        {
            SetTongKet();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtIdSV.Text == "" || txtName.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }
            var sv = new SinhVien
            {
               MSSV = txtIdSV.Text,
               TenSV = txtName.Text,
               LopSH = cbbLSH.SelectedItem.ToString(),
               MaHP= ((CBBItems)cbbHP.SelectedItem).Value,
               Gender = (rBtnFemale.Checked ? true : false),
               NgayThi = DateTime.Parse(dtpkNgayThi.Value.Date.ToString()),
               DiemBT = Convert.ToDouble(txtDiemBT.Text),
               DiemCK = Convert.ToDouble(txtDiemCK.Text),
               DiemGK = Convert.ToDouble(txtDiemGK.Text),
        };

            if (MSSV == null)
            {
                QLSV_BLL.Instance.AddSV(sv);
            }
            else
            {
                QLSV_BLL.Instance.UpdateSV(sv);
            }
            this.ShowData();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void SetTongKet()
        {
            double bt = 0, gk = 0, ck = 0;
            if (txtDiemBT.Text == "" || txtDiemGK.Text == "" || txtDiemCK.Text == "")
            {

            }
            else
            {
                if (txtDiemBT.Text != "") bt = Convert.ToDouble(txtDiemBT.Text);
                if (txtDiemGK.Text != "") gk = Convert.ToDouble(txtDiemGK.Text);
                if (txtDiemCK.Text != "") ck = Convert.ToDouble(txtDiemCK.Text);
            }

            txtTongKet.Text = (bt * 0.2 + gk * 0.3 + ck* 0.5).ToString();
            txtTongKet.Enabled = false;
        }
    }
}
