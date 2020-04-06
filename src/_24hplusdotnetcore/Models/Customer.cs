using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _24hplusdotnetcore.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MaKH { get; set; }
        [BsonRequired]
        public string UserName { get; set; }
        [BsonRequired]
        public int Status { get; set; }
        public string CMND_CCCD_CMQD { get; set; }
        public string HocVan { get; set; }
        public string NoiCapCMND_CCCD { get; set; }
        public string GioiTinh { get; set; }
        public string NgaySinh { get; set; }
        public string DoDienThoai { get; set; }
        public string TinhTrangHonNhan { get; set; }
        public DiaChiThuongTru ThuongTru { get; set; }
        public DiaChiTamTru TamTru { get; set; }
        public string LoaiHinhNhaO { get; set; }
        public string Email { get; set; }
        public string NgheNghiep { get; set; }
        public string ChucDanhCongViec { get; set; }
        public TruSoChinh TruSoChinh { get; set; }
        public NoiLamViecThucTe NoiLamViecThucTes { get; set; }
        public string MaSoThueCty { get; set; }
        public ThoiGianSong_Conngtac ThoiGianCongTac { get; set; }
        public string ThuNhap { get; set; }
        public string LoaiHDLD { get; set; }
        public string PhuongThucNhanLuong { get; set; }
        public string KhoangTraHangThangKhacTCTD { get; set; }
        public string MucDichVay { get; set; }
        public string ThoiGianVay { get; set; }
        public string LoaiSanPham { get; set; }
        public string TenSanPham { get; set; }
        public bool DongYMuaBaoHiemVay { get; set; }
        public string SoTienHanMucDeNghi { get; set; }
        public HopDongBHNT HopDongBHNT { get; set; }
        public string GiaTriBinhQuanHoaDonTienIch { get; set; }
        public string SoDuBinhQuanTK { get; set; }
        public string GiaXeTaiThoiDienVay { get; set; }
        public HopDongVayCu HopDongVayCu { get; set; }
        public ThongTinGiaDinh ThongTinGiaDinh { get; set; }
        public NguoiThamChieu NguoiThamChieu1 { get; set; }
        public NguoiThamChieu NguoiThamChieu2 { get; set; }
        public NguoiCCHangHoa_KHThuongXuyen NguoiCC1 { get; set; }
        public NguoiCCHangHoa_KHThuongXuyen NguoiCC2 { get; set; }
        public KyHopDongGiaiNgan KyHopDongGiaiNgan { get; set; }
        public ThongTinSale ThongTinSale { get; set; }
    }

    public class ThongTinSale
    {
        public string TenSale { get; set; }
        public string SaleCode { get; set; }
        public string SDT { get; set; }
        public string GhiChu { get; set; }
    }

    public class KyHopDongGiaiNgan
    {
        public string DiaDiemKyHD { get; set; }
        public string PhuongThucGianNgan { get; set; }
        public string TenNguoiThuHuong { get; set; }
        public string NganHangThuHuong { get; set; }
        public string PGD_CN { get; set; }
        public string STKThuHuong { get; set; }
    }

    public class NguoiCCHangHoa_KHThuongXuyen
    {
        public string HoTen { get; set; }
        public string SDT { get; set; }
    }

    public class NguoiThamChieu
    {
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string MoiQuanHeNVV { get; set; }
    }

    public class ThongTinGiaDinh
    {
        public string MoiQuanHe { get; set; }
        public string MoiQuanHeKhac { get; set; }
        public string HoTen { get; set; }
        public string NgaySinh { get; set; }
        public string CMND_CCCD { get; set; }
        public string SDT { get; set; }
        public string SoNguoiPhuThuoc { get; set; }
        public string ChoOHientai { get; set; }
        public bool GiongDiaChiNVV { get; set; }
    }

    public class HopDongVayCu
    {
        public string SoHDTD { get; set; }
        public string NgayBatDau { get; set; }
        public string KyHan { get; set; }
        public string GiaTriKhoangHangThang { get; set; }
    }

    public class DiaChi
    {
        public string Tinh_TP { get; set; }
        public string Quan_Huyen { get; set; }
        public string Phuong_Xa { get; set; }
        public string SoNha_TenDuong { get; set; }
    }
    public class DiaChiThuongTru : DiaChi
    {

    }
    public class DiaChiTamTru: DiaChi
    {
        public bool GiongDiaChiThuongTru { get; set; }
        public bool GiongDiaChiCongTac { get; set; }
        public ThoiGianSong_Conngtac ThoiGianSong { get; set; }

    }
    public class ThoiGianSong_Conngtac
    {
        public string SoNam { get; set; }
        public string SoThang { get; set; }
    }
    public class TruSoChinh : DiaChi
    {
        public string TenTruSoChinh { get; set; }
        public string DienThoaiTruSoChinh { get; set; }
    }
    public class NoiLamViecThucTe: DiaChi
    {
        public bool GiongDiaChiTruSoChinh { get; set; }
        public bool GiongSDTTruSuChinh { get; set; }
        public string SDTNoiLamViecThucTe { get; set; }

    }
    public class HopDongBHNT
    {
        public string TenCtyBH { get; set; }
        public TienPhiBH TienPhiBH { get; set; }
        public string DinhKyDongPhi { get; set; }

    }
    public class TienPhiBH 
    {
        public string SoTienDinhKy { get; set; }
        public string KyDongPhiKhac { get; set; }
    }
}
