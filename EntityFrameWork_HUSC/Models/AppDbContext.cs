using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameWork_HUSC.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conString = "Server=DESKTOP-OKEUGVN\\SQLEXPRESS;Database=QLLopHoc;User Id=sa;Password=123;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(conString);
        }

      
    }
    [Table("LopHoc")]
    public class LopHoc
    {
        [Key]
        public int MaLopHoc { get; set; }
        [StringLength(50)]
        public string TenLopHoc { get; set; }
        [StringLength(50)]
        public string PhongHoc { get; set; }
        public virtual List<SinhVien> ListSinhVien { get; set; }
    }
    [Table("SinhVien")]
    public class SinhVien
    {
        [Key]
        public int id { get; set; }
        [StringLength(50)]
        public string MaSinhVien {  get; set; } //Unique
        [StringLength(50)]
        public string Ho {  get; set; }
        [StringLength(50)]
        public string Ten {  get; set; }
        public DateTime? NgaySinh { get; set; }
        public EGioiTinh GioiTinh {  get; set; }

        public int MaLopHoc { get; set; }
        [ForeignKey(nameof(MaLopHoc))]
        public virtual LopHoc  LopHoc { get; set; }
    }
    public enum EGioiTinh
    {
        Nam, Nu, Khac
    }
    public class SinhVienMapper
    {
        public int id { get; set; }
        public string MaSinhVien { get; set; } //Unique
        public string Ho { get; set; }
        public string Ten { get; set; }
        public DateTime? NgaySinh { get; set; }
        public EGioiTinh GioiTinh { get; set; }
    }
}
