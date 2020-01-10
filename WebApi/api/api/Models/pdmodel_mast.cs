using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("PDMODEL_MAST", Schema = "TESTDB")]
    public class pdmodel_mast
    {
        [Key, Column("PDMODEL_CODE")]
        [StringLength(4)]
        public string PDMODEL_CODE { get; set; }

        [Column("PDMODEL_TNAME")]
        [StringLength(40)]
        public string PDMODEL_TNAME { get; set; }

        [Column("STATUS")]
        [StringLength(2)]
        public string STATUS { get; set; } = "A";
    }
}