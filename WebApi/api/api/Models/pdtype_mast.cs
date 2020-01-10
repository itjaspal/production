using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("PDTYPE_MAST", Schema = "TESTDB")]
    public class pdtype_mast
    {
        [Key, Column("PDTYPE_CODE")]
        [StringLength(4)]
        public string PDTYPE_CODE { get; set; }

        [Column("PDTYPE_TNAME")]
        [StringLength(40)]
        public string PDTYPE_TNAME { get; set; }

        [Column("STATUS")]
        [StringLength(2)]
        public string STATUS { get; set; } = "A";


    }
}