using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.EMMA;
using Models.Dao;
using Models.EntityFramework;
using Project.Areas.Admin.Models;
using Project.Common;

namespace Project.Areas.Admin.Controllers
{
    public class LopController : BaseController
    {
        public ActionResult Index()
        {
            var dao = new LopDao();
            var danhSach = dao.DanhSachAll();
            return View(danhSach);
        }

        public ActionResult Including(long id)
        {
            var dao = new HocVienDao();
            var danhSach = dao.DanhSachTheoLop(id);
            return View(danhSach);
        }

        public ActionResult ViewAdd() { return View(); }

        [HttpPost]
        public ActionResult Add(LopModel lop)
        {
            if (ModelState.IsValid)
            {
                var dao = new LopDao();
                var result = dao.Them(lop.tenLop, lop.maxHocVien, lop.thu, lop.soBuoi, lop.ngayStart, lop.bac);
                if (result == 0)
                {
                    SetAlert("Thêm lớp thành công", "success");
                    return RedirectToAction("Index");
                }
                if (result == 1) { ModelState.AddModelError("", "Không thể thêm lớp"); }
                if (result == 2) { ModelState.AddModelError("", "Không thể thêm lớp, số buổi không được nhỏ hơn 20"); }
                if (result == 3) { ModelState.AddModelError("", "Không thể thêm lớp, ngày bắt đầu đã qua rồi"); }
            }
            return View("ViewAdd");
        }

        public ActionResult ViewEdit(long id)
        {
            var dao = new LopDao();
            var lopDao = dao.ChiTiet(id);
            var model = new LopModel
            {
                id = lopDao.MaLOP,
                tenLop = lopDao.TenLOP,
                maxHocVien = lopDao.MaxHocVienLOP,
                thu = lopDao.ThuLOP,
                soBuoi = lopDao.SoBuoiLOP,
                ngayStart = lopDao.NgayStartLOP,
                bac = lopDao.BacLOP,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(LopModel lop)
        {
            if (ModelState.IsValid)
            {
                var dao = new LopDao();
                var result = dao.Sua(lop.id, lop.tenLop, lop.maxHocVien, lop.thu, lop.soBuoi, lop.ngayStart, lop.bac);
                if (result == 0) 
                {
                    SetAlert("Sửa lớp thành công", "success");
                    return RedirectToAction("Index"); 
                }
                if (result == 1) { ModelState.AddModelError("", "Không thể sửa lớp"); }
                if (result == 2) { ModelState.AddModelError("", "Không thể sửa lớp, số buổi không được nhỏ hơn 20"); }
                if (result == 3) { ModelState.AddModelError("", "Không thể sửa lớp, ngày bắt đầu đã qua rồi"); }
            }
            return View("ViewEdit", lop);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            var dao = new LopDao();
            var result = dao.Xoa(id);
            if (result)
            {
                SetAlert("Xóa lớp thành công", "success");
                return RedirectToAction("Index");
            }
            SetAlert("Xóa lớp không thành công", "error");
            return View("Index", dao.DanhSachAll());
        }
    }
}