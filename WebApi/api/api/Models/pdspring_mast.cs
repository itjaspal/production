using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("PDSPRING_MAST", Schema = "TESTDB")]
    public class pdspring_mast
    {
        [Key, Column("SPRINGTYPE_CODE")]
        [StringLength(6)]
        public string SPRINGTYPE_CODE { get; set; }

        [Column("SPRINGTYPE_NAME")]
        [StringLength(150)]
        public string SPRINGTYPE_NAME { get; set; }

        [Column("SPRING_PIC_FILE")]
        [StringLength(100)]
        public string SPRING_PIC_FILE { get; set; }

        [Column("STATUS")]
        [StringLength(2)]
        public string STATUS { get; set; }

    }
}