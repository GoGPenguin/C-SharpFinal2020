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
                txtDiemBT.Text = "0";
                txtDiemGK.Text = "0";
                txtDiemCK.Text = "0";
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
            }
            SetTongKet();
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


            double bt = (txtDiemBT.Text == "" || !isNumeric(txtDiemBT.Text)) ? 0 : Convert.ToDouble(txtDiemBT.Text);
            double gk = (txtDiemGK.Text == "" || !isNumeric(txtDiemGK.Text)) ? 0 : Convert.ToDouble(txtDiemGK.Text);
            double ck = (txtDiemCK.Text == "" || !isNumeric(txtDiemCK.Text)) ? 0 : Convert.ToDouble(txtDiemCK.Text);


            var sv = new SinhVien
            {
                MSSV = txtIdSV.Text,
                TenSV = txtName.Text,
                LopSH = cbbLSH.SelectedItem.ToString(),
                MaHP = ((CBBItems)cbbHP.SelectedItem).Value,
                Gender = (rBtnFemale.Checked ? true : false),
                NgayThi = DateTime.Parse(dtpkNgayThi.Value.Date.ToString()),
                DiemBT = bt,
                DiemCK = gk,
                DiemGK = ck,
            };

            if (MSSV == null)
            {   
                if (!checkExist(txtIdSV.Text, ((CBBItems)cbbHP.SelectedItem).Value.ToString()))
                {
                    QLSV_BLL.Instance.AddSV(sv);
                    this.ShowData();
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Không thể tồn tại nhiều hơn cùng 1 sinh viên trong danh sách học phần");
                }
            }
            else
            {
                QLSV_BLL.Instance.UpdateSV(sv);
                this.ShowData();
                this.Dispose();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void SetTongKet()
        {
            double bt = (txtDiemBT.Text == "" || !isNumeric(txtDiemBT.Text)) ? 0 : Convert.ToDouble(txtDiemBT.Text);
            double gk = (txtDiemGK.Text == "" || !isNumeric(txtDiemGK.Text)) ? 0 : Convert.ToDouble(txtDiemGK.Text);
            double ck = (txtDiemCK.Text == "" || !isNumeric(txtDiemCK.Text)) ? 0 : Convert.ToDouble(txtDiemCK.Text);

            txtTongKet.Text = (bt * 0.2 + gk * 0.3 + ck* 0.5).ToString();
            txtTongKet.Enabled = false;
        }

        public bool checkExist(string mssv, string mahp)
        {
            var data = QLSV_BLL.Instance.GetSVByID(mssv, mahp);
            if (data == null) return false;
            return true;
        }

        public bool isNumeric(string s)
        {
            return double.TryParse(s, out _);
        }
    }
}
