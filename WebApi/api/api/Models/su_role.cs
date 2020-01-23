using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("SU_ROLE", Schema = "TESTDB")]
    public class su_role
    {
        [Key, Column("ROLE_ID")]
        [StringLength(15)]
        public string ROLE_ID { get; set; }


        [Column("ROLE_NAME")]
        [StringLength(60)]
        public string ROLE_NAME { get; set; }

        [Column("ACTIVE")]
        [StringLength(1)]
        public string ACTIVE { get; set; }
    }
}