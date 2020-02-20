using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("MPS_MR_PCS", Schema = "TESTDB")]
    public class mps_mr_pcs
    {
        [Key, Column("MPS_ID", Order = 0)]
        public int MPS_ID { get; set; }

        [Key, Column("PCS_BARCODE", Order = 2)]
        [StringLength(15)]
        public string PCS_BARCODE { get; set; }

        [Column("BOM_SEQ")]
        public int BOM_SEQ { get; set; }

        [Column("BOM_SUB")]
        [StringLength(22)]
        public string BOM_SUB { get; set; }

        [Column("BOM_NAME")]
        [StringLength(50)]
        public string BOM_NAME { get; set; }

        [Column("RM_SEQ")]
        public int RM_SEQ { get; set; }

        [Column("RM_CODE")]
        [StringLength(22)]
        public string RM_CODE { get; set; }

        [Column("RM_NAME")]
        [StringLength(22)]
        public string RM_NAME { get; set; }

        [Column("SHORT_NAME")]
        [StringLength(50)]
        public string SHORT_NAME { get; set; }

        [Column("UOM_CODE")]
        [StringLength(4)]
        public string UOM_CODE { get; set; }

        [Column("UNIT_QTY")]
        public int UNIT_QTY { get; set; }

        [Column("ITEM_NAME")]
        [StringLength(50)]
        public string ITEM_NAME { get; set; }

        [Column("ENTITY")]
        [StringLength(8)]
        public string ENTITY { get; set; }

        [Column("DEPT_BOM")]
        [StringLength(6)]
        public string DEPT_BOM { get; set; }




    }
}