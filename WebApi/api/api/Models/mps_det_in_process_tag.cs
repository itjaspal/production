using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("MPS_DET_IN_PROCESS_TAG", Schema = "TESTDB")]
    public class mps_det_in_process_tag
    {
        [Key, Column("ENTITY", Order = 0)]
        ///[StringLength(38)]
        public string ENTITY { get; set; }

        [Key, Column("REQ_DATE", Order = 1)]
        public DateTime REQ_DATE { get; set; }

        [Key, Column("MC_CODE", Order = 2)]
        [StringLength(10)]
        public string MC_CODE { get; set; }

        [Key, Column("SEQ_NO", Order = 3)]
        public int SEQ_NO { get; set; }

        [Column("FIN_DATE")]
        public DateTime FIN_DATE { get; set; }

        [Column("PROCESS_TAG_NO")]
        public int PROCESS_TAG_NO { get; set; }

        [Column("PROCESS_TAG_QR")]
        [StringLength(500)]
        public string PROCESS_TAG_QR { get; set; }

        [Column("REF_DOC_NO")]
        [StringLength(15)]
        public string REF_DOC_NO { get; set; }

        [Column("PROD_CODE")]
        [StringLength(22)]
        public string PROD_CODE { get; set; }

        [Column("PROD_TNAME")]
        [StringLength(22)]
        public string PROD_TNAME { get; set; }

        [Column("UPD_BY")]
        [StringLength(15)]
        public string UPD_BY { get; set; }

        [Column("UPD_DATE")]
        public DateTime UPD_DATE { get; set; }
    }
}