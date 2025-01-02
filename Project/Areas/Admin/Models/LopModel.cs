using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Models.EntityFramework;

namespace Project.Areas.Admin.Models
{
    public class LopModel
    {
        public long id { get; set; }

        [Required(ErrorMessage = "Mời nhập tên lớp")]
        public string tenLop { get; set; }

        [Required(ErrorMessage = "Mời nhập số lượng học viên tối đa")]
        public int maxHocVien { get; set; }
        
        [Required(ErrorMessage = "Mời chọn thứ")]
        public string thu { get; set; }

        [Required(ErrorMessage = "Mời nhập số buổi học")]
        public int soBuoi { get; set; }

        [Required(ErrorMessage = "Mời nhập hoặc chọn ngày bắt đầu")]
        public DateTime ngayStart { get; set; }

        [Required(ErrorMessage = "Mời chọn bậc đào tạo")]
        public string bac { get; set; }
    }
}