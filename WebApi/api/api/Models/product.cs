using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("PRODUCT", Schema = "TESTDB")]
    public class product
    {
        [Key, Column("PROD_CODE")]
        [StringLength(22)]
        public string PROD_CODE { get; set; }

        [Column("PROD_TNAME")]
        [StringLength(50)]
        public string PROD_TNAME { get; set; }

        [Column("PDMODEL_DESC")]
        [StringLength(40)]
        public string PDMODEL_DESC { get; set; }
    }
}