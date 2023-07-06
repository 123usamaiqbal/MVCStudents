using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCStudents.Models;

namespace MVCStudents.Data
{
    public class MVCStudentsContext : DbContext
    {
        public MVCStudentsContext (DbContextOptions<MVCStudentsContext> options)
            : base(options)
        {
        }

        public DbSet<MVCStudents.Models.Students> Students { get; set; } = default!;
        public DbSet<MVCStudents.Models.Grading> Grading { get; set; } = default!;
        
        
    }
}
