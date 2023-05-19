using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_SharpExercise2.DTO
{
    public class SinhVien
    {
        [Key, Column(Order =0)]
        public string MSSV { get; set; }

        [Key, Column(Order = 1)]
        public string MaHP { get; set; }
        public string TenSV { get; set; }

        public string LopSH { get; set; }

        public bool Gender { get; set; }

        public double DiemBT { get; set; }

        public double DiemGK { get; set; }

        public double DiemCK { get; set; }

        public DateTime NgayThi { get; set; }

        [ForeignKey("MaHP")]
        public virtual HocPhan HocPhan { get; set; }
    }
}
