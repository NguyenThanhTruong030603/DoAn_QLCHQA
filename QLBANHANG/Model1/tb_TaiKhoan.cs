namespace QLBANHANG.Model1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_TaiKhoan
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string TenTaiKhoan { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string Matkhau { get; set; }
    }
}
