namespace QLBANHANG.Model1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_CTHD
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaHH { get; set; }

        public int? SoLuong { get; set; }

        public int? DonGia { get; set; }

        public virtual tb_HangHoa tb_HangHoa { get; set; }

        public virtual tb_HoaDon tb_HoaDon { get; set; }
    }
}
