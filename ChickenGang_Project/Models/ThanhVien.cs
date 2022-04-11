namespace ChickenGang_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThanhVien")]
    public partial class ThanhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThanhVien()
        {
            BinhLuan = new HashSet<BinhLuan>();
            KhachHang = new HashSet<KhachHang>();
        }

        [Key]
        [DisplayName("Mã Thành Viên")]
        public int MaThanhVien { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Hãy nhập tài khoản của bạn")]
        [DisplayName("Tài Khoản")]
        public string TaiKhoan { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Hãy nhập mật khẩu của bạn")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Mật khẩu phải từ 8 ký tự trở lên và chứa một ký tự hoa, một ký tự thường, một chữ số và một ký tự đặc biệt.")]
        [DisplayName("Mật Khẩu")]
        public string MatKhau { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Hãy nhập Họ và Tên của bạn")]
        [DisplayName("Họ và Tên")]
        public string HoTen { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Hãy nhập địa chỉ của bạn")]
        public string DiaChi { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Hãy nhập emaiil của bạn")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail Không hợp lệ")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [StringLength(11)]
        [Required(ErrorMessage = "Hãy nhập số điện thoại của bạn")]
        [DisplayName("Số Điện Thoại")]
        public string SoDienThoai { get; set; }

        [DisplayName("Câu Hỏi")]
        public string CauHoi { get; set; }

        [DisplayName("Câu Trả Lời")]
        public string CauTraLoi { get; set; }

        [DisplayName("Mã Loại Thành Viên")]
        public int? MaLoaiTV { get; set; }

        [StringLength(50)]
        public string LoaiAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachHang> KhachHang { get; set; }

        public virtual LoaiThanhVien LoaiThanhVien { get; set; }
    }
}
