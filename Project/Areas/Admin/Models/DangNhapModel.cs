using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Areas.Admin.Models
{
    public class DangNhapModel
    {
        [Required(ErrorMessage = "Mời nhập tài khoản")]
        public string taiKhoan { get; set; }

        [Required(ErrorMessage = "Mời nhập mật khẩu")]
        public string matKhau { get; set; }

        public bool ghiNhoMatKhau { get; set; }
    }
}