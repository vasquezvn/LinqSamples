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
            //var query =
            //    from car in cars
            //    where car.Manufacturer == "BMW" && car.Year == 2016     // asi se filtra por mas de un criterio
            //    orderby car.Combined descending, car.Name ascending  // esta es la manera que aplicas mas de una manera de ordenar
            //    select car;


            var query =
                from car in cars
                where car.Manufacturer == "BMW" && car.Year == 2016     // asi se filtra por mas de un criterio
                orderby car.Combined descending, car.Name ascending  // esta es la manera que aplicas mas de una manera de ordenar
                select new 
                {
                    car.Manufacturer,
                    car.Name,
                    car.Combined
                };


            var result = cars.SelectMany(c => c.Name)
                .OrderBy(c => c);


            foreach (var character in result)
            {
                Console.WriteLine(character);
            }
            


            //var result = cars.Any(c => c.Manufacturer == "Ford");


            //cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
            //.OrderByDescending(c => c.Combined)
            //.ThenBy(c => c.Name)                                  // ThenBy nos permite aplicar un segundo criterio para ordenar los elementos
            //.Select(c => c)
            //.First();

            //cars
            //    .OrderByDescending(c => c.Combined)
            //    .ThenBy(c => c.Name)
            //    .Select(c => c)
            //    .FirstOrDefault(c => c.Manufacturer == "BMW" && c.Year == 2016); 

            Console.WriteLine(result);

            //foreach (var car in query.Take(10))
            //{
            //    Console.WriteLine($"{car.Manufacturer} {car.Name} : {car.Combined}");
            //}
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

                File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 1)
                .ToCar();


                //from line in File.ReadAllLines(path).Skip(1)
                //where line.Length > 1
                //select Car.ParseFromCsv(line);

            return query.ToList();

        }

        
    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}
