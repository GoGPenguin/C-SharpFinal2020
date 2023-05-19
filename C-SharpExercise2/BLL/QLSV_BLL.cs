using C_SharpExercise2.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_SharpExercise2.BLL
{
    public class QLSV_BLL
    {
        private static QLSV_BLL instance;

        public static QLSV_BLL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QLSV_BLL();
                }
                return instance;
            }
            private set { }
        }

        private QLSVFinal db;

        private QLSV_BLL()
        {
            db = new QLSVFinal();
        }

        public List<CBBItems> GetLopHP()
        {
            List<CBBItems> list = new List<CBBItems>();


            foreach (var item in db.HocPhans.Distinct())
            {
                list.Add(new CBBItems
                {
                    Text = item.TenHP,
                    Value = item.MaHP,
                });
            }

            return list;
        }

        public List<CBBItems> GetLopSH()
        {
            List<CBBItems> list = new List<CBBItems >();

            foreach (var item in db.SinhViens.Select(p => p.LopSH).Distinct())
            {
                list.Add(new CBBItems
                {
                    Text = item,
                    Value = item,
                });
            }

            return list;
        }

        public SinhVien GetSVByID(string id, string id2)
        {
            return db.SinhViens.FirstOrDefault(p => p.MSSV == id && p.MaHP == id2);
        }

        public List<object> GetAllSV(string txtSearch = "", string cbb = "All", string sortType = "MSSV")
        {
            List<object> list = new List<object>();

            if (cbb == "All")
            {
                var data = db.SinhViens.Where(p => ((p.MSSV + p.TenSV).ToLower()).Contains(txtSearch))
                    .Select(p => new DGVItem
                    {
                        MSSV= p.MSSV,
                        TenSV = p.TenSV,
                        LopSH = p.LopSH,
                        MaHP = p.MaHP,
                        TenHP = p.HocPhan.TenHP,
                        DiemBT = p.DiemBT,
                        DiemGK= p.DiemGK,
                        DiemCK= p.DiemCK,
                        DiemTongKet = p.DiemBT * 0.2 + p.DiemGK * 0.3 + p.DiemCK * 0.5,
                        NgayThi = p.NgayThi,
                        Gender = p.Gender,
                    }).OrderBy(sortType).ToList();
                foreach (var item in data)
                {
                    list.Add(item);
                }
            }
            else
            {
                var data = db.SinhViens.Where(p =>  ((p.MSSV + p.TenSV).ToLower()).Contains(txtSearch) && p.HocPhan.TenHP.Contains(cbb))
                    .Select(p => new DGVItem
                    {
                        MSSV = p.MSSV,
                        TenSV = p.TenSV,
                        LopSH = p.LopSH,
                        MaHP = p.MaHP,
                        TenHP = p.HocPhan.TenHP,
                        DiemBT = p.DiemBT,
                        DiemGK = p.DiemGK,
                        DiemCK = p.DiemCK,
                        DiemTongKet = p.DiemBT * 0.2 + p.DiemGK * 0.3 + p.DiemCK * 0.5,
                        NgayThi = p.NgayThi,
                        Gender = p.Gender,
                    }).OrderBy(sortType).ToList();
                foreach (var item in data)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public void AddSV(SinhVien sv)
        {
            db.SinhViens.Add(sv);
            db.SaveChanges();
        }

        public void UpdateSV(SinhVien data)
        {
            var item = db.SinhViens.Find(data.MSSV, data.MaHP);
            item = data;
            db.SinhViens.AddOrUpdate(item);
            db.SaveChanges();
        }

        public void DeleteSV(string id1 = "", string id2 = "")
        {
            var item = db.SinhViens.Find(id1, id2);
            db.SinhViens.Remove(item);
            db.SaveChanges();
        }
    }
}
