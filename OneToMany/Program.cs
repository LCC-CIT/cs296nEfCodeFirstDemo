using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneToMany
{
    class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }

    class County
    {
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public List<City> Cities { get; set; }
    }

    class OneToManyDbContext : DbContext
    {
        public DbSet<County> Counties { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            AddCountyAndCities();
            ShowCities("Lane");
        }

        static void AddCountyAndCities()
        {
            OneToManyDbContext context = new OneToManyDbContext();

            County lane = new County() { CountyName = "Lane" };

            lane.Cities= new List<City>();
            lane.Cities.Add(new City() { CityName = "Springfield" });
            lane.Cities.Add(new City() { CityName = "Eugene" });

            context.Counties.Add(lane);
            context.SaveChanges();
        }

        static void ShowCities(string countyName)
        {
            OneToManyDbContext context = new OneToManyDbContext();
            County county = (from c in context.Counties 
                             where c.CountyName == countyName 
                             select c).Include(c => c.Cities).FirstOrDefault();
            foreach (City city in county.Cities)
                Console.WriteLine(city.CityName);
        }
    }
}
