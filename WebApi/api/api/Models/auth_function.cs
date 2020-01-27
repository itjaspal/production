using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("AUTH_FUNCTION", Schema = "TESTDB")]
    public class auth_function
    {
        [Key, Column("DEPT_CODE", Order = 0)]
        [StringLength(6)]
        public string DEPT_CODE { get; set; }

        [Key, Column("DOC_CODE", Order = 1)]
        [StringLength(4)]
        public string DOC_CODE { get; set; }

        [Key, Column("FUNCTION_ID", Order = 2)]
        [StringLength(15)]
        public string FUNCTION_ID { get; set; }

        [Key, Column("USER_ID", Order = 3)]
        [StringLength(15)]
        public string USER_ID { get; set; }

        [Key, Column("ENTITY_CODE", Order = 4)]
        [StringLength(8)]
        public string ENTITY_CODE { get; set; }

        [Column("EMAIL_ADDRESS")]
        [StringLength(50)]
        public string EMAIL_ADDRESS { get; set; }

    }
}