namespace ChickenGang_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            BinhLuan = new HashSet<BinhLuan>();
            ChiTietDonDat = new HashSet<ChiTietDonDat>();
            ChiTietPhieuNhap = new HashSet<ChiTietPhieuNhap>();
        }

        [Key]
        [DisplayName("Mã sản phẩm")]
        public int MaSP { get; set; }

        [StringLength(255)]
        [DisplayName("Tên sản phẩm")]
        public string TenSP { get; set; }

        [DisplayName("Đơn giá")]
        public decimal? DonGia { get; set; }

        [DisplayName("Ngày cập nhật")]
        public DateTime? NgayCapNhat { get; set; }

        [DisplayName("Mô tả 1")]
        public string Mota { get; set; }

        [DisplayName("Mô tả 2")]
        public string Mota2 { get; set; }

        [DisplayName("Mô tả 3")]
        public string Mota3 { get; set; }

        [DisplayName("Hình ảnh")]
        public string HinhAnh { get; set; }

        [DisplayName("Số lượng tồn")]
        public int? SoLuongTon { get; set; }

        [DisplayName("Lượt xem")]
        public int? LuotXem { get; set; }

        [DisplayName("Lượt bình chọn")]
        public int? LuotBinhChon { get; set; }

        [DisplayName("Lượt bình luận")]
        public int? LuotBinhLuan { get; set; }

        [DisplayName("Số lần mua")]
        public int? SoLanMua { get; set; }

        public int? Moi { get; set; }

        [DisplayName("Mã nhà cung cấp")]
        public int? MaNCC { get; set; }

        [DisplayName("Mã nhà sản xuất")]
        public int? MaNSX { get; set; }

        [DisplayName("Mã loại sản phẩm")]
        public int? MaLoaiSP { get; set; }

        public int? DaXoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonDat> ChiTietDonDat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhap { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }

        public virtual NhaSanXuat NhaSanXuat { get; set; }
        public List<NhaCungCap> ListNCC = new List<NhaCungCap>();
        public List<NhaSanXuat> ListNSX = new List<NhaSanXuat>();
        public List<LoaiSanPham> ListLSP = new List<LoaiSanPham>();

    }
}
