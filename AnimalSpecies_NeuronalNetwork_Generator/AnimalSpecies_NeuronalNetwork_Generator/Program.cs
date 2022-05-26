using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace AnimalSpecies_NeuronalNetwork_Generator
{
    public class Animal
    {
        [Index(0)]
        public double NumberOfLegs { get; set; }
        [Index(1)]
        public double CanFly { get; set; }
        [Index(2)]
        public double CanSwimm { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
            };
            var records = new List<Animal>();
            for (int i = 0; i < 100; i++)
            {
                records.Add(GenerateAnimal("human"));
                records.Add(GenerateAnimal("fish"));
                records.Add(GenerateAnimal("bird"));
            }

            using (var writer = new StreamWriter(@"C:\Users\pkubo\OneDrive\Dokumenty\GitHub\CSV_files_generators\CSV_Files\AnimalSpecies.csv"))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteHeader<Animal>();
                    csv.NextRecord();
                    foreach (var record in records)
                    {
                        csv.WriteRecord(record);
                        csv.NextRecord();
                    }
                }
            }



            Console.WriteLine("--------------------END---------------------");
        }

        static Animal GenerateAnimal(string type)
        {
            Animal animal = new Animal();
            Random r = new Random();

            if (type == "human")
            {
                animal.NumberOfLegs = 2 - r.Next(0,1);
                animal.CanFly       = Math.Min(r.NextDouble(), 0.2);
                animal.CanSwimm     = r.NextDouble();
            }
            if (type == "bird")
            {
                animal.NumberOfLegs = 2 - r.Next(0, 1);
                animal.CanFly       = Math.Max(r.NextDouble(), 0.7); ;
                animal.CanSwimm     = Math.Min(r.NextDouble(), 0.2); ;
            }
            if (type == "fish")
            {
                animal.NumberOfLegs = 0;
                animal.CanFly = Math.Min(r.NextDouble(), 0.2); ;
                animal.CanSwimm = Math.Max(r.NextDouble(), 0.9); ;
            }

            return animal;
        }
    }
}
