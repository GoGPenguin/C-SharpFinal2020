using C_SharpExercise2.DTO;
using System;
using System.Data.Entity;
using System.Linq;

namespace C_SharpExercise2
{
    public class QLSVFinal : DbContext
    {
       
        public QLSVFinal()
            : base("name=QLSVFinal")
        {
            Database.SetInitializer<QLSVFinal>(new CreateDB());
        }

        public virtual DbSet<SinhVien> SinhViens { get; set; }

        public virtual DbSet<HocPhan> HocPhans { get; set; }

    }


}