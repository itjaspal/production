using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("WHMOBILEPRNT_CTL", Schema = "TESTDB")]
    public class whmobileprnt_ctl
    {

        [Key, Column("SERIES_NO", Order = 0)]
        [StringLength(20)]
        public string SERIES_NO { get; set; }

        [Key, Column("GRP_TYPE", Order = 1)]
        [StringLength(20)]
        public string GRP_TYPE { get; set; }


        [Column("PRNT_POINT_NAME")]
        [StringLength(50)]
        public string PRNT_POINT_NAME { get; set; }

        [Key, Column("FILEPATH_DATA")]
        [StringLength(60)]
        public string FILEPATH_DATA { get; set; }

        [Column("FILEPATH_BTW")]
        [StringLength(60)]
        public string FILEPATH_BTW { get; set; }

        [Column("FILEPATH_TXT")]
        [StringLength(60)]
        public string FILEPATH_TXT { get; set; }

        //[Column("DEFAULT_NO")]
        //[StringLength(20)]
        //public string DEFAULT_NO { get; set; }

        [Column("USER_CODE")]
        [StringLength(15)]
        public string USER_CODE { get; set; }

        [Column("SYS_DATE")]
        public DateTime SYS_DATE { get; set; }

    }
}