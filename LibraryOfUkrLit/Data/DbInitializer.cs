using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryOfUkrLit.Models;

namespace LibraryOfUkrLit.Data
{
    public class DbInitializer
    {
        public static void Initialize(Authorship context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Author.Any())
            {
                return;   // DB has been seeded
            }

            var authors = new Author[]
            {
            new Author{ID= 1,Information="Письменник, публіцист, гром. діяч, поет",LastName="Франко",Year=DateTime.Parse("1856-27-08")},
         
            };
            foreach (Author s in authors)
            {
                context.Author.Add(s);
            }
            context.SaveChanges();

            var proses = new Prose[]
            {
            new Prose{ProseID=1,Name="Перехресні стежки",Year=1904,Section=47},
           
            };
            foreach (Prose c in proses)
            {
                context.Prose.Add(c);
            }
            context.SaveChanges();

          

            var authorprose = new AuthorProse[]
            {
            new AuthorProse{AuthorProseID=1,ProseID=1,AuthorID = 1},
           
            };
            foreach (AuthorProse e in authorprose)
            {
                context.AuthorProse.Add(e);
            }
            context.SaveChanges();
        }
    }
}
