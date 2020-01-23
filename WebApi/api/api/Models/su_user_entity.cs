using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("SU_USER_ENTITY", Schema = "TESTDB")]
    public class su_user_entity
    {
        [Key, Column("USER_ID")]
        [StringLength(8)]
        public string USER_ID { get; set; }


        [Column("ENTITY_ALLOWED")]
        [StringLength(8)]
        public string ENTITY_ALLOWED { get; set; }
    }
}