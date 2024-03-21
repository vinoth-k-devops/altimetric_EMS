using System.Collections.Generic;
using System.Reflection;
using EMS_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMS_Domain.EMS
{
	public class EMSContext : DbContext
    {
        public EMSContext(DbContextOptions<EMSContext> options) : base(options)
        {

        }

        public DbSet<election_constant_values> election_constant_values_lists => Set<election_constant_values>();
        public DbSet<election_contesting_candidate> election_contesting_candidates => Set<election_contesting_candidate>();
        public DbSet<election_mp_seat_by_state> election_mp_seat_by_states => Set<election_mp_seat_by_state>();
        public DbSet<election_parties> election_parties_lists => Set<election_parties>();
        public DbSet<election_poll_data> election_poll_datas => Set<election_poll_data>();
        public DbSet<election_result> election_results => Set<election_result>();
        public DbSet<election_state> election_states => Set<election_state>();
        public DbSet<election_city> election_citys => Set<election_city>();
        public DbSet<election_symbols> election_symbols_lists => Set<election_symbols>();
        public DbSet<election_user> election_users => Set<election_user>();
        public DbSet<election_year_to_date> election_year_to_dates => Set<election_year_to_date>();

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=/Volumes/VinothSSD/Altimetrik/altimetric_EMS/EMS_SQLLite/EMS.db");
        //}
    }
}

