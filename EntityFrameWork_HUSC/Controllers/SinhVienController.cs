using EntityFrameWork_HUSC.Models;
using EntityFrameWork_HUSC.Services;
using EntityFrameWork_HUSC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EntityFrameWork_HUSC.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly SinhVienService sinhVienService;
        private readonly AppDbContext _context;

        public SinhVienController()
        {
            _context = new AppDbContext();
            sinhVienService = new SinhVienService();
        }
        public IActionResult Index(int malop)
        {
            var ds = new List<SinhVien>();
            if(malop != 0)
            {
                ds = sinhVienService.GetListSinhVienByMaLop(malop);
            }
            else
            {
                ds = sinhVienService.GetAll();
            }
          
            var sinhViens = new SinhVienViewModel
            {
                listSinhVien = ds
            };
            return View(sinhViens);
        }
        

        public IActionResult Xoa(int id)
        {
            var sv = sinhVienService.Delete(id);
            if (sv)
            {
                TempData["Message"] = "Xóa sinh viên thành công !";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Them()
        {
            ViewBag.MaLopHoc = new SelectList(_context.LopHocs, "MaLopHoc", "TenLopHoc");
            return View();
        }

        [HttpPost]
        public IActionResult Them(SinhVien model)
        {
            ViewBag.MaLopHoc = new SelectList(_context.LopHocs, "MaLopHoc", "TenLopHoc", model.id);
            var sv = sinhVienService.Insert(model);
            if (!sv)
            {
                ViewBag.Message = "Đã tồn tại sinh viên này !";
                return View(model);
            }
            TempData["Message"]= "Thêm sinh viên thành công !";
            return RedirectToAction("Index");
        }

        public IActionResult Sua(string id)
        {
            ViewBag.MaLopHoc = new SelectList(_context.LopHocs, "MaLopHoc", "TenLopHoc", id);
            var lopHoc = sinhVienService.GetByMaSinhVien(id);
            return View(lopHoc);
        }

        [HttpPost]
        public IActionResult Sua(SinhVien model)
        {
            var sv = sinhVienService.Update( model);
            if (sv){
                TempData["Message"] = "Chỉnh sửa sinh viên thành công !";
            }
            return RedirectToAction("Index");
        }

        public IActionResult TimKiem(string ten)
        {

            if (!string.IsNullOrEmpty(ten))
            {
                var ds = sinhVienService.Search(ten);
                SinhVienViewModel model = new SinhVienViewModel
                {
                    listSinhVien = ds
                };
                TempData["Ten"] = ten;
                return View("Index", model);
            }
            return RedirectToAction("Index");

        }
    }
}
