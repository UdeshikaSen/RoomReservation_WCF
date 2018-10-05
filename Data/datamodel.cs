using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Data.Models;

namespace Data
{
    public class datamodel: DbContext
    {
        public DbSet<User> User { get; set; }

    }
}
