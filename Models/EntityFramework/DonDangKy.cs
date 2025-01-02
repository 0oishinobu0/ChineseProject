namespace Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonDangKy")]
    public partial class DonDangKy
    {
        [Key]
        public long MaDONDK { get; set; }

        public DateTime NgayTaoDONDK { get; set; }

        public bool? TrangThaiDONDK { get; set; }

        public long MaHV { get; set; }

        public virtual HocVien HocVien { get; set; }
    }
}
