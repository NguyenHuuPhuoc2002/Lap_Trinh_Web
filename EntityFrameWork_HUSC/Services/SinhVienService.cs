using EntityFrameWork_HUSC.Models;

namespace EntityFrameWork_HUSC.Services
{
    public class SinhVienService
    {
        AppDbContext db;
        public SinhVienService()
        {
            if (db == null)
            {
                db = new AppDbContext();
            }
        }

        public List<SinhVien> GetListSinhVienByMaLop(int malop)
        {
            var ls = db.SinhViens.Where(p => p.MaLopHoc == malop).ToList();
            return ls;
        }

        public List<SinhVien> GetAll()
        {
            var ls = db.SinhViens.ToList();
            return ls;
        }
        public SinhVien GetByMaSinhVien(string id )
        {
            var ls = db.SinhViens.FirstOrDefault(p => p.MaSinhVien == id);
            return ls;
        }
        
        public bool Insert (SinhVien entity)
        {
            try
            {
                var sv = db.SinhViens.Where(p => p.MaSinhVien == entity.MaSinhVien).FirstOrDefault();
                if(sv != null)
                {
                    return false;
                }
                db.SinhViens.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(SinhVien entity)
        {
            try
            {
                var ds = db.SinhViens.FirstOrDefault(p => p.MaSinhVien == entity.MaSinhVien);
                ds.MaSinhVien = entity.MaSinhVien;
                ds.Ho = entity.Ho;
                ds.Ten = entity.Ten;
                ds.NgaySinh = entity.NgaySinh;
                ds.GioiTinh = entity.GioiTinh;
                ds.MaLopHoc = entity.MaLopHoc;
                var _sinhVien = db.SinhViens.Update(ds);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var sv = db.SinhViens.FirstOrDefault(p => p.id == id);
                db.SinhViens.Remove(sv);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<SinhVien> Search(string key)
        {
            List<SinhVien> ds = new List<SinhVien>();

            if (!string.IsNullOrEmpty(key))
            {
                ds = db.SinhViens.Where(p => p.MaSinhVien.Contains(key) 
                                || p.Ten.ToLower().Contains(key.ToLower().Trim()) 
                                || p.Ho.ToLower().Contains(key.ToLower().Trim())).ToList();
            }

            return ds;
        }
    }
}
