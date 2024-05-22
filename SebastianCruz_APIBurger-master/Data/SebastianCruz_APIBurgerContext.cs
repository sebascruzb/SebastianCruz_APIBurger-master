using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SebastianCruz_APIBurger.Data.Models;

namespace SebastianCruz_APIBurger.Data
{
    public class SebastianCruz_APIBurgerContext : DbContext
    {
        public SebastianCruz_APIBurgerContext (DbContextOptions<SebastianCruz_APIBurgerContext> options)
            : base(options)
        {
        }

        public DbSet<SebastianCruz_APIBurger.Data.Models.Burger> Burger { get; set; } = default!;
        public DbSet<SebastianCruz_APIBurger.Data.Models.Promo> Promo { get; set; } = default!;
    }
}
