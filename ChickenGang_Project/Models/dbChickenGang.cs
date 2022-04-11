using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ChickenGang_Project.Models
{
    public partial class dbChickenGang : DbContext
    {
        public dbChickenGang()
            : base("name=dbChickenGang7")
        {
        }

        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<ChiTietDonDat> ChiTietDonDats { get; set; }
        public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public virtual DbSet<DonDatHang> DonDatHangs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<LoaiThanhVien> LoaiThanhViens { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; }
        public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<ThanhVien> ThanhViens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDonDat>()
                .Property(e => e.DonGia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietPhieuNhap>()
                .Property(e => e.DonGiaNhap)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DonDatHang>()
                .Property(e => e.Sdt)
                .IsUnicode(false);

            modelBuilder.Entity<DonDatHang>()
                .HasMany(e => e.ChiTietDonDat)
                .WithOptional(e => e.DonDatHang)
                .WillCascadeOnDelete();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.DonDatHang)
                .WithOptional(e => e.KhachHang)
                .WillCascadeOnDelete();

            modelBuilder.Entity<LoaiThanhVien>()
                .HasMany(e => e.ThanhVien)
                .WithOptional(e => e.LoaiThanhVien)
                .WillCascadeOnDelete();

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap>()
                .HasMany(e => e.PhieuNhap)
                .WithOptional(e => e.NhaCungCap)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SanPham>()
                .Property(e => e.DonGia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.BinhLuan)
                .WithOptional(e => e.SanPham)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietDonDat)
                .WithOptional(e => e.SanPham)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ThanhVien>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhVien>()
                .HasMany(e => e.BinhLuan)
                .WithOptional(e => e.ThanhVien)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ThanhVien>()
                .HasMany(e => e.KhachHang)
                .WithOptional(e => e.ThanhVien)
                .WillCascadeOnDelete();
        }
    }
}
