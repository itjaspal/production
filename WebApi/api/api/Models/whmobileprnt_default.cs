using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("WHMOBILEPRNT_DEFAULT", Schema = "TESTDB")]
    public class whmobileprnt_default
    {
        [Key, Column("MC_CODE")]
        [StringLength(20)]
        public string MC_CODE { get; set; }

        [Column("SERIES_NO")]
        [StringLength(20)]
        public string SERIES_NO { get; set; }

        [Column("UPD_BY")]
        [StringLength(15)]
        public string UPD_BY { get; set; }

        [Column("UPD_DATE")]
        public DateTime UPD_DATE { get; set; }

    }
}