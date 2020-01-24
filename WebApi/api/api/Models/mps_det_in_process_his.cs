using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("MPS_DET_IN_PROCESS_HIS", Schema = "TESTDB")]
    public class mps_det_in_process_his
    {
        [Key, Column("MPS_DET_IN_PROCESS_HIS_ID")]
        //[StringLength(38)]
        public int MPS_DET_IN_PROCESS_HIS_ID { get; set; }

        [Column("MPS_DET_IN_PROCESS_ID")]
        //[StringLength(38)]
        public int MPS_DET_IN_PROCESS_ID { get; set; }

        [Column("ENTITY")]
        [StringLength(8)]
        public string ENTITY { get; set; }

        [Column("REQ_DATE")]
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

        [Column("MC_CODE_FROM")]
        [StringLength(10)]
        public string MC_CODE_FROM { get; set; }

        [Column("MC_CODE_TO")]
        [StringLength(10)]
        public string MC_CODE_TO { get; set; }

        [Column("PCS_BARCODE")]
        [StringLength(15)]
        public string PCS_BARCODE { get; set; }

        [Column("PROD_CODE")]
        [StringLength(22)]
        public string PROD_CODE { get; set; }

        [Column("GEN_BY")]
        [StringLength(15)]
        public string GEN_BY { get; set; }

        [Column("GEN_DATE")]
        //[StringLength(15)]
        public DateTime GEN_DATE { get; set; }

        [Column("UPD_BY")]
        [StringLength(15)]
        public string UPD_BY { get; set; }

        [Column("UPD_DATE")]
        //[StringLength(15)]
        public DateTime UPD_DATE { get; set; }
    }
}