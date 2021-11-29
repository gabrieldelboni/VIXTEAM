using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VIXTEAM.Models;

namespace DELBONI.Data
{
    public class DELBONIContext : DbContext
    {
        public DELBONIContext (DbContextOptions<DELBONIContext> options)
            : base(options)
        {
        }

        public DbSet<VIXTEAM.Models.PessoaModel> PessoaModel { get; set; }
    }
}
