using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace WebsiteBanSach.Models
{
    public class CongTyPhatHanh
    {
        [Key]
        public int MaCongTyPhatHanh { get; set; }

        [DisplayName("Tên Công Ty")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string TenCongTy { get; set; }

        [DisplayName("Số Lượng Sách")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        [Range(1, 10000, ErrorMessage ="Số Lượng Sách Từ 1 đến 10000")]
        public int SoLuongSach { get; set; }
        public ICollection<Sach> Saches { get; set; }
    }
}