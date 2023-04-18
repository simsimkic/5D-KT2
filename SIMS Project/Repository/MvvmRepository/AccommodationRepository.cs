using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Model;
using SIMS_Project.Repository.MvvmRepository.Interfaces;
using SIMS_Project.Resources;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Injector;

namespace SIMS_Project.Repository.MvvmRepository
{
    public class AccommodationRepository : ICsvSerialize<Accommodation>, IAccommodationRepository
    {
        private readonly string _filename = ResourcePath.DataPath + "accommodations.csv";
        private readonly FileManager _fileManager;
        private readonly CsvSerializer _csvSerializer;
        private List<Accommodation> _accommodations;

        public AccommodationRepository()
        {
            _csvSerializer = new CsvSerializer(_filename);
            _fileManager = new FileManager();
            _accommodations = new List<Accommodation>();

            Load();
        }

        public void Load()
        {
            ILocationRepository locationRepository = Injector.Injector.CreateInstance<ILocationRepository>();
            IUserRepository userRepository = Injector.Injector.CreateInstance<IUserRepository>();
            foreach (string row in _csvSerializer.LoadCsv())
            {
                Accommodation accommodation = ObjectFromCsv(row);
                accommodation.Location = locationRepository.GetById(accommodation.LocationId);
                accommodation.Owner = userRepository.GetById(accommodation.OwnerId);
                _accommodations.Add(accommodation);
            }
        }
        public void Save()
        {
            StringBuilder csvStringBuilder = new StringBuilder();
            foreach (Accommodation accommodation in _accommodations)
            {
                csvStringBuilder.AppendLine(_csvSerializer.CreateCsvRow(ObjectToCsv(accommodation)));
            }
            _csvSerializer.SaveCsv(csvStringBuilder);
        }

        public Accommodation ObjectFromCsv(string row)
        {
            Accommodation accommodation = new Accommodation();
            string[] values = _csvSerializer.ReadCsvRow(row);

            accommodation.Id = int.Parse(values[0]);
            accommodation.Name = values[1];
            accommodation.OwnerId = int.Parse(values[2]);
            accommodation.LocationId = int.Parse(values[3]);
            accommodation.Type = Accommodation.ParseType(values[4]);
            accommodation.MaxGuests = int.Parse(values[5]);
            accommodation.MinDays = int.Parse(values[6]);
            accommodation.DaysToCancel = int.Parse(values[7]);
            accommodation.Images.AddRange(values[8].Split(","));

            return accommodation;
        }

        public string[] ObjectToCsv(Accommodation entity)
        {
            string[] csvValues = {
                entity.Id.ToString(),
                entity.Name,
                entity.OwnerId.ToString(),
                entity.LocationId.ToString(),
                entity.Type.ToString(),
                entity.MaxGuests.ToString(),
                entity.MinDays.ToString(),
                entity.DaysToCancel.ToString(),
                entity.ConcatImages()
            };

            return csvValues;
        }

        public IEnumerable<Accommodation> GetAllValid()
        {
            return _accommodations; // Add deleted field to accommodations
        }

        public IEnumerable<Accommodation> GetAll()
        {
            return _accommodations;
        }

        public Accommodation GetById(int id)
        {
            return _accommodations.Find(a => a.Id == id);
        }

        private int NextId()
        {
            if (_accommodations.Count != 0)
                return _accommodations.Max(a => a.Id) + 1;
            else
                return 0;
        }

        public Accommodation Add(Accommodation entity)
        {
            ILocationRepository locationRepository = Injector.Injector.CreateInstance<ILocationRepository>();
            entity.Id = NextId();
            entity.Images = _fileManager.UploadImages(entity.Images, ResourcePath.AccommodationMediaPath, entity.Id);
            entity.Location = locationRepository.Add(entity.Location);
            entity.LocationId = entity.Location.Id;
            _accommodations.Add(entity);
            Save();
            return entity;
        }

        public Accommodation Update(Accommodation entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Accommodation entity)
        {
            throw new NotImplementedException(); // Add deleted field to accommodations
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException(); // Add deleted field to accommodations
        }

        public IEnumerable<Accommodation> GetByOwnerId(int id)
        {
            return _accommodations.FindAll(a => a.OwnerId == id);
        }

        public IEnumerable<Accommodation> QueryByContaingName(string queryParameter)
        {
            return _accommodations.Where(acc => acc.Name.ToLower().Contains(queryParameter.ToLower()));
        }

        public IEnumerable<Accommodation> QueryByContaingLocation(string queryParameter)
        {
            return _accommodations.Where(acc => acc.Location.City.ToLower().Contains(queryParameter.ToLower()) || acc.Location.Country.ToLower().Contains(queryParameter.ToLower()));
        }

        public IEnumerable<Accommodation> QueryByContaingType(string queryParameter)
        {
            return _accommodations.Where(acc => acc.Type.ToString().ToLower().Contains(queryParameter.ToLower()));
        }

        public IEnumerable<Accommodation> QueryByNumberOfGuests(int queryParameter)
        {
            return _accommodations.Where(acc => acc.MaxGuests >= queryParameter);
        }

        public IEnumerable<Accommodation> QueryByNumberOfDays(int queryParameter)
        {
            return _accommodations.Where(acc => acc.MinDays <= queryParameter);
        }
    }
}
