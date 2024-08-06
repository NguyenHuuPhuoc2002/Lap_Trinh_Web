using EntityFrameWork_HUSC.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;

namespace EntityFrameWork_HUSC.Services
{
    public class LopHocService
    {
        AppDbContext db;
        public LopHocService()
        {
            if (db == null)
            {
                db = new AppDbContext();
            }
        }
        public List<LopHoc> GetList()
        {
            var ls = db.LopHocs.ToList();
            return ls;
        }
        public bool Insert(LopHoc lopHoc)
        {
            try
            {
                db.LopHocs.Add(lopHoc);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(int id, LopHoc lopHoc)
        {
            try
            {
                var ds = db.LopHocs.FirstOrDefault(p => p.MaLopHoc == id);
                ds.TenLopHoc = lopHoc.TenLopHoc;
                ds.PhongHoc = lopHoc.PhongHoc;
                var _lopHoc = db.LopHocs.Update(ds);
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
                var ds = db.LopHocs.FirstOrDefault(p => p.MaLopHoc == id);
                db.LopHocs.Remove(ds);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public LopHoc GetById(int id)
        {
            var _lopHoc = db.LopHocs.FirstOrDefault(p => p.MaLopHoc == id);
            return _lopHoc;
        }

        public List<LopHoc> Search(string key)
        {
            List<LopHoc> ds = new List<LopHoc>();

            if (!string.IsNullOrEmpty(key))
            {
                ds = db.LopHocs.Where(p => p.TenLopHoc.Contains(key) || p.PhongHoc.Contains(key)).ToList();
            }

            return ds;
        }


    }
}
