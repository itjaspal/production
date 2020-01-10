using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("PDDSGN_MAST", Schema = "TESTDB")]
    public class pddsgn_mast
    {
        [Key, Column("PDDSGN_CODE")]
        [StringLength(4)]
        public string PDDSGN_CODE { get; set; }

        [Column("PDDSGN_TNAME")]
        [StringLength(40)]
        public string PDDSGN_TNAME { get; set; }

        [Column("STATUS")]
        [StringLength(2)]
        public string STATUS { get; set; } = "A";
    }
}