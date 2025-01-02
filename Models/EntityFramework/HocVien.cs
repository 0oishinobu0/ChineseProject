namespace Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HocVien")]
    public partial class HocVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HocVien()
        {
            ChiTietLops = new HashSet<ChiTietLop>();
            DonDangKies = new HashSet<DonDangKy>();
        }

        [Key]
        public long MaHV { get; set; }

        [Required]
        [StringLength(100)]
        public string TenHV { get; set; }

        [Required]
        [StringLength(10)]
        public string SoDienThoaiHV { get; set; }

        [Required]
        [StringLength(255)]
        public string EmailHV { get; set; }

        [Required]
        [StringLength(50)]
        public string CoSoHV { get; set; }

        public string HoTroHV { get; set; }

        [Required]
        [StringLength(100)]
        public string TrangThaiHV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietLop> ChiTietLops { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonDangKy> DonDangKies { get; set; }
    }
}
