using SIMS_Project.Repository.MvvmRepository.Interfaces;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository.MvvmRepository
{
    public class CsvSerializer
    {
        private const char _delimiter = '|';
        private readonly string _filename;

        public CsvSerializer(string filename)
        {
            _filename = filename;
        }

        public string CreateCsvRow(string[] values)
        {
            return string.Join(_delimiter.ToString(), values);
        }

        public string[] ReadCsvRow(string row)
        {
            return row.Split(_delimiter);
        }

        public string[] LoadCsv()
        {
            return File.ReadLines(_filename).ToArray();
        }

        public void SaveCsv(StringBuilder rows)
        {
            File.WriteAllText(_filename, rows.ToString());
        }
    }
}
