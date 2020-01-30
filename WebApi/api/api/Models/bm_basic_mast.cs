using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("BM_BASIC_MAST", Schema = "TESTDB")]
    public class bm_basic_mast
    {
        [Key, Column("HARD_CODE")]
        [StringLength(2)]
        public string HARD_CODE { get; set; }

        [Column("SPRING_PATH")]
        [StringLength(100)]
        public string SPRING_PATH { get; set; }

    }
}