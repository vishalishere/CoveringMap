using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoverMap.Models
{
    public class CoverContext : DbContext
    {
        public DbSet<Cover> Covers { get; set; }
    }
}