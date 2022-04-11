namespace ChickenGang_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDonDat")]
    public partial class ChiTietDonDat
    {
        [Key]
        public int MaChiTietDonDat { get; set; }

        public int? MaDDH { get; set; }

        public int? MaSP { get; set; }

        [StringLength(255)]
        public string TenSP { get; set; }

        public int? SoLuong { get; set; }

        public decimal? DonGia { get; set; }

        public virtual DonDatHang DonDatHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
