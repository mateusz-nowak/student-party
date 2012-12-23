using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Party.Models
{
    public class DbSet : DbContext
    {
        public DbSet<Event> Events { get; set; }
    }
}