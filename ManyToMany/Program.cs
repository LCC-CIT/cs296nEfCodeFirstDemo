using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyToMany
{
    class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public List<Club> Clubs { get; set; }
    }

    class Club
    {
        public int ClubId { get; set; }
        public string Name { get; set; }
        public List<Person> People { get; set; }
    }

    class ManyToManyDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            AddPeopleAndClubs();
            ShowClubs("Brian");
        }

        static void AddPeopleAndClubs()
        {
            ManyToManyDbContext context = new ManyToManyDbContext();

            Person brian = new Person() { Name = "Brian" };
            brian.Clubs = new List<Club>();

            Club eadnug = new Club() { Name = "Eugene Area DotNet User's Group" };
            brian.Clubs.Add(eadnug);
            Club wna = new Club() { Name = "Washburne Neighborhood Association" };
            brian.Clubs.Add(wna);

            Person amanda = new Person() { Name = "Amanda" };
            amanda.Clubs = new List<Club>();
            amanda.Clubs.Add(wna);

            Club nanowrimo = new Club() { Name = "National Novel Writing Month" };
            amanda.Clubs.Add(nanowrimo);

            context.People.Add(brian);
            context.People.Add(amanda);
            context.SaveChanges();
        }

        static void ShowClubs(string personName)
        {
            ManyToManyDbContext context = new ManyToManyDbContext();
            Person person = (from p in context.People
                             where p.Name == personName
                             select p).Include(p => p.Clubs).FirstOrDefault();
            foreach (Club club in person.Clubs)
                Console.WriteLine(club.Name);
        }
    }
}
