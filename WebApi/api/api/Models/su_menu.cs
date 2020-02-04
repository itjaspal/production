using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("SU_MENU", Schema = "TESTDB")]
    public class su_menu
    {
        [Key, Column("MENU_ID")]
        [StringLength(10)]
        public string MENU_ID { get; set; }

        [Column("MENU_NAME")]
        [StringLength(60)]
        public string MENU_NAME { get; set; }

        [Column("MENU_TYPE")]
        [StringLength(1)]
        public string MENU_TYPE { get; set; }

        [Column("MAIN_MENU")]
        [StringLength(10)]
        public string MAIN_MENU { get; set; }

        [Column("LINK_NAME")]
        [StringLength(15)]
        public string LINK_NAME { get; set; }

        [Column("ICON_NAME")]
        [StringLength(50)]
        public string ICON_NAME { get; set; }



    }
}