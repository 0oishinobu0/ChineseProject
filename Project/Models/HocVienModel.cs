using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class HocVienModel
    {
        [Required(ErrorMessage = "Mời nhập họ tên")]
        public string ten { get; set; }
        
        [Required(ErrorMessage = "Mời nhập số điện thoại")]
        public string soDienThoai { get; set; }

        [Required(ErrorMessage = "Mời nhập email")]
        public string email { get; set; }
        
        [Required(ErrorMessage = "Mời chọn sơ cở")]
        public string coSo { get; set; }

        public string hoTro { get; set; }

        public string trangThai { get; set; }

    }
}