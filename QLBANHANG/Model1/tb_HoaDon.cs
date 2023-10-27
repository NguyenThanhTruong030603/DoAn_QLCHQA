namespace QLBANHANG.Model1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_HoaDon()
        {
            tb_CTHD = new HashSet<tb_CTHD>();
        }

        [Key]
        [StringLength(10)]
        public string MaHD { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayLap { get; set; }

        [StringLength(10)]
        public string NguoiLap { get; set; }

        [StringLength(10)]
        public string KhachHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_CTHD> tb_CTHD { get; set; }

        public virtual tb_KhachHang tb_KhachHang { get; set; }

        public virtual tb_NhanVien tb_NhanVien { get; set; }
    }
}
