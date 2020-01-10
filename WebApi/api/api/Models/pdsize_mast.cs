using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("PDSIZE_MAST", Schema = "TESTDB")]
    public class pdsize_mast
    {
        [Key, Column("PDSIZE_CODE")]
        [StringLength(10)]
        public string PDSIZE_CODE { get; set; }

        [Column("PDSIZE_TNAME")]
        [StringLength(40)]
        public string PDSIZE_TNAME { get; set; }

        [Column("STATUS")]
        [StringLength(2)]
        public string STATUS { get; set; } = "A";
    }
}