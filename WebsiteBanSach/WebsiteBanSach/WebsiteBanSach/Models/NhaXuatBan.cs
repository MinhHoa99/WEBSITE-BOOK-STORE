using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace WebsiteBanSach.Models
{
    public class NhaXuatBan
    {
        [Key]
        public int MaNhaXuatBan { get; set; }

        [DisplayName("Tên Nhà Xuất Bản")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string TenNXB { get; set; }

        [DisplayName("Số Lượng Sách")]
        [Required(ErrorMessage ="Không được bỏ trống")]
        [Range(1, 10000, ErrorMessage ="Số lượng sách từ 1 đến 1.000 quyển")]
        public int SoLuongSach { get; set; }

        public ICollection<Sach> Saches { get; set; }
    }
}