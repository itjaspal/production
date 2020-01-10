using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("PDCOLOR_MAST", Schema = "TESTDB")]
    public class pdcolor_mast
    {
        [Key, Column("PDCOLOR_CODE")]
        [StringLength(3)]
        public string PDCOLOR_CODE { get; set; }

        [Column("PDCOLOR_TNAME")]
        [StringLength(40)]
        public string PDCOLOR_TNAME { get; set; }

        [Column("STATUS")]
        [StringLength(2)]
        public string STATUS { get; set; } = "A";
    }
}