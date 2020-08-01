using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDb>());

            InsertData();
            QueryData();
        }

        private static void QueryData()
        {
            var db = new CarDb();
            //db.Database.Log = Console.WriteLine;

            //var query = from car in db.Cars
            //            orderby car.Combined descending, car.Name ascending
            //            select car;

            var query =
                 db.Cars.Where(c => c.Manufacturer == "BMW")
                        .OrderByDescending(c => c.Combined)
                        .ThenBy(c => c.Name)
                        .Take(10);

            foreach (var car in query)
            {
                Console.WriteLine($"{car.Name}: {car.Combined}");
            }
        }

        private static void InsertData()
        {
            var cars = ProcessCars("fuel.csv");
            var db = new CarDb();
            //db.Database.Log = Console.WriteLine;

            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Cars.Add(car);
                }
                db.SaveChanges();
            }
        }

        //static void Main(string[] args)
        //{
        //    CreateXml();
        //    QueryXml();


        //    //var manufacturers = ProcessManufacturers("manufacturers.csv");

        //    //var query =
        //    //    from car in cars
        //    //    group car by car.Manufacturer into carGroup
        //    //    select new
        //    //    {
        //    //        Name = carGroup.Key,
        //    //        Max = carGroup.Max(c => c.Combined),
        //    //        Min = carGroup.Min(c => c.Combined),
        //    //        Avg = carGroup.Average(c => c.Combined)
        //    //    } into result
        //    //    orderby result.Max descending
        //    //    select result;

        //    //var query2 =
        //    //    cars.GroupBy(c => c.Manufacturer)
        //    //        .Select(g =>
        //    //        {
        //    //            var results = g.Aggregate(new CarStatistics(),
        //    //                               (acc, c) => acc.Accumulate(c),
        //    //                               acc => acc.Compute());
        //    //            return new
        //    //            {
        //    //                Name = g.Key,
        //    //                Avg = results.Average,
        //    //                Min = results.Min,
        //    //                Max = results.Max
        //    //            };
        //    //        })
        //    //        .OrderByDescending(r => r.Max);

        //    //var query =
        //    //    from manufacturer in manufacturers
        //    //    join car in cars on manufacturer.Name equals car.Manufacturer
        //    //        into carGroup
        //    //    orderby manufacturer.Name
        //    //    select new
        //    //    {
        //    //        Manufacturer = manufacturer,
        //    //        Cars = carGroup
        //    //    } into result
        //    //    group result by result.Manufacturer.Headquarters;

        //    //var query2 =
        //    //    manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer,
        //    //    (m, g) => new
        //    //    {
        //    //        Manufacturer = m,
        //    //        Cars = g,
        //    //    })
        //    //    .GroupBy(m => m.Manufacturer.Headquarters);

        //    //var query =
        //    //    from car in cars
        //    //    group car by car.Manufacturer.ToUpper() into manufacturer
        //    //    orderby manufacturer.Key
        //    //    select manufacturer;

        //    //var query2 =
        //    //    cars.GroupBy(c => c.Manufacturer.ToUpper())
        //    //    .OrderBy(g => g.Key);


        //    // to print all manufacturesr with represent of two cars with better performance
        //    //foreach (var result in query2)
        //    //{
        //    //    Console.WriteLine($"{result.Name}");
        //    //    Console.WriteLine($"\t Max: {result.Max}");
        //    //    Console.WriteLine($"\t Min: {result.Min}");
        //    //    Console.WriteLine($"\t Avg: {result.Avg}");
        //    //}

        //    // to print all cars manufacturer and number of cars by each
        //    //foreach (var result in query)
        //    //{
        //    //    Console.WriteLine($"{result.Key} has {result.Count()} cars");
        //    //}

        //    //var cars2 = ProcessCars("fuel.csv");
        //    //var manufacturers = ProcessManufacturers("manufacturers.csv");

        //    //var query2 =
        //    //    from car in cars2
        //    //    join manufacture in manufacturers 
        //    //        on new { car.Manufacturer, car.Year } 
        //    //            equals 
        //    //            new { Manufacturer = manufacture.Name, manufacture.Year }
        //    //    orderby car.Combined descending, car.Name ascending
        //    //    select new
        //    //    { 
        //    //        manufacture.Headquarters,
        //    //        car.Name,
        //    //        car.Combined
        //    //    };

        //    //var query3 =
        //    //    cars2.Join(manufacturers,
        //    //        c => new { c.Manufacturer, c.Year },
        //    //        m => new { Manufacturer = m.Name, m.Year }, 
        //    //        (c, m) => new
        //    //        {
        //    //            m.Headquarters,
        //    //            c.Name,
        //    //            c.Combined
        //    //        })
        //    //    .OrderByDescending(c => c.Combined)
        //    //    .ThenBy(c => c.Name);


        //    //foreach (var car in query3.Take(10))
        //    //{
        //    //    Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
        //    //}


        //    //var cars = ProcessFile("fuel.csv");

        //    // METHOD SYNTAX
        //    //var query = cars.OrderByDescending(c => c.Combined)
        //    //                .ThenBy(c => c.Name);   // permite agregar una segunda manera de ordernar 

        //    // QUERY SYNTAX
        //    //var query =
        //    //    from car in cars
        //    //    where car.Manufacturer == "BMW" && car.Year == 2016     // asi se filtra por mas de un criterio
        //    //    orderby car.Combined descending, car.Name ascending  // esta es la manera que aplicas mas de una manera de ordenar
        //    //    select car;


        //    //var query =
        //    //    from car in cars
        //    //    where car.Manufacturer == "BMW" && car.Year == 2016     // asi se filtra por mas de un criterio
        //    //    orderby car.Combined descending, car.Name ascending  // esta es la manera que aplicas mas de una manera de ordenar
        //    //    select new
        //    //    {
        //    //        car.Manufacturer,
        //    //        car.Name,
        //    //        car.Combined
        //    //    };


        //    //var result = cars.SelectMany(c => c.Name)
        //    //    .OrderBy(c => c);


        //    //foreach (var character in result)
        //    //{
        //    //    Console.WriteLine(character);
        //    //}



        //    //var result = cars.Any(c => c.Manufacturer == "Ford");


        //    //cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
        //    //.OrderByDescending(c => c.Combined)
        //    //.ThenBy(c => c.Name)                                  // ThenBy nos permite aplicar un segundo criterio para ordenar los elementos
        //    //.Select(c => c)
        //    //.First();

        //    //cars
        //    //    .OrderByDescending(c => c.Combined)
        //    //    .ThenBy(c => c.Name)
        //    //    .Select(c => c)
        //    //    .FirstOrDefault(c => c.Manufacturer == "BMW" && c.Year == 2016); 

        //    //Console.WriteLine(result);

        //    //foreach (var car in query.Take(10))
        //    //{
        //    //    Console.WriteLine($"{car.Manufacturer} {car.Name} : {car.Combined}");
        //    //}
        //}

        private static void QueryXml()
        {
            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";

            var document = XDocument.Load("fuel.xml");

            var query =
                from element in document.Element(ns + "Cars")?.Elements(ex + "Car") 
                                                        ?? Enumerable.Empty<XElement>()
                where element.Attribute("Manufacturer")?.Value == "BMW"
                select element.Attribute("Name").Value;

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        private static void CreateXml()
        {
            var records = ProcessCars("fuel.csv");

            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";

            var document = new XDocument();

            var cars = new XElement(ns + "Cars",
                from record in records
                select new XElement(ex + "Car",
                        new XAttribute("Name", record.Name),
                        new XAttribute("Combined", record.Combined),
                        new XAttribute("Manufacturer", record.Manufacturer))
                );

            cars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));

            document.Add(cars);
            document.Save("fuel.xml");
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query = File.ReadLines(path)
                .Where(l => l.Length > 1)
                .Select( l =>
                {
                    var columns = l.Split(',');
                    return new Manufacturer
                    { 
                        Name = columns[0],
                        Headquarters = columns[1],
                        Year = int.Parse(columns[2])
                    };
                });

            return query.ToList();
        }

        private static List<Car> ProcessCars(string path)
        {
            var query = File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 1)
                .ToCar();

            return query.ToList();
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

    public class CarStatistics
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public double Average { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }

        public CarStatistics()
        {
            Max = Int32.MinValue;
            Min = Int32.MaxValue;
        }

        public CarStatistics Accumulate(Car car)
        {
            Count += 1;
            Total += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);

            return this;
        }

        public CarStatistics Compute()
        {
            Average = Total / Count;

            return this;
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
