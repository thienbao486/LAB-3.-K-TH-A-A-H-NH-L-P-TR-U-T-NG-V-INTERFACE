using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab03_OOPAdvanced
{
    // ========================= BAI 1-2: KE THUA, BASE, VIRTUAL/OVERRIDE =========================
    public class Nguoi
    {
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }

        public Nguoi(string hoTen, DateTime ngaySinh)
        {
            HoTen = hoTen;
            NgaySinh = ngaySinh;
        }

        public virtual string MoTa()
        {
            return $"{HoTen} - {NgaySinh:dd/MM/yyyy}";
        }
    }

    // ========================= INTERFACE BAI 4 =========================
    public interface IValidatable
    {
        bool IsValid(out string message);
    }

    public interface IPrintable
    {
        string ToDisplayText();
    }

    public class SinhVien : Nguoi, IValidatable, IPrintable
    {
        public string MaSV { get; set; }
        public double DiemTB { get; set; }

        public SinhVien(string ma, string ten, DateTime ngaySinh, double diem)
            : base(ten, ngaySinh)
        {
            MaSV = ma;
            DiemTB = diem;
        }

        public override string MoTa()
        {
            return base.MoTa() + $" - {MaSV} - {DiemTB:N2}";
        }

        public bool IsValid(out string message)
        {
            if (string.IsNullOrWhiteSpace(MaSV))
            {
                message = "Thiếu mã sinh viên.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(HoTen))
            {
                message = "Thiếu họ tên.";
                return false;
            }

            if (DiemTB < 0 || DiemTB > 10)
            {
                message = "Điểm trung bình phải nằm trong khoảng 0 đến 10.";
                return false;
            }

            message = "";
            return true;
        }

        public string ToDisplayText()
        {
            return $"{MaSV} - {HoTen}";
        }
    }

    // ========================= BAI 3 VA 5: LOP TRUU TUONG, DA HINH =========================
    public abstract class NguoiHoc
    {
        public string HoTen { get; set; } = "";
        public int SoTinChi { get; set; }

        public abstract decimal TinhHocPhi();
    }

    public class SinhVienChinhQuy : NguoiHoc
    {
        public override decimal TinhHocPhi()
        {
            return SoTinChi * 450000m;
        }
    }

    public class SinhVienLienThong : NguoiHoc
    {
        public override decimal TinhHocPhi()
        {
            return SoTinChi * 520000m;
        }
    }

    // ========================= BAI 6: HE THONG NHAN VIEN =========================
    public abstract class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }

        protected NhanVien(string maNV, string hoTen)
        {
            MaNV = maNV;
            HoTen = hoTen;
        }

        public abstract decimal TinhLuong();

        public virtual string MoTa()
        {
            return $"{MaNV} - {HoTen}";
        }
    }

    public class NhanVienVanPhong : NhanVien
    {
        public decimal LuongCoBan { get; set; }
        public decimal PhuCap { get; set; }

        public NhanVienVanPhong(string ma, string ten, decimal luong, decimal phuCap)
            : base(ma, ten)
        {
            LuongCoBan = luong;
            PhuCap = phuCap;
        }

        public override decimal TinhLuong() => LuongCoBan + PhuCap;
    }

    public class NhanVienKinhDoanh : NhanVien
    {
        public decimal LuongCoBan { get; set; }
        public decimal DoanhSo { get; set; }
        public decimal TyLeHoaHong { get; set; }

        public NhanVienKinhDoanh(string ma, string ten, decimal luong, decimal doanhSo, decimal tyLeHoaHong)
            : base(ma, ten)
        {
            LuongCoBan = luong;
            DoanhSo = doanhSo;
            TyLeHoaHong = tyLeHoaHong;
        }

        public override decimal TinhLuong() => LuongCoBan + DoanhSo * TyLeHoaHong;
    }

    public class GiangVien : NhanVien
    {
        public int SoTietDay { get; set; }
        public decimal TienMoiTiet { get; set; }

        public GiangVien(string ma, string ten, int soTiet, decimal tienMoiTiet)
            : base(ma, ten)
        {
            SoTietDay = soTiet;
            TienMoiTiet = tienMoiTiet;
        }

        public override decimal TinhLuong() => SoTietDay * TienMoiTiet;
    }

    // ========================= BAI 7: QUAN LY PHUONG TIEN =========================
    public interface IMove
    {
        double TinhTocDo();
        decimal TinhChiPhiVanHanh(double soKm);
        string LayThongTin();
    }

    public class XeMay : IMove
    {
        public string Ten { get; set; }
        public double QuangDuongKm { get; set; }
        public double ThoiGianGio { get; set; }
        public decimal ChiPhiMoiKm { get; set; }

        public XeMay(string ten, double quangDuong, double thoiGian, decimal chiPhiMoiKm)
        {
            Ten = ten; QuangDuongKm = quangDuong; ThoiGianGio = thoiGian; ChiPhiMoiKm = chiPhiMoiKm;
        }

        public double TinhTocDo() => ThoiGianGio <= 0 ? 0 : QuangDuongKm / ThoiGianGio;
        public decimal TinhChiPhiVanHanh(double soKm) => (decimal)Math.Max(0, soKm) * ChiPhiMoiKm;
        public string LayThongTin() => $"Xe máy {Ten}";
    }

    public class OTo : IMove
    {
        public string Ten { get; set; }
        public double QuangDuongKm { get; set; }
        public double ThoiGianGio { get; set; }
        public decimal ChiPhiMoiKm { get; set; }

        public OTo(string ten, double quangDuong, double thoiGian, decimal chiPhiMoiKm)
        {
            Ten = ten; QuangDuongKm = quangDuong; ThoiGianGio = thoiGian; ChiPhiMoiKm = chiPhiMoiKm;
        }

        public double TinhTocDo() => ThoiGianGio <= 0 ? 0 : QuangDuongKm / ThoiGianGio;
        public decimal TinhChiPhiVanHanh(double soKm) => (decimal)Math.Max(0, soKm) * ChiPhiMoiKm;
        public string LayThongTin() => $"Ô tô {Ten}";
    }

    public class XeDap : IMove
    {
        public string Ten { get; set; }
        public double QuangDuongKm { get; set; }
        public double ThoiGianGio { get; set; }
        public decimal ChiPhiMoiKm { get; set; }

        public XeDap(string ten, double quangDuong, double thoiGian, decimal chiPhiMoiKm)
        {
            Ten = ten; QuangDuongKm = quangDuong; ThoiGianGio = thoiGian; ChiPhiMoiKm = chiPhiMoiKm;
        }

        public double TinhTocDo() => ThoiGianGio <= 0 ? 0 : QuangDuongKm / ThoiGianGio;
        public decimal TinhChiPhiVanHanh(double soKm) => (decimal)Math.Max(0, soKm) * ChiPhiMoiKm;
        public string LayThongTin() => $"Xe đạp {Ten}";
    }

    // ========================= BAI 8: QUAN LY TAI LIEU THU VIEN =========================
    public abstract class TaiLieu
    {
        public string MaTaiLieu { get; set; }
        public string TieuDe { get; set; }

        protected TaiLieu(string ma, string tieuDe)
        {
            MaTaiLieu = ma; TieuDe = tieuDe;
        }

        public abstract string InThongTin();
    }

    public class Sach : TaiLieu
    {
        public string TacGia { get; set; }
        public Sach(string ma, string tieuDe, string tacGia) : base(ma, tieuDe) => TacGia = tacGia;
        public override string InThongTin() => $"[Sách] {MaTaiLieu} | {TieuDe} | Tác giả: {TacGia}";
    }

    public class TapChi : TaiLieu
    {
        public int SoPhatHanh { get; set; }
        public TapChi(string ma, string tieuDe, int soPhatHanh) : base(ma, tieuDe) => SoPhatHanh = soPhatHanh;
        public override string InThongTin() => $"[Tạp chí] {MaTaiLieu} | {TieuDe} | Số phát hành: {SoPhatHanh}";
    }

    public class LuanVan : TaiLieu
    {
        public string TacGia { get; set; }
        public string ChuyenNganh { get; set; }
        public LuanVan(string ma, string tieuDe, string tacGia, string chuyenNganh) : base(ma, tieuDe)
        {
            TacGia = tacGia; ChuyenNganh = chuyenNganh;
        }
        public override string InThongTin() => $"[Luận văn] {MaTaiLieu} | {TieuDe} | Tác giả: {TacGia} | Ngành: {ChuyenNganh}";
    }

    // ========================= BAI 9: HE THONG THANH TOAN =========================
    public interface IPayment
    {
        bool KiemTra(decimal soTien, out string thongBao);
        string ThanhToan(decimal soTien);
    }

    public class ThanhToanTienMat : IPayment
    {
        public bool KiemTra(decimal soTien, out string thongBao)
        {
            thongBao = soTien > 0 ? "Số tiền hợp lệ." : "Số tiền phải lớn hơn 0.";
            return soTien > 0;
        }
        public string ThanhToan(decimal soTien) => $"Đã ghi nhận thanh toán tiền mặt: {soTien:N0} VNĐ.";
    }

    public class ChuyenKhoan : IPayment
    {
        public bool KiemTra(decimal soTien, out string thongBao)
        {
            thongBao = soTien > 0 ? "Số tiền hợp lệ." : "Số tiền phải lớn hơn 0.";
            return soTien > 0;
        }
        public string ThanhToan(decimal soTien) => $"Đã ghi nhận thanh toán chuyển khoản: {soTien:N0} VNĐ.";
    }

    public class ViDienTu : IPayment
    {
        public bool KiemTra(decimal soTien, out string thongBao)
        {
            thongBao = soTien > 0 ? "Số tiền hợp lệ." : "Số tiền phải lớn hơn 0.";
            return soTien > 0;
        }
        public string ThanhToan(decimal soTien) => $"Đã ghi nhận thanh toán ví điện tử: {soTien:N0} VNĐ.";
    }

    // ========================= BAI 10: NANG CAP QUAN LY SINH VIEN =========================
    public abstract class SinhVienTheoLoai : IPrintable, IValidatable
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public double DiemTB { get; set; }

        protected SinhVienTheoLoai(string ma, string ten, double diem)
        {
            MaSV = ma; HoTen = ten; DiemTB = diem;
        }

        public abstract string LoaiSinhVien();

        public bool IsValid(out string message)
        {
            if (string.IsNullOrWhiteSpace(MaSV)) { message = "Thiếu mã sinh viên."; return false; }
            if (string.IsNullOrWhiteSpace(HoTen)) { message = "Thiếu họ tên."; return false; }
            if (DiemTB < 0 || DiemTB > 10) { message = "Điểm phải từ 0 đến 10."; return false; }
            message = "";
            return true;
        }

        public string ToDisplayText() => $"{LoaiSinhVien()} | {MaSV} | {HoTen} | Điểm: {DiemTB:N2}";
    }

    public class SinhVienChinhQuyThongTin : SinhVienTheoLoai
    {
        public SinhVienChinhQuyThongTin(string ma, string ten, double diem) : base(ma, ten, diem) { }
        public override string LoaiSinhVien() => "Chính quy";
    }

    public class SinhVienLienThongThongTin : SinhVienTheoLoai
    {
        public SinhVienLienThongThongTin(string ma, string ten, double diem) : base(ma, ten, diem) { }
        public override string LoaiSinhVien() => "Liên thông";
    }

    // ========================= CHUONG TRINH MENU =========================
    internal class Program
    {
        private static void Main()
        {
            // Cau hinh ma hoa UTF-8 de nhap/xuat tieng Viet co dau tren Console.
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine("\n========== LAB 3 - OOP NÂNG CAO ==========");
                Console.WriteLine("1. Kế thừa Nguoi - SinhVien");
                Console.WriteLine("2. Ghi đè phương thức MoTa()");
                Console.WriteLine("3. Lớp trừu tượng NguoiHoc và tính học phí");
                Console.WriteLine("4. Interface kiểm tra và in dữ liệu");
                Console.WriteLine("5. Danh sách đa hình tính tổng học phí");
                Console.WriteLine("6. Hệ thống nhân viên");
                Console.WriteLine("7. Quản lý phương tiện");
                Console.WriteLine("8. Quản lý tài liệu thư viện");
                Console.WriteLine("9. Hệ thống thanh toán");
                Console.WriteLine("10. Nâng cấp quản lý sinh viên");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn bài: ");
                string chon = Console.ReadLine() ?? "";

                try
                {
                    switch (chon)
                    {
                        case "1": Bai1(); break;
                        case "2": Bai2(); break;
                        case "3": Bai3(); break;
                        case "4": Bai4(); break;
                        case "5": Bai5(); break;
                        case "6": Bai6(); break;
                        case "7": Bai7(); break;
                        case "8": Bai8(); break;
                        case "9": Bai9(); break;
                        case "10": Bai10(); break;
                        case "0": return;
                        default: Console.WriteLine("Lựa chọn không hợp lệ."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Có lỗi: " + ex.Message);
                }
                Console.WriteLine("\nNhấn Enter để quay lại menu...");
                Console.ReadLine();
            }
        }

        private static void Bai1()
        {
            SinhVien sv = new SinhVien("SV001", "Nguyễn An", new DateTime(2005, 5, 12), 8.2);
            Console.WriteLine("Đã tạo sinh viên kế thừa từ Nguoi:");
            Console.WriteLine(sv.MoTa());
        }

        private static void Bai2()
        {
            Nguoi nguoi = new Nguoi("Trần Bình", new DateTime(2004, 3, 21));
            Nguoi sinhVien = new SinhVien("SV002", "Lê Hoa", new DateTime(2005, 8, 9), 9.1);
            Console.WriteLine("Đối tượng Nguoi: " + nguoi.MoTa());
            Console.WriteLine("Tham chiếu Nguoi nhưng đối tượng SinhVien (đa hình): " + sinhVien.MoTa());
        }

        private static void Bai3()
        {
            NguoiHoc sv1 = new SinhVienChinhQuy { HoTen = "Nguyễn An", SoTinChi = 18 };
            NguoiHoc sv2 = new SinhVienLienThong { HoTen = "Trần Bình", SoTinChi = 15 };
            Console.WriteLine($"{sv1.HoTen}: {sv1.TinhHocPhi():N0} VNĐ");
            Console.WriteLine($"{sv2.HoTen}: {sv2.TinhHocPhi():N0} VNĐ");
        }

        private static void Bai4()
        {
            SinhVien sv = new SinhVien("SV003", "Phạm Minh", new DateTime(2005, 1, 1), 7.5);
            if (sv.IsValid(out string message))
                Console.WriteLine("Dữ liệu hợp lệ: " + sv.ToDisplayText());
            else
                Console.WriteLine("Dữ liệu không hợp lệ: " + message);

            SinhVien svSai = new SinhVien("", "Bạn A", new DateTime(2005, 1, 1), 12);
            if (!svSai.IsValid(out message))
                Console.WriteLine("Thử dữ liệu sai: " + message);
        }

        private static void Bai5()
        {
            List<NguoiHoc> ds = new List<NguoiHoc>
            {
                new SinhVienChinhQuy { HoTen = "An", SoTinChi = 18 },
                new SinhVienLienThong { HoTen = "Bình", SoTinChi = 15 }
            };

            decimal tong = 0;
            foreach (NguoiHoc item in ds)
            {
                decimal hocPhi = item.TinhHocPhi();
                tong += hocPhi;
                Console.WriteLine($"{item.HoTen}: {hocPhi:N0} VNĐ");
            }
            Console.WriteLine($"Tổng học phí: {tong:N0} VNĐ");
        }

        private static void Bai6()
        {
            List<NhanVien> ds = new List<NhanVien>
            {
                new NhanVienVanPhong("NV01", "Nguyễn An", 8000000m, 1000000m),
                new NhanVienKinhDoanh("NV02", "Trần Bình", 7000000m, 50000000m, 0.05m),
                new GiangVien("NV03", "Lê Hoa", 80, 150000m)
            };

            foreach (NhanVien nv in ds)
                Console.WriteLine($"{nv.MoTa()} | Lương: {nv.TinhLuong():N0} VNĐ");
        }

        private static void Bai7()
        {
            List<IMove> ds = new List<IMove>
            {
                new XeMay("Honda", 60, 1.5, 1200m),
                new OTo("Sedan", 100, 2, 5000m),
                new XeDap("Thể thao", 15, 1, 200m)
            };

            foreach (IMove xe in ds)
                Console.WriteLine($"{xe.LayThongTin()} | Tốc độ: {xe.TinhTocDo():N1} km/h | Chi phí 10 km: {xe.TinhChiPhiVanHanh(10):N0} VNĐ");
        }

        private static void Bai8()
        {
            List<TaiLieu> ds = new List<TaiLieu>
            {
                new Sach("TL01", "Lập trình C#", "Nguyễn Văn A"),
                new TapChi("TL02", "Công nghệ số", 12),
                new LuanVan("TL03", "Ứng dụng OOP", "Trần Văn B", "Công nghệ thông tin")
            };

            Console.WriteLine("Danh sách tài liệu:");
            foreach (TaiLieu tl in ds)
                Console.WriteLine(tl.InThongTin());

            Console.Write("Nhập từ khóa tìm kiếm: ");
            string tuKhoa = Console.ReadLine() ?? "";
            var ketQua = ds.Where(tl => tl.MaTaiLieu.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)
                                     || tl.TieuDe.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)).ToList();
            Console.WriteLine($"Tìm thấy {ketQua.Count} tài liệu:");
            foreach (TaiLieu tl in ketQua)
                Console.WriteLine(tl.InThongTin());
        }

        private static void Bai9()
        {
            Console.WriteLine("Chọn phương thức: 1. Tiền mặt  2. Chuyển khoản  3. Ví điện tử");
            Console.Write("Lựa chọn: ");
            string loai = Console.ReadLine() ?? "";
            IPayment? payment = loai switch
            {
                "1" => new ThanhToanTienMat(),
                "2" => new ChuyenKhoan(),
                "3" => new ViDienTu(),
                _ => null
            };

            if (payment == null)
            {
                Console.WriteLine("Phương thức thanh toán không hợp lệ.");
                return;
            }

            Console.Write("Nhập số tiền: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal soTien))
            {
                Console.WriteLine("Số tiền không hợp lệ.");
                return;
            }

            if (!payment.KiemTra(soTien, out string thongBao))
            {
                Console.WriteLine(thongBao);
                return;
            }

            Console.WriteLine(thongBao);
            Console.WriteLine(payment.ThanhToan(soTien));
        }

        private static void Bai10()
        {
            List<SinhVienTheoLoai> ds = new List<SinhVienTheoLoai>
            {
                new SinhVienChinhQuyThongTin("SV001", "Nguyễn An", 8.5),
                new SinhVienLienThongThongTin("SV002", "Trần Bình", 7.2),
                new SinhVienChinhQuyThongTin("SV003", "Lê Hoa", 9.0),
                new SinhVienLienThongThongTin("SV004", "Phạm Minh", 6.8)
            };

            Console.WriteLine("Danh sách sinh viên:");
            foreach (SinhVienTheoLoai sv in ds)
            {
                if (sv.IsValid(out string message))
                    Console.WriteLine(sv.ToDisplayText());
                else
                    Console.WriteLine($"Dữ liệu lỗi ({sv.MaSV}): {message}");
            }

            Console.WriteLine("\nThống kê theo loại:");
            foreach (var nhom in ds.GroupBy(sv => sv.LoaiSinhVien()))
            {
                Console.WriteLine($"{nhom.Key}: {nhom.Count()} sinh viên, điểm trung bình = {nhom.Average(sv => sv.DiemTB):N2}");
            }

            Console.Write("\nNhập mã sinh viên cần tìm: ");
            string ma = Console.ReadLine() ?? "";
            SinhVienTheoLoai? tim = ds.FirstOrDefault(sv => sv.MaSV.Equals(ma, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(tim == null ? "Không tìm thấy sinh viên." : "Kết quả: " + tim.ToDisplayText());
        }
    }
}
