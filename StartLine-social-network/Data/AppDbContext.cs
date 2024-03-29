﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StartLine_social_network.Models;

namespace StartLine_social_network.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
