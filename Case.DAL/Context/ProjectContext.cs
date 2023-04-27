using Case.Entities.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.DAL.Context
{
    public class ProjectContext:IdentityDbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {


        }

      
        public DbSet<Form> Forms { get; set; }
    }
}
