using EntityFrameWork_HUSC.Models;
using EntityFrameWork_HUSC.Services;
using EntityFrameWork_HUSC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameWork_HUSC.Controllers
{
    public class LopHocController : Controller
    {
        private readonly LopHocService lopHocService;

        public LopHocController() 
        {
            lopHocService = new LopHocService();
        }
        public IActionResult Index()
        {
            var ds = lopHocService.GetList();
            LopHocViewModel model = new LopHocViewModel
            {
                listLopHoc = ds
            };
            return View(model);
        }

        public IActionResult TimKiem(string ten)
        {

            if (!string.IsNullOrEmpty(ten))
            {
                var ds = lopHocService.Search(ten);
                LopHocViewModel model = new LopHocViewModel
                {
                    listLopHoc = ds
                };
                TempData["Ten"] = ten;
                return View("Index",model);
            }
            return RedirectToAction("Index");

        }

        public IActionResult Xoa(int id)
        {
            var lh = lopHocService.Delete(id);
            if (lh)
            {
                TempData["Message"] = "Xóa lớp học thành công !";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Them()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Them(LopHoc model)
        {
            var lh = lopHocService.Insert(model);
            if (!lh)
            {
                ViewBag.Message = "Đã tồn tại lớp học này !";
                return View(model);
            }
            TempData["Message"] = "Thêm lớp học thành công !";
            return RedirectToAction("Index");
        }

        public IActionResult Sua(int id)
        {
            var lopHoc = lopHocService.GetById(id);
            return View(lopHoc);
        }

        [HttpPost]
        public IActionResult Sua(int id, LopHoc model)
         {
             var lh = lopHocService.Update(id, model);
            if (lh)
            {
                TempData["Message"] = "Chỉnh sửa lớp học thành công !";
            }
            
            return RedirectToAction("Index");
         }
    }
}
