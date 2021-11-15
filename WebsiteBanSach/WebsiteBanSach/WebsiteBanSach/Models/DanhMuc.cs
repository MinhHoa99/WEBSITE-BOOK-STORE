using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace WebsiteBanSach.Models
{
    public class DanhMuc
    {
        [Key]
        public int MaDanhMuc { get; set; }

        [DisplayName("Tên Danh Mục")]
        [Required(ErrorMessage ="Khôngdược bỏ trống")]
        public string TenDanhMuc { get; set; }
        public ICollection<Sach> Saches { get; set; }
    }
}