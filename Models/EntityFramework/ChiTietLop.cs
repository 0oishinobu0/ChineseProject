namespace Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietLop")]
    public partial class ChiTietLop
    {
        [Key]
        public long MaCTLOP { get; set; }

        public long MaHV { get; set; }

        public long MaLOP { get; set; }

        public virtual HocVien HocVien { get; set; }

        public virtual Lop Lop { get; set; }
    }
}
