using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryOfUkrLit.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryOfUkrLit.Data
{
    public class Authorship : DbContext
    {
        public Authorship(DbContextOptions<Authorship> options) : base(options)
        {
        }

        public DbSet<Author> Author { get; set; }
        public DbSet<AuthorProse> AuthorProse { get; set; }
        public DbSet<Prose> Prose { get; set; }
        public DbSet<Poem> Poem { get; set; }
        public DbSet<AuthorPoem> AuthorPoem { get; set; }
        public DbSet<Books> Book { get; set; }
        public DbSet<Publishers> Publisher { get; set; }
        public DbSet<BookPoems> BookPoem { get; set; }
        public DbSet<BookProse> BookProse { get; set; }
        public DbSet<PublisherBook> PublisherBook { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<AuthorProse>().ToTable("AuthorProse");
            modelBuilder.Entity<Prose>().ToTable("Prose");
            modelBuilder.Entity<AuthorPoem>().ToTable("AuthorPoem");
            modelBuilder.Entity<Poem>().ToTable("Poem");
            modelBuilder.Entity<Books>().ToTable("Books");
            modelBuilder.Entity<BookPoems>().ToTable("BookPoems");
            modelBuilder.Entity<BookProse>().ToTable("BookProse");
            modelBuilder.Entity<Publishers>().ToTable("Publishers");
            modelBuilder.Entity<PublisherBook>().ToTable("PublisherBook");
        }
       // public DbSet<LibraryOfUkrLit.Models.Poem> Poems { get; set; }
    }
}