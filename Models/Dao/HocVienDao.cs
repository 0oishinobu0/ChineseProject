using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EntityFramework;

namespace Models.Dao
{
    public class HocVienDao
    {
        ChineseDbContext db = null;

        public HocVienDao() { db = new ChineseDbContext(); }

        public List<HocVien> DanhSachAll() { return db.HocViens.ToList(); }

        public List<HocVien> DanhSachDuyet() { return db.HocViens.Where(x => x.TrangThaiHV == "Đã duyệt").ToList(); }

        public List<HocVien> DanhSachDuyetTheoNgay(DateTime? startDate, DateTime? endDate)
        {
            var danhSach = db.HocViens.AsNoTracking()
                                      .Join(db.DonDangKies, hocVien => hocVien.MaHV, don => don.MaHV, (hocVien, don) => new { hocVien, don })
                                      .Where(x => x.hocVien.TrangThaiHV == "Đã duyệt");
            if (startDate.HasValue)
            {
                var start = startDate.Value.Date;
                danhSach = danhSach.Where(x => x.don.NgayTaoDONDK >= start);
            }
            if (endDate.HasValue)
            {
                var end = endDate.Value.Date.AddDays(1);
                danhSach = danhSach.Where(x => x.don.NgayTaoDONDK < end);
            }

            var result = danhSach.Select(x => new
            {
                x.hocVien.MaHV,
                x.hocVien.TenHV,
                x.hocVien.SoDienThoaiHV,
                x.hocVien.EmailHV,
                x.hocVien.CoSoHV,
            }).ToList();

            return result.Select(x => new HocVien
            {
                MaHV = x.MaHV,
                TenHV = x.TenHV,
                SoDienThoaiHV = x.SoDienThoaiHV,
                EmailHV = x.EmailHV,
                CoSoHV = x.CoSoHV
            }).ToList();
        }

        public List<HocVien> DanhSachTheoLop(long id)
        {
            return db.HocViens.AsNoTracking()
                                      .Join(db.ChiTietLops, hocVien => hocVien.MaHV, chiTiet => chiTiet.MaHV, (hocVien, chiTiet) => new { hocVien, chiTiet })
                                      .Where(x => x.chiTiet.MaLOP == id)
                                      .Select(x => x.hocVien)
                                      .ToList();
        }

        public HocVien ChiTiet(long id) { return db.HocViens.Find(id); }

        public long Them(string ten, string soDienThoai, string email, string coSo, string hoTro)
        {
            try
            {
                var hocVien = new HocVien
                {
                    TenHV = ten,
                    SoDienThoaiHV = soDienThoai,
                    EmailHV = email,
                    CoSoHV = coSo,
                    HoTroHV = hoTro,
                    TrangThaiHV = "Đang chờ"
                };
                db.HocViens.Add(hocVien);
                db.SaveChanges();
                return hocVien.MaHV;
            }
            catch (Exception) { return 0; }
        }
    }
}
