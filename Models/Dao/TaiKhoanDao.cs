using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EntityFramework;

namespace Models.Dao
{
    public class TaiKhoanDao
    {
        ChineseDbContext db = null;

        public TaiKhoanDao() { db = new ChineseDbContext(); }

        public TaiKhoan LayTaiKhoan(string taiKhoan) { return db.TaiKhoans.SingleOrDefault(x => x.TenTK == taiKhoan); }

        public bool DangNhap(string taikhoan, string matkhau)
        {
            return db.TaiKhoans.Any(x => x.TenTK == taikhoan && x.MatKhauTK == matkhau);
        }
    }
}
