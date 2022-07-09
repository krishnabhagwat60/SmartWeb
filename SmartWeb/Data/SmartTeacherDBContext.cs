using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartWeb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartWeb.Data
{
    public class SmartTeacherDBContext : IdentityDbContext
    {
        public SmartTeacherDBContext(DbContextOptions<SmartTeacherDBContext> options)
            : base(options)
        {
        }
        public DbSet<TblApplicationUser> TblApplicationUser { get; set; }
        public DbSet<TblAlert> TblAlert { get; set; }
    }
}
