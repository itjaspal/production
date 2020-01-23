using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("DEPARTMENT", Schema = "TESTDB")]
    public class department
    {
        [Key, Column("DEPT_CODE")]
        [StringLength(6)]
        public string DEPT_CODE { get; set; }

        [Column("DEPT_NAMET")]
        [StringLength(50)]
        public string DEPT_NAMET { get; set; }

    }
}