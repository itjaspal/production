using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("MPS_DET_WC", Schema = "TESTDB")]
    public class mps_det_wc
    {
        [Key, Column("ENTITY", Order = 0)]
        [StringLength(8)]
        public string ENTITY { get; set; }

        [Key, Column("REQ_DATE", Order = 1)]
        public DateTime REQ_DATE { get; set; }

        [Key, Column("PCS_NO", Order = 2)]
        [StringLength(5)]
        public string PCS_NO { get; set; }

        [Key, Column("BUILD_NO", Order = 3)]
        [StringLength(2)]
        public string BUILD_NO { get; set; }

        [Key, Column("WC_CODE", Order = 4)]
        [StringLength(6)]
        public string WC_CODE { get; set; }

        [Column("PCS_BARCODE")]
        [StringLength(15)]
        public string PCS_BARCODE { get; set; }

        [Column("BAR_CODE")]
        [StringLength(14)]
        public string BAR_CODE { get; set; }

        [Column("PROD_CODE")]
        [StringLength(22)]
        public string PROD_CODE { get; set; }

        [Column("PROD_NAME")]
        [StringLength(50)]
        public string PROD_NAME { get; set; }

        [Column("PROD_GROUP")]
        [StringLength(4)]
        public string PROD_GROUP { get; set; }

        [Column("PDBRND_CODE")]
        [StringLength(4)]
        public string PDBRND_CODE { get; set; }

        [Column("PDTYPE_CODE")]
        [StringLength(4)]
        public string PDTYPE_CODE { get; set; }

        [Column("PDDSGN_CODE")]
        [StringLength(4)]
        public string PDDSGN_CODE { get; set; }

        [Column("PDSIZE_CODE")]
        [StringLength(10)]
        public string PDSIZE_CODE { get; set; }

        [Column("PDCOLOR_CODE")]
        [StringLength(3)]
        public string PDCOLOR_CODE { get; set; }

        [Column("UOM_CODE")]
        [StringLength(4)]
        public string UOM_CODE { get; set; }

        [Column("POR_NO")]
        [StringLength(12)]
        public string POR_NO { get; set; }

        [Column("REF_NO")]
        [StringLength(20)]
        public string REF_NO { get; set; }

        [Column("QTY_PDT")]
        //[StringLength(12)]
        public int QTY_PDT { get; set; }

        [Column("PROCESS_NO")]
        [StringLength(5)]
        public string PROCESS_NO { get; set; }

        [Column("SD_NO")]
        [StringLength(8)]
        public string SD_NO { get; set; }

        [Column("TICK_NO")]
        [StringLength(12)]
        public string TICK_NO { get; set; }

        [Column("MODEL_NAME")]
        [StringLength(12)]
        public string MODEL_NAME { get; set; }

        [Column("MPS_ST")]
        [StringLength(1)]
        public string MPS_ST { get; set; }

        [Column("FIN_BY")]
        [StringLength(15)]
        public string FIN_BY { get; set; }

        [Column("FIN_DATE")]
        public DateTime? FIN_DATE { get; set; }

        //[Column("GEN_BY")]
        //[StringLength(15)]
        //public string GEN_BY { get; set; }

        //[Column("GEN_DATE")]
        //public DateTime? GEN_DATE { get; set; }

        [Column("UPD_BY")]
        [StringLength(15)]
        public string UPD_BY { get; set; }

        [Column("UPD_DATE")]
        public DateTime? UPD_DATE { get; set; }

    }
}