using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Dao;
using Models.EntityFramework;
using Project.Models;

namespace Project.Controllers
{
    public class TrangChuController : Controller
    {
        public ActionResult Index() { return View(); }

        [HttpPost]
        public ActionResult Create(HocVienModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.soDienThoai.ToString().Length > 10)
                {
                    ModelState.AddModelError("", "Số điện thoại không hợp lệ");
                    return View("Index");
                }
                var daoDon = new DonDangKyDao();
                var daoHocVien = new HocVienDao();
                long idHocVien = daoHocVien.Them(model.ten, model.soDienThoai, model.email, model.coSo, model.hoTro);
                bool result = daoDon.Them(idHocVien);
                if (result)
                {
                    TempData["DangKy"] = "Đăng ký thành công";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Đăng ký không thành công");
            }
            return View("Index");
        }

        public ActionResult Test() { return View(); }
    }
}