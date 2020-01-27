using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("MPS_MAST", Schema = "TESTDB")]
    public class mps_mast
    {
        [Key, Column("ENTITY", Order = 0)]
        [StringLength(8)]
        public string ENTITY { get; set; }

        [Key, Column("REQ_DATE", Order = 1)]
        public DateTime REQ_DATE { get; set; }

        [Key, Column("BUILD_NO", Order = 2)]
        public int BUILD_NO { get; set; }
    }
}