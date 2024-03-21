﻿// <auto-generated />
using System;
using EMS_Domain.EMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EMS_Domain.Migrations
{
    [DbContext(typeof(EMSContext))]
    [Migration("20240316144737_ems_initial")]
    partial class ems_initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.17");

            modelBuilder.Entity("EMS_Domain.Entities.election_city", b =>
                {
                    b.Property<int>("election_city_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("election_city_active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("election_city_name")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("TEXT");

                    b.Property<int>("election_state_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("election_city_id");

                    b.ToTable("election_citys");
                });

            modelBuilder.Entity("EMS_Domain.Entities.election_constant_values", b =>
                {
                    b.Property<int>("election_constant_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_constant_key")
                        .HasColumnType("INTEGER");

                    b.Property<string>("election_constant_value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("group_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("election_constant_id");

                    b.ToTable("election_constant_values_lists");
                });

            modelBuilder.Entity("EMS_Domain.Entities.election_contesting_candidate", b =>
                {
                    b.Property<int>("election_contest_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_city_id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_party_id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_state_id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_user_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("election_contest_id");

                    b.ToTable("election_contesting_candidates");
                });

            modelBuilder.Entity("EMS_Domain.Entities.election_mp_seat_by_state", b =>
                {
                    b.Property<int>("election_mp_seat_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_mp_seat_no")
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_state_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("election_mp_seat_id");

                    b.ToTable("election_mp_seat_by_states");
                });

            modelBuilder.Entity("EMS_Domain.Entities.election_parties", b =>
                {
                    b.Property<int>("election_party_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("election_party_active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("election_party_name")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("TEXT");

                    b.Property<int>("election_sym_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("election_party_id");

                    b.ToTable("election_parties_lists");
                });

            modelBuilder.Entity("EMS_Domain.Entities.election_poll_data", b =>
                {
                    b.Property<int>("election_poll_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_city_id")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(4);

                    b.Property<int>("election_id")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(0);

                    b.Property<int>("election_party_id")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(2);

                    b.Property<int>("election_state_id")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(3);

                    b.Property<int>("election_user_id")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(1);

                    b.HasKey("election_poll_id");

                    b.ToTable("election_poll_datas");
                });

            modelBuilder.Entity("EMS_Domain.Entities.election_result", b =>
                {
                    b.Property<int>("election_result_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_city_id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_party_id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_poll_count")
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_state_id")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("election_winner_by_state_wise")
                        .HasColumnType("INTEGER");

                    b.HasKey("election_result_id");

                    b.ToTable("election_results");
                });

            modelBuilder.Entity("EMS_Domain.Entities.election_state", b =>
                {
                    b.Property<int>("election_state_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("election_state_active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("election_state_name")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("TEXT");

                    b.HasKey("election_state_id");

                    b.ToTable("election_states");
                });

            modelBuilder.Entity("EMS_Domain.Entities.election_symbols", b =>
                {
                    b.Property<int>("election_sym_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("election_sym_active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("election_sym_font_name")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("TEXT");

                    b.Property<string>("election_sym_name")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("TEXT");

                    b.HasKey("election_sym_id");

                    b.ToTable("election_symbols_lists");
                });

            modelBuilder.Entity("EMS_Domain.Entities.election_user", b =>
                {
                    b.Property<int>("election_user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("election_address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("election_city_id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_state_id")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("election_user_approve_status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("election_voter_id")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("TEXT");

                    b.Property<string>("election_voter_name")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("TEXT");

                    b.Property<string>("election_voter_password")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("TEXT");

                    b.HasKey("election_user_id");

                    b.ToTable("election_users");
                });

            modelBuilder.Entity("EMS_Domain.Entities.election_year_to_date", b =>
                {
                    b.Property<int>("election_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("election_current_status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("election_end_date_time")
                        .HasColumnType("TEXT");

                    b.Property<string>("election_name")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("election_start_date_time")
                        .HasColumnType("TEXT");

                    b.HasKey("election_id");

                    b.ToTable("election_year_to_dates");
                });
#pragma warning restore 612, 618
        }
    }
}
