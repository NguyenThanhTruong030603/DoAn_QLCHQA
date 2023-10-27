using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QLBANHANG.Model1
{
    public partial class HangHoaContext : DbContext
    {
        public HangHoaContext()
            : base("name=HangHoaContext")
        {
        }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tb_CTHD> tb_CTHD { get; set; }
        public virtual DbSet<tb_HangHoa> tb_HangHoa { get; set; }
        public virtual DbSet<tb_HoaDon> tb_HoaDon { get; set; }
        public virtual DbSet<tb_KhachHang> tb_KhachHang { get; set; }
        public virtual DbSet<tb_NhanVien> tb_NhanVien { get; set; }
        public virtual DbSet<tb_TaiKhoan> tb_TaiKhoan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tb_CTHD>()
                .Property(e => e.MaHD)
                .IsUnicode(false);

            modelBuilder.Entity<tb_CTHD>()
                .Property(e => e.MaHH)
                .IsUnicode(false);

            modelBuilder.Entity<tb_HangHoa>()
                .Property(e => e.MaHang)
                .IsUnicode(false);

            modelBuilder.Entity<tb_HangHoa>()
                .HasMany(e => e.tb_CTHD)
                .WithRequired(e => e.tb_HangHoa)
                .HasForeignKey(e => e.MaHH);

            modelBuilder.Entity<tb_HoaDon>()
                .Property(e => e.MaHD)
                .IsUnicode(false);

            modelBuilder.Entity<tb_HoaDon>()
                .Property(e => e.NguoiLap)
                .IsUnicode(false);

            modelBuilder.Entity<tb_HoaDon>()
                .Property(e => e.KhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<tb_KhachHang>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<tb_KhachHang>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<tb_KhachHang>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tb_KhachHang>()
                .HasMany(e => e.tb_HoaDon)
                .WithOptional(e => e.tb_KhachHang)
                .HasForeignKey(e => e.KhachHang)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tb_NhanVien>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<tb_NhanVien>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<tb_NhanVien>()
                .HasMany(e => e.tb_HoaDon)
                .WithOptional(e => e.tb_NhanVien)
                .HasForeignKey(e => e.NguoiLap)
                .WillCascadeOnDelete();
        }
    }
}
