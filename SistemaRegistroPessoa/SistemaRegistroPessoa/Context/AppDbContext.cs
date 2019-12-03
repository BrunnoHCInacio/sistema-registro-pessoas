using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaRegistroPessoa.Models;

namespace SistemaRegistroPessoa.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<People> Peoples { get; set; }
    }
}
