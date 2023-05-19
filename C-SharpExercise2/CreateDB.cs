using C_SharpExercise2.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_SharpExercise2
{
    public class CreateDB : DropCreateDatabaseAlways<QLSVFinal>
    {
        protected override void Seed(QLSVFinal context)
        {
            context.HocPhans.AddRange(new HocPhan[]
            {
                new HocPhan
                {
                    MaHP = "1",
                    TenHP = "C#",
                },
                new HocPhan
                {
                    MaHP = "2",
                    TenHP = "DSTT"
                }
            });

            context.SinhViens.AddRange(new SinhVien[]
            {
                new SinhVien
                {
                    MSSV= "1",
                    MaHP= "1",
                    TenSV= "Truong Son",
                    LopSH = "21T_DT",
                    Gender = false,
                    DiemBT = 9.0,
                    DiemGK = 9.0,
                    DiemCK = 9.5,
                    NgayThi = DateTime.Parse("06/03/2003"),
                },
                new SinhVien
                {
                    MSSV= "1",
                    MaHP= "2",
                    TenSV= "Truong Son",
                    LopSH = "21T_DT",
                    Gender = false,
                    DiemBT = 9.2,
                    DiemGK = 9.2,
                    DiemCK = 9.0,
                    NgayThi = DateTime.Parse("07/03/2003"),
                },
                new SinhVien
                {
                    MSSV= "2",
                    MaHP= "2",
                    TenSV= "Ngoc Hoai",
                    LopSH = "22PFIEV2",
                    Gender = true,
                    DiemBT = 10.0,
                    DiemGK = 10.0,
                    DiemCK = 10.0,
                    NgayThi = DateTime.Parse("08/10/2004"),
                },
            });
        }
    }
}
