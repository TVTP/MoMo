create database Web_QuanLyBanHang
go
use Web_QuanLyBanHang
drop database Web_QuanLyBanHang
go

create table NhaSanXuat (
	MaNSX int not null Identity primary key,
	TenNSX nvarchar(100),
	ThongTin nvarchar(255),
	Logo nvarchar(max)
)
go

Create table NhaCungCap (
	MaNCC int not null identity primary key,
	TenNCC nvarchar(100),
	DiaChi nvarchar(255),
	Email nvarchar(255),
	SoDienThoai varchar(11),
	Fax nvarchar(50),
)
go

create table PhieuNhap (
	MaPN int not null identity primary key,
	MaNCC int,
	NgayNhap datetime,
	DaXoa bit,
	foreign key (MaNCC) references NhaCungCap(MaNCC) on delete cascade
)
go

create table LoaiThanhVien (
	MaLoaiTV int not null identity primary key,
	TenLoai nvarchar(50),
	UuDai int
)
go

create table ThanhVien
(
	MaThanhVien int primary key identity,
	TaiKhoan nvarchar(100),
	MatKhau nvarchar(100),
	HoTen nvarchar(100),
	DiaChi nvarchar(255),
	Email nvarchar(100),
	SoDienThoai varchar(11),
	CauHoi nvarchar(max),
	CauTraLoi nvarchar(max),
	MaLoaiTV int,
	LoaiAccount nvarchar(50),
	foreign key (MaLoaiTV) references LoaiThanhVien(MaLoaiTV) on delete cascade
)
go

create table DonDatHang (
	MaDDH int identity primary key,
	NgayDat datetime,
	TinhTrangGiaoHang bit,
	NgayGiao datetime,
	DaThanhToan bit,
	MaKH int, 
	UuDai int,
	DiaChiGiaoHang nvarchar(MaX),
	Sdt int ,
	foreign key (MaKH) references KhachHang(MaKH) on delete cascade,
)

create table KhachHang(
	MaKH int identity primary key,
	TenKH nvarchar(100),
	DiaChi nvarchar(100),
	Email nvarchar(255),
	SoDienThoai varchar(11),
	MaThanhVien int,
	foreign key (MaThanhVien) references ThanhVien(MaThanhVien) on delete cascade
)
go

create table LoaiSanPham (
	MaLoaiSP int primary key identity,
	TenLoai nvarchar(100),
	Icon nvarchar(max),
	BiDanh nvarchar(50),
)
go

create table SanPham(
	MaSP int identity primary key,
	TenSP nvarchar(255),
	DonGia decimal(18,0),
	NgayCapNhat datetime,
	Mota nvarchar(max),
	Mota2 nvarchar(max),
	Mota3 nvarchar(max),
	HinhAnh nvarchar(max),
	SoLuongTon int,
	LuotXem int,
	LuotBinhChon int,
	LuotBinhLuan int,
	SoLanMua int,
	Moi int,
	MaNCC int, 
	MaNSX int,
	MaLoaiSP int,
	DaXoa int
	foreign key (MaNCC) references NhaCungCap(MaNCC),
	foreign key (MaNSX) references NhaSanXuat(MaNSX),
	foreign key (MaLoaiSP) references LoaiSanPham(MaLoaiSP), 
)
go

create table ChiTietPhieuNhap (
	MaChiTiet int identity primary key,
	MaPN int,
	MaSP int,
	DonGiaNhap decimal(18,0),
	SoLuongNhap int,
	foreign key (MaSP) references SanPham(MaSP),
	foreign key (MaPN) references PhieuNhap(MaPN),
)
go


go

create table ChiTietDonDat (
	MaChiTietDonDat int identity primary key,
	MaDDH int,
	MaSP int, 
	TenSP nvarchar(255),
	SoLuong int,
	DonGia decimal(18,0),
	foreign key (MaSP) references SanPham(MaSP) on delete cascade,
	foreign key (MaDDH) references DonDatHang(MaDDH) on delete cascade,
)
go

create table BinhLuan (
	MaBinhLuan int identity primary key,
	NoiDung nvarchar(max),
	MaThanhVien int,
	MaSP int,
	foreign key (MaSP) references SanPham(MaSP) on delete cascade,
	foreign key (MaThanhVien) references ThanhVien(MaThanhVien) on delete cascade,
)
go

drop table SanPham
go

drop table NhaCungCap
go

drop table BinhLuan
go

drop table ChiTietDonDat
go

drop table ChiTietPhieuNhap
go
drop table KhachHang
drop table ThanhVien
drop table LoaiThanhVien