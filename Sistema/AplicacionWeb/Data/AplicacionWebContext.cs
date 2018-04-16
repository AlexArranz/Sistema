using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AplicacionWeb.Models
{
    public class AplicacionWebContext : DbContext
    {
        public AplicacionWebContext (DbContextOptions<AplicacionWebContext> options)
            : base(options)
        {
        }

        public DbSet<AplicacionWeb.Models.Categoria> Categoria { get; set; }
    }
}
