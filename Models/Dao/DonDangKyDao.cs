using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EntityFramework;

namespace Models.Dao
{
    public class DonDangKyDao
    {
        ChineseDbContext db = null;

        public DonDangKyDao() { db = new ChineseDbContext(); }

        public List<DonDangKy> DanhSachAll() { return db.DonDangKies.ToList(); }

        public bool Them(long idHocVien)
        {
            try
            {
                if (idHocVien == 0) { return false; }
                db.DonDangKies.Add(new DonDangKy
                {
                    MaHV = idHocVien,
                    NgayTaoDONDK = DateTime.Now,
                });
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                db.HocViens.Remove(db.HocViens.Find(idHocVien));
                db.SaveChanges();
                return false;
            }
        }

        public bool TuChoiDuyet(long id)
        {
            try
            {
                var don = db.DonDangKies.Find(id);
                var hocVien = db.HocViens.Find(don.MaHV);
                don.TrangThaiDONDK = false;
                hocVien.TrangThaiHV = "Không duyệt";
                db.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool Duyet(long id)
        {
            try
            {
                var don = db.DonDangKies.Find(id);
                var hocVien = db.HocViens.Find(don.MaHV);
                don.TrangThaiDONDK = true;
                hocVien.TrangThaiHV = "Đã duyệt";
                db.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
    }
}
