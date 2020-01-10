using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("PDGRP_MAST", Schema = "TESTDB")]
    public class pdgrp_mast
    {
        [Key, Column("PDGRP_CODE")]
        [StringLength(4)]
        public string PDGRP_CODE { get; set; }

        [Column("PDGRP_TNAME")]
        [StringLength(40)]
        public string PDGRP_TNAME { get; set; }

        [Column("STATUS")]
        [StringLength(2)]
        public string STATUS { get; set; } = "A";


    }
}