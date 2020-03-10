using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("SU_USER", Schema = "TESTDB")]
    public class su_user
    {
        [Key, Column("USER_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(8)]
        public string USER_ID { get; set; }


        [Column("USER_NAME")]
        [StringLength(60)]
        public string USER_NAME { get; set; }

        [Column("USER_PASSWORD")]
        [StringLength(15)]
        public string USER_PASSWORD { get; set; }

        [Column("DEPT_CODE")]
        [StringLength(6)]
        public string DEPT_CODE { get; set; }

        [Column("ACTIVE")]
        [StringLength(2)]
        public string ACTIVE { get; set; } = "A";

        [ForeignKey("USER_ID")]
        public virtual List<su_user_role> su_user_role { get; set; }

        //[ForeignKey("DEPT_CODE")]
        //public virtual department Department { get; set; }

        [ForeignKey("DEPT_CODE")]
        public virtual department departments { get; set; }

        [ForeignKey("USER_ID")]
        public virtual pd_mapp_user_mac user_mac { get; set; }

    }
}