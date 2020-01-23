using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("SU_ROLE_MENU", Schema = "TESTDB")]
    public class su_role_menu
    {
        [Key, Column("ROLE_ID", Order = 0)]
        [StringLength(2)]
        public string ROLE_ID { get; set; }

        [Key, Column("MENU_ID", Order = 1)]
        [StringLength(15)]
        public string MENU_ID { get; set; }



        //[ForeignKey("ROLE_ID")]
        //public virtual List<su_role> su_role { get; set; }

        //[ForeignKey("ROLE_ID")]
        //public virtual su_user_role su_user_role { get; set; }

        //[ForeignKey("MENU_ID")]
        //public virtual List<su_menu> su_menu { get; set; }



       

    }
}