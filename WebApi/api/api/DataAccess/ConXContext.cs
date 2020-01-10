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

            this.Configuration.LazyLoadingEnabled = false;

        }




        public DbSet<ic_mast> ic_masts { get; set; }
        public DbSet<pdgrp_mast> pdgroup { get; set; }
        public DbSet<pdtype_mast> pdtype { get; set; }
        public DbSet<pdbrnd_mast> pdbrnd { get; set; }
        public DbSet<pdcolor_mast> pdcolor { get; set; }
        public DbSet<pdsize_mast> pdsize { get; set; }
        public DbSet<pdmodel_mast> pdmodel { get; set; }
        public DbSet<pddsgn_mast> pddsgn { get; set; }
    }
}