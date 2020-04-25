using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFile("fuel.csv");

            // METHOD SYNTAX
            //var query = cars.OrderByDescending(c => c.Combined)
            //                .ThenBy(c => c.Name);   // permite agregar una segunda manera de ordernar 

            // QUERY SYNTAX
            var query =
                from car in cars
                where car.Manufacturer == "BMW" && car.Year == 2016     // asi se filtra por mas de un criterio
                orderby car.Combined descending, car.Name ascending  // esta es la manera que aplicas mas de una manera de ordenar
                select car;

            var query2 =
                cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name)
                .Select(c => c);


            foreach (var car in query2.Take(10))
            {
                Console.WriteLine($"{car.Manufacturer} {car.Name} : {car.Combined}");
            }
        }

        private static List<Car> ProcessFile(string path)
        {
            // METHOD SYNTAX
            //return
            //File.ReadAllLines(path)
            //    .Skip(1)
            //    .Where(line => line.Length > 1)
            //    .Select(Car.ParseFromCsv)
            //    .ToList();


            // QUERY SYNTAX
            var query =

                from line in File.ReadAllLines(path).Skip(1)
                where line.Length > 1
                select Car.ParseFromCsv(line);

            return query.ToList();

        }
    }
}
