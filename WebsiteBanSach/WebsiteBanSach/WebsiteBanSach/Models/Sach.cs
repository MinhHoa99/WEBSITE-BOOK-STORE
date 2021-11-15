using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace WebsiteBanSach.Models
{
    public class Sach
    {
        [Key]
        public int MaSach { get; set; }
        [DisplayName("Tên Sách")]
        [Required(ErrorMessage ="Không được bỏ trống")]
        public string TenSach { get; set; }

        [Required(ErrorMessage ="Không được bỏ trống")]
        [Range(10000, 50000000, ErrorMessage ="giá từ 10.000 đến 50.000.000")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0,0}")]
        public int GiaSach { get; set; }

        [DisplayName("Tác Giả")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string TacGia { get; set; }

        [DisplayName("Loại Bìa")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string LoaiBia { get; set; }

        [DisplayName("Ngày Xuất Bản")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public DateTime NgayXuatBan { get; set; }

        public int? MaNhaXuatBan { get; set; }
        public NhaXuatBan NhaXuatBan { get; set; }
        public int? MaCongTyPhatHanh { get; set; }
        public CongTyPhatHanh CongTyPhatHanh { get; set; }
        public int? MaDanhMuc { get; set; }
        public DanhMuc DanhMuc { get; set; }

        [DisplayName("Số Trang")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int SoTrang { get; set; }

        [DisplayName("Kích Thước")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string KichThuoc { get; set; }

        [DisplayName("Mô Tả")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string MoTa { get; set; }

        [DisplayName("Hình Ảnh")]
        public string Hinh { get; set; }

        [DisplayName("Số Lượng")]
        public int SoLuongSach { get; set; }

        [DisplayName("Tổng Tiền")]
        public int TongTien
        {
            get
            {
                return SoLuongSach * GiaSach;
            }
        }
    }
}