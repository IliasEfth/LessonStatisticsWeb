using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StatisticsWebModels;
//TODO: MAKE PUBLIC ENUMS TO HAVE CONNECTIONS ON ONE PLACE
namespace StatisticsWebDBModel.DBRelation
{
    public class StatisticsWebDB : DbContext
    {
        public DbSet<University> Universities { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<MappedSemester> MappedSemesters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=StatisticsWebMainDB;uid=root;password=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<University>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(d => d.University)
                  .WithMany(p => p.Departments);
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.HasOne(d => d.Department)
                  .WithMany(p => p.User);
            });
            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(d => d.Department)
                  .WithMany(p => p.Lesson);
            });
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Graded).IsRequired();
                entity.HasOne(d => d.Lesson)
                  .WithMany(p => p.Grades);
                entity.HasOne(d => d.User);
            });
            modelBuilder.Entity<MappedSemester>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Start).IsRequired();
                entity.Property(e => e.End).IsRequired();
                entity.HasOne(e => e.Department);
            });
        }
    }
}
