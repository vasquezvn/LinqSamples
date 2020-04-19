using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {

            var numbers = MyLinq.Random().Where(n => n > 0.5).Take(10);

            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            var movies = new List<Movie>
            {
                new Movie { Title = "The Dark knight",      Rating = 8.9f, Year = 2019 },
                new Movie { Title = "The King's Speech",    Rating = 8.0f, Year = 2020 },
                new Movie { Title = "CasaBlanca",           Rating = 8.5f, Year = 1942 },
                new Movie { Title = "Star Wars V",          Rating = 8.7f, Year = 1995 } 
            };

            //var query = Enumerable.Empty<Movie>();

            //var query = movies.Where(m => m.Year > 2001);

            //var query = movies.Where(m => m.Year > 2001)
            //    .OrderByDescending(m => m.Rating);

            //foreach (var movie in query)
            //{
            //    Console.WriteLine(movie.Title);
            //}

            var query = from movie in movies
                        where movie.Year > 2000
                        orderby movie.Rating descending
                        select movie;

            var enumerator = query.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }
        }
    }
}
