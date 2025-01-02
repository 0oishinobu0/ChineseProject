using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Models.EntityFramework
{
    public partial class ChineseDbContext : DbContext
    {
        public ChineseDbContext()
            : base("name=ChineseDbContext")
        {
        }

        public virtual DbSet<ChiTietLop> ChiTietLops { get; set; }
        public virtual DbSet<DonDangKy> DonDangKies { get; set; }
        public virtual DbSet<HocVien> HocViens { get; set; }
        public virtual DbSet<Lop> Lops { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HocVien>()
                .Property(e => e.SoDienThoaiHV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<HocVien>()
                .Property(e => e.EmailHV)
                .IsUnicode(false);

            modelBuilder.Entity<HocVien>()
                .HasMany(e => e.ChiTietLops)
                .WithRequired(e => e.HocVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HocVien>()
                .HasMany(e => e.DonDangKies)
                .WithRequired(e => e.HocVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lop>()
                .HasMany(e => e.ChiTietLops)
                .WithRequired(e => e.Lop)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TenTK)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhauTK)
                .IsUnicode(false);
        }
    }
}
