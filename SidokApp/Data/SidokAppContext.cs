using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SidokApp.Models.Dto;

namespace SidokApp.Data
{
    public class SidokAppContext : DbContext
    {
        public SidokAppContext (DbContextOptions<SidokAppContext> options)
            : base(options)
        {
        }

        public DbSet<SidokApp.Models.Dto.DokterDto> DokterDto { get; set; } = default!;
    }
}
