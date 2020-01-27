using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    [Table("PD_JIT_SCHEDULE_CTL", Schema = "TESTDB")]
    public class pd_jit_schedule_ctl
    {
        [Key, Column("PD_ENTITY", Order = 0)]
        [StringLength(8)]
        public string PD_ENTITY { get; set; }

        [Key, Column("SEQ_NO", Order = 1)]
        public int SEQ_NO { get; set; }

        [Key, Column("PDGRP_CODE", Order = 2)]
        [StringLength(4)]
        public string PDGRP_CODE { get; set; }

        [Key, Column("WC_CODE", Order = 3)]
        [StringLength(15)]
        public string WC_CODE { get; set; }

        [Key, Column("MPS_BACK_DAY", Order = 4)]
        public int MPS_BACK_DAY { get; set; }

        //[Column("START_TIME")]
        //public int START_TIME { get; set; }

        //[Column("FINISED_TIME")]
        //public int FINISED_TIME { get; set; }

    }
}