﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using SIMS_Project.Model;

//Code taken from project example 01.03.2023.
namespace SIMS_Project.Serializer
{
    class Serializer<T> where T : ISerializable, new()
    {
        private const char Delimiter = '|';

        public void ToCSV(string fileName, List<T> objects)
        {
            StringBuilder csv = new StringBuilder();

            foreach (T obj in objects)
            {
                string line = string.Join(Delimiter.ToString(), obj.ToCSV());
                csv.AppendLine(line);
            }

            File.WriteAllText(fileName, csv.ToString());

        }

        public List<T> FromCSV(string fileName)
        {
            List<T> objects = new List<T>();

            foreach (string line in File.ReadLines(fileName))
            {
                string[] csvValues = line.Split(Delimiter);
                T obj = new T();
                obj.FromCSV(csvValues);
                objects.Add(obj);
            }

            return objects;
        }

        public static implicit operator Serializer<T>(Serializer<AccommodationOwnerRate> v)
        {
            throw new NotImplementedException();
        }
    }
}
