using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office2010.Excel;
using Models.Dao;

namespace Project.Areas.Admin.Controllers
{
    public class HocVienController : BaseController
    {
        public ActionResult Index()
        {
            var dao = new HocVienDao();
            var danhSach = dao.DanhSachAll();
            return View(danhSach);
        }

        public ActionResult BelongTo(long id)
        {
            var dao = new LopDao();
            var danhSach = dao.DanhSachTheoHv(id);
            return View(danhSach);
        }

        public ActionResult ViewArrange(long id)
        {
            var dao = new LopDao();
            var danhSach = dao.DanhSachXepDoiLop(id);
            return View(danhSach);
        }

        [HttpPost]
        public ActionResult Arrange(long idHv, long idLop)
        {
            var dao = new LopDao();
            var result = dao.XepLop(idHv, idLop);
            if (result == 0)
            {
                SetAlert("Xếp lớp thành công", "success");
                return RedirectToAction("Index");
            }
            if (result == 1) { ModelState.AddModelError("", "Không thể xếp lớp"); }
            if (result == 2) { ModelState.AddModelError("", "Không thể xếp lớp, lớp đã đủ học viên"); }
            if (result == 3) { ModelState.AddModelError("", "Không thể xếp lớp, lớp đã học qua 2 buổi"); }
            return View("ViewArrange", dao.DanhSachXepDoiLop(idHv));
        }

        public ActionResult ViewChange(long idHv, long idLopCu)
        {
            var dao = new LopDao();
            var danhSach = dao.DanhSachXepDoiLop(idHv);
            return View(danhSach);
        }

        [HttpPost]
        public ActionResult Change(long idHv, long idLopCu, long idLopMoi)
        {
            var dao = new LopDao();
            var result = dao.DoiLop(idHv, idLopCu, idLopMoi);
            if (result == 0)
            {
                SetAlert("Đổi lớp thành công", "success");
                return RedirectToAction("BelongTo", new { id = idHv });
            }
            if (result == 1) { ModelState.AddModelError("", "Không thể đổi lớp"); }
            if (result == 2) { ModelState.AddModelError("", "Không thể đổi lớp, lớp đã đủ học viên"); }
            if (result == 3) { ModelState.AddModelError("", "Không thể đổi lớp, lớp đã học qua 2 buổi"); }
            if (result == 4) { ModelState.AddModelError("", "Không thể đổi lớp, lớp này không cùng bậc đào tạo với lớp cũ"); }
            return View("ViewChange", dao.DanhSachXepDoiLop(idHv));
        }
    }
}