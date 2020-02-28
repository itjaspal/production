using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("MPS_DET_IN_PROCESS", Schema = "TESTDB")]
    public class mps_det_in_process
    {
        [Key, Column("MPS_DET_IN_PROCESS_ID")]
        //[StringLength(38)]
        public int MPS_DET_IN_PROCESS_ID { get; set; }

        [Column("ENTITY")]
        [StringLength(8)]
        public string ENTITY { get; set; }

        [Column("REQ_DATE")]
        //[StringLength(2)]
        public DateTime REQ_DATE { get; set; }

        [Column("PCS_NO")]
        //[StringLength(5)]
        public int PCS_NO { get; set; }

        [Column("BUILD_NO")]
        //[StringLength(6)]
        public int BUILD_NO { get; set; }

        [Column("WC_CODE")]
        [StringLength(6)]
        public string WC_CODE { get; set; }

        [Column("MC_CODE")]
        [StringLength(10)]
        public string MC_CODE { get; set; }

        [Column("PCS_BARCODE")]
        [StringLength(15)]
        public string PCS_BARCODE { get; set; }

        [Column("BAR_CODE")]
        [StringLength(14)]
        public string BAR_CODE { get; set; }

        [Column("PROD_CODE")]
        [StringLength(22)]
        public string PROD_CODE { get; set; }

        [Column("PDSIZE_CODE")]
        [StringLength(10)]
        public string PDSIZE_CODE { get; set; }

        [Column("PDSIZE_DESC")]
        [StringLength(40)]
        public string PDSIZE_DESC { get; set; }

        [Column("SD_NO")]
        [StringLength(8)]
        public string SD_NO { get; set; }

        [Column("SPRING_GRP")]
        [StringLength(6)]
        public string SPRING_GRP { get; set; }


        [Column("SPRINGTYPE_CODE")]
        [StringLength(6)]
        public string SPRINGTYPE_CODE { get; set; }

        [Column("MPS_ST")]
        [StringLength(4)]
        public string MPS_ST { get; set; }

        [Column("FIN_BY")]
        [StringLength(15)]
        public string FIN_BY { get; set; }

        [Column("FIN_DATE")]
        public DateTime? FIN_DATE { get; set; }

        [Column("GEN_BY")]
        [StringLength(15)]
        public string GEN_BY { get; set; }

        [Column("GEN_DATE")]
        public DateTime? GEN_DATE { get; set; }

        [Column("UPD_BY")]
        [StringLength(15)]
        public string UPD_BY { get; set; }

        [Column("UPD_DATE")]
        public DateTime? UPD_DATE { get; set; }

        [Column("PROCESS_TAG_QR")]
        [StringLength(500)]
        public string PROCESS_TAG_QR { get; set; }


    }
}