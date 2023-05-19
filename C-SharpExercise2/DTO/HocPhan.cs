using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_SharpExercise2.DTO
{
    public class HocPhan
    {
        public HocPhan() 
        {
            SinhViens = new HashSet<SinhVien>();
        }

        [Key]
        public string MaHP { get; set; }

        public string TenHP { get; set; }

        public virtual ICollection<SinhVien> SinhViens { get;}
    }
}
