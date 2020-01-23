using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("PD_MAPP_USER_MAC", Schema = "TESTDB")]
    public class pd_mapp_user_mac
    {
        [Key, Column("USER_ID")]
        [StringLength(2)]
        public string USER_ID { get; set; }

        [Column("MC_CODE")]
        [StringLength(40)]
        public string MC_CODE { get; set; }

        [Column("STATUS")]
        [StringLength(2)]
        public string STATUS { get; set; } = "A";
    }
}