using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("SU_USER_ROLE", Schema = "TESTDB")]
    public class su_user_role
    {
        [Key, Column("USER_ID", Order = 0)]
        [StringLength(15)]
        public string USER_ID { get; set; }

        [Key, Column("ROLE_ID", Order = 1)]
        [StringLength(15)]
        public string ROLE_ID { get; set; }

        [Column("ACTIVE")]
        [StringLength(1)]
        public string ACTIVE { get; set; }

        //[ForeignKey("USER_ID")]
        //public virtual List<su_user> su_user { get; set; }

        //[ForeignKey("ROLE_ID")]
        //////public virtual su_role_menu su_role_menu { get; set; }
        //public virtual List<su_role_menu> su_role_menu { get; set; }
    }
}