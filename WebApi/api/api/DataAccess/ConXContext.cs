using api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.DataAccess
{
    [DbConfigurationType(typeof(MyDbConfiguration))]
    public partial class ConXContext : DbContext
    //public class ConXContext : DbContext   
    {
        public ConXContext() : base("OracleDbContext")
        {

            //this.Configuration.LazyLoadingEnabled = false;

            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;

        }

     


        public DbSet<ic_mast> ic_masts { get; set; }
        public DbSet<pdgrp_mast> pdgroup { get; set; }
        public DbSet<pdtype_mast> pdtype { get; set; }
        public DbSet<pdbrnd_mast> pdbrnd { get; set; }
        public DbSet<pdcolor_mast> pdcolor { get; set; }
        public DbSet<pdsize_mast> pdsize { get; set; }
        public DbSet<pdmodel_mast> pdmodel { get; set; }
        public DbSet<pddsgn_mast> pddsgn { get; set; }
        public DbSet<department> departments { get; set; }
        public DbSet<su_user> user { get; set; }
        public DbSet<su_role> role { get; set; }
        public DbSet<su_menu> menu { get; set; }
        public DbSet<su_user_role> user_role { get; set; }
        public DbSet<su_role_menu> role_menu { get; set; }
        public DbSet<pd_mapp_user_mac> user_mac { get; set; }
        //public DbSet<su_user_entity> user_entity { get; set; }
        //public DbSet<mps_det_wc> mps_wc { get; set; }
        //public DbSet<mps_det_in_process> mps_in_process { get; set; }
        //public DbSet<mps_det_in_process_det> mps_in_process_det { get; set; }
        //public DbSet<mps_det_in_process_his> mps_in_process_his { get; set; }


    }
}