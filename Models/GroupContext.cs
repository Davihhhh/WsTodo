using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class GroupContext : DbContext
    {
        public GroupContext(DbContextOptions<CategoryContext> options)
            : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }
    }
}
