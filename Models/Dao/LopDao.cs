using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EntityFramework;

namespace Models.Dao
{
    public class LopDao
    {
        ChineseDbContext db = null;

        public LopDao() { db = new ChineseDbContext(); }

        public List<Lop> DanhSachAll() { return db.Lops.ToList(); }

        public Lop ChiTiet(long id) { return db.Lops.Find(id); }

        public List<Lop> DanhSachTheoHv(long id)
        {
            return db.Lops.AsNoTracking()
                          .Join(db.ChiTietLops, lop => lop.MaLOP, chiTiet => chiTiet.MaLOP, (lop, chiTiet) => new { lop, chiTiet })
                          .Where(x => x.chiTiet.MaHV == id)
                          .Select(x => x.lop)
                          .ToList();
        }

        public List<Lop> DanhSachXepDoiLop(long id)
        {
            var ketQua = db.HocViens.Where(hv => hv.MaHV == id)
                                    .SelectMany(hv => db.Lops, (hv, lop) => new { HocVien = hv, Lop = lop })
                                    .GroupJoin(db.ChiTietLops,
                                               x => new { x.HocVien.MaHV, x.Lop.MaLOP },
                                               chiTiet => new { chiTiet.MaHV, chiTiet.MaLOP },
                                               (x, chiTietGroup) => new { x.HocVien, x.Lop, ChiTietGroup = chiTietGroup })
                                    .SelectMany(x => x.ChiTietGroup.DefaultIfEmpty(),
                                               (x, chiTiet) => new
                                               {
                                                   MaChiTietLop = chiTiet != null ? chiTiet.MaLOP : (long?)null,
                                                   MaLop = x.Lop.MaLOP,
                                                   x.Lop.TenLOP,
                                                   x.Lop.MaxHocVienLOP,
                                                   x.Lop.ThuLOP,
                                                   x.Lop.SoBuoiLOP,
                                                   x.Lop.NgayStartLOP,
                                                   x.Lop.BacLOP
                                               })
                                    .ToList();

            return ketQua.Select(x => new Lop
            {
                MaLOP = x.MaLop,
                MaChiTietLOP = x.MaChiTietLop,
                TenLOP = x.TenLOP,
                MaxHocVienLOP = x.MaxHocVienLOP,
                ThuLOP = x.ThuLOP,
                SoBuoiLOP = x.SoBuoiLOP,
                NgayStartLOP = x.NgayStartLOP,
                BacLOP = x.BacLOP
            }).ToList();
        }

        public DateTime BuoiThu(DateTime ngayStart, string thuString, int buoiThu)
        {
            int thu = 0;
            switch (thuString)
            {
                case "Chủ Nhật": thu = 0; break;
                case "Thứ Hai": thu = 1; break;
                case "Thứ Ba": thu = 2; break;
                case "Thứ Tư": thu = 3; break;
                case "Thứ Năm": thu = 4; break;
                case "Thứ Sáu": thu = 5; break;
                case "Thứ Bảy": thu = 6; break;
            }
            int ngayDifference = (thu - (int)ngayStart.DayOfWeek + 7) % 7;
            if (ngayDifference == 0) ngayDifference = 7;
            return ngayStart.AddDays(ngayDifference + 7 * (buoiThu - 1));
        }

        public int XepLop(long idHv, long idLop)
        {
            try
            {
                var lop = db.Lops.Find(idLop);
                if (db.ChiTietLops.Count(x => x.MaLOP == lop.MaLOP) >= lop.MaxHocVienLOP) { return 2; }
                if (DateTime.Now > BuoiThu(lop.NgayStartLOP, lop.ThuLOP, 2)) { return 3; }
                db.ChiTietLops.Add(new ChiTietLop
                {
                    MaHV = idHv,
                    MaLOP = idLop
                });
                db.SaveChanges();
                return 0;
            }
            catch (Exception) { return 1; }
        }

        public int DoiLop(long idHv, long idLopCu, long idLopMoi)
        {
            try
            {
                var lopCu = db.Lops.Find(idLopCu);
                var lopMoi = db.Lops.Find(idLopMoi);
                if (db.ChiTietLops.Count(x => x.MaLOP == lopMoi.MaLOP) >= lopMoi.MaxHocVienLOP) { return 2; }
                if (DateTime.Now > BuoiThu(lopMoi.NgayStartLOP, lopMoi.ThuLOP, 2)) { return 3; }
                if (lopCu.BacLOP != lopMoi.BacLOP) { return 4; }
                db.ChiTietLops.SingleOrDefault(x => x.MaHV == idHv && x.MaLOP == idLopCu).MaLOP = idLopMoi;
                db.SaveChanges();
                return 0;
            }
            catch (Exception) { return 1; }
        }

        public int Them(string ten, int maxHocVien, string thu, int soBuoi, DateTime ngayStart, string bac)
        {
            try
            {
                if (soBuoi < 20) { return 2; }
                if (DateTime.Now > ngayStart) { return 3; }
                db.Lops.Add(new Lop
                {
                    TenLOP = ten,
                    MaxHocVienLOP = maxHocVien,
                    ThuLOP = thu,
                    SoBuoiLOP = soBuoi,
                    NgayStartLOP = ngayStart,
                    BacLOP = bac
                });
                db.SaveChanges();
                return 0;
            }
            catch (Exception) { return 1; }
        }

        public int Sua(long id, string ten, int maxHocVien, string thu, int soBuoi, DateTime ngayStart, string bac)
        {
            try
            {
                if (soBuoi < 20) { return 2; }
                if (DateTime.Now > ngayStart) { return 3; }
                var lop = db.Lops.Find(id);
                lop.TenLOP = ten;
                lop.MaxHocVienLOP = maxHocVien;
                lop.ThuLOP = thu;
                lop.SoBuoiLOP = soBuoi;
                lop.NgayStartLOP = ngayStart;
                lop.BacLOP = bac;
                db.SaveChanges();
                return 0;
            }
            catch (Exception) { return 1; }
        }

        public bool Xoa(long id)
        {
            try
            {
                db.Lops.Remove(db.Lops.Find(id));
                db.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
    }
}
