using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("PDBRND_MAST", Schema = "TESTDB")]
    public class pdbrnd_mast
    {
        [Key, Column("PDBRND_CODE")]
        [StringLength(2)]
        public string PDBRND_CODE { get; set; }

        [Column("PDBRND_TNAME")]
        [StringLength(40)]
        public string PDBRND_TNAME { get; set; }

        [Column("STATUS")]
        [StringLength(2)]
        public string STATUS { get; set; } = "A";
    }
}