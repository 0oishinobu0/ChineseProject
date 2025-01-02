using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Models;
using Models.Dao;
using Models.EntityFramework;
using Project.Areas.Admin.Models;
using Project.Common;

namespace Project.Areas.Admin.Controllers
{
    public class DangNhapController : Controller
    {
        public ActionResult Index() { return View(); }

        [HttpPost]
        public ActionResult DangNhap(DangNhapModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new TaiKhoanDao();
                var result = dao.DangNhap(model.taiKhoan, MaHoa.MaHoaMD5(model.matKhau));
                if (result)
                {
                    var user = dao.LayTaiKhoan(model.taiKhoan);
                    var userSession = new TaiKhoanDangNhap();
                    userSession.taiKhoan = user.TenTK;
                    Session.Add(HangSo.USER_SESSION, userSession);
                    return RedirectToAction("Index", "DonDangKy");
                }
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
            }
            return View("Index");
        }

        public ActionResult DangXuat()
        {
            Session.Remove(HangSo.USER_SESSION);
            return RedirectToAction("Index");
        }
    }
}