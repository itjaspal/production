using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("IC_MAST", Schema = "TESTDB")]
    public class ic_mast
    {
        [Key, Column("IC_ENTITY", Order = 0)]
        [StringLength(8)]
        public string IC_ENTITY { get; set; }

        [Key, Column("PROD_CODE", Order = 1)]
        [StringLength(22)]
        public string PROD_CODE { get; set; }

        [Key, Column("WH_CODE", Order = 2)]
        [StringLength(8)]
        public string WH_CODE { get; set; }

        [Column("QTY_OH")]
        public Int32 QTY_OH { get; set; }

        [Column("QTY_ALL")]
        public Int32 QTY_ALL { get; set; }

        [Column("QTY_SHIP")]
        public Int32? QTY_SHIP { get; set; }
        [NotMapped]
        public Int32 QTY_SHIP_DISPLAY
        {
            get
            {
                return QTY_SHIP ?? 0;
            }
            set
            {
                QTY_SHIP = value == 0 ? new Nullable<int>() : value;
            }
        }

        [Column("QTY_BUILD")]
        public Int32? QTY_BUILD { get; set; }
        [NotMapped]
        public Int32 QTY_BUILD_DISPLAY
        {
            get
            {
                return QTY_BUILD ?? 0;
            }
            set
            {
                QTY_BUILD = value == 0 ? new Nullable<int>() : value;
            }
        }

        [Column("QTY_AVAI")]
        public Int32 QTY_AVAI { get; set; }



    }
}