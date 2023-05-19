using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_SharpExercise2.DTO
{
    public class DGVItem
    {
        public string MSSV { get; set; }

        public string MaHP { get; set; }

        public string TenHP { get; set; }

        public string TenSV { get; set; }

        public string LopSH { get; set; }

        public bool Gender { get; set; }

        public double DiemBT { get; set; }

        public double DiemGK { get; set; }

        public double DiemCK { get; set; }

        public double DiemTongKet { get; set; }

        public DateTime NgayThi { get; set; }
    }
}
