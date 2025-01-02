namespace Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Lop")]
    public partial class Lop
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lop()
        {
            ChiTietLops = new HashSet<ChiTietLop>();
        }

        [Key]
        public long MaLOP { get; set; }

        [NotMapped]
        public long? MaChiTietLOP { get; set; }

        [Required]
        [StringLength(100)]
        public string TenLOP { get; set; }

        public int MaxHocVienLOP { get; set; }

        [Required]
        [StringLength(8)]
        public string ThuLOP { get; set; }

        public int SoBuoiLOP { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayStartLOP { get; set; }

        [Required]
        [StringLength(10)]
        public string BacLOP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietLop> ChiTietLops { get; set; }
    }
}
