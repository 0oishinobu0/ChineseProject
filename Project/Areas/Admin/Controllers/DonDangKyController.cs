using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using Models.Dao;
using Models.EntityFramework;

namespace Project.Areas.Admin.Controllers
{
    public class DonDangKyController : BaseController
    {
        public ActionResult Index()
        {
            var dao = new DonDangKyDao();
            var danhSach = dao.DanhSachAll();
            return View(danhSach);
        }

        public ActionResult BelongTo(long id)
        {
            var dao = new HocVienDao();
            var chiTiet = dao.ChiTiet(id);
            return View(chiTiet);
        }

        [HttpPost]
        public ActionResult Deny(long id)
        {
            var dao = new DonDangKyDao();
            var result = dao.TuChoiDuyet(id);
            if (result) 
            {
                SetAlert("Từ chối duyệt đơn thành công", "success");
                return RedirectToAction("Index"); 
            }
            SetAlert("Từ chối duyệt đơn không thành công", "error");
            return View("Index", dao.DanhSachAll());
        }

        [HttpPost]
        public ActionResult Accept(long id)
        {
            var dao = new DonDangKyDao();
            var result = dao.Duyet(id);
            if (result) 
            {
                SetAlert("Duyệt đơn thành công", "success");
                return RedirectToAction("Index"); 
            }
            SetAlert("Duyệt đơn không thành công", "error");
            return View("Index", dao.DanhSachAll());
        }

        public ActionResult Export(string fileName)
        {
            var dao = new HocVienDao();
            var danhSach = dao.DanhSachDuyet();
            if (string.IsNullOrWhiteSpace(fileName)) { fileName = "DanhSachHocVienDaDuyet"; }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Danh sách đã duyệt");
                worksheet.Cell(1, 1).Value = "Mã học viên";
                worksheet.Cell(1, 2).Value = "Họ tên";
                worksheet.Cell(1, 3).Value = "Số điện thoại";
                worksheet.Cell(1, 4).Value = "Email";
                worksheet.Cell(1, 5).Value = "Cơ sở";

                var headerRange = worksheet.Range(1, 1, 1, 5);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                for (int i = 0; i < danhSach.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = danhSach[i].MaHV;
                    worksheet.Cell(i + 2, 2).Value = danhSach[i].TenHV;
                    worksheet.Cell(i + 2, 3).Value = danhSach[i].SoDienThoaiHV;
                    worksheet.Cell(i + 2, 4).Value = danhSach[i].EmailHV;
                    worksheet.Cell(i + 2, 5).Value = danhSach[i].CoSoHV;
                }

                var dataRange = worksheet.Range(2, 1, danhSach.Count + 1, 5);
                dataRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
                }
            }
        }
    }
}