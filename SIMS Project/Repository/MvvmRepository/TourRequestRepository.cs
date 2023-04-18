using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Application.UseCases;
using SIMS_Project.Model;
using SIMS_Project.Model.Enums;
using SIMS_Project.Repository.MvvmRepository;
using SIMS_Project.Repository.MvvmRepository.Interfaces;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository.MvvmRepository
{
    public class TourRequestRepository : ICsvSerialize<TourRequest>, ITourRequestRepository
    {
        private readonly string _filename = ResourcePath.DataPath + "tour-requests.csv";
        private readonly CsvSerializer _csvSerializer;
        private List<TourRequest> _tourRequests;

        public TourRequestRepository()
        {
            _csvSerializer = new CsvSerializer(_filename);
            _tourRequests  = new List<TourRequest>();
            Load();
        }

        public void Load()
        {
            IUserRepository userRepository = Injector.Injector.CreateInstance<IUserRepository>();
            ILocationRepository locationRepository = Injector.Injector.CreateInstance<ILocationRepository>();
            foreach (string row in _csvSerializer.LoadCsv())
            {
                TourRequest tourRequest = ObjectFromCsv(row);
                tourRequest.Guest2 = userRepository.GetById(tourRequest.Guest2Id);
                tourRequest.Location = locationRepository.GetById(tourRequest.LocationId);
                _tourRequests.Add(tourRequest);
            }
        }

        public void Save()
        {
            StringBuilder csvStringBuilder = new StringBuilder();
            foreach (TourRequest tourRequest in _tourRequests)
            {
                csvStringBuilder.AppendLine(_csvSerializer.CreateCsvRow(ObjectToCsv(tourRequest)));
            }
            _csvSerializer.SaveCsv(csvStringBuilder);
        }

        public TourRequest ObjectFromCsv(string row)
        {
            TourRequest tourRequest = new TourRequest();
            string[] values = _csvSerializer.ReadCsvRow(row);

            tourRequest.Id = int.Parse(values[0]);
            tourRequest.Guest2Id = int.Parse(values[1]);
            tourRequest.LocationId = int.Parse(values[2]);
            tourRequest.Description = values[3];
            tourRequest.Language = values[4];
            tourRequest.GuestNumber = int.Parse(values[5]);
            tourRequest.EarliestDate = DateOnly.Parse(values[6]);
            tourRequest.LatestDate = DateOnly.Parse(values[7]);
            tourRequest.Status = Enum.Parse<TourRequestStatus>(values[8]);

            return tourRequest;
        }

        public string[] ObjectToCsv(TourRequest entity)
        {
            string[] csvValues =
            {
                entity.Id.ToString(),
                entity.Guest2Id.ToString(),
                entity.LocationId.ToString(),
                entity.Description,
                entity.Language,
                entity.GuestNumber.ToString(),
                entity.EarliestDate.ToString(),
                entity.LatestDate.ToString(),
                entity.Status.ToString()
            };

            return csvValues;
        }

        private int NextId()
        {
            if (_tourRequests.Count != 0)
            {
                return _tourRequests.Max(a => a.Id) + 1;
            }
            else
            {
                return 0;
            }
        }

        public IEnumerable<TourRequest> GetAllValid()
        {
            return _tourRequests;
        }

        public IEnumerable<TourRequest> GetAll()
        {
            return _tourRequests;
        }

        public TourRequest GetById(int id)
        {
            return _tourRequests.Find(tr => tr.Id == id);
        }

        public TourRequest Add(TourRequest entity)
        {
            ILocationRepository locationRepository = Injector.Injector.CreateInstance<ILocationRepository>();
            entity.Id = NextId();
            entity.Location = locationRepository.Add(entity.Location);
            entity.LocationId = entity.Location.Id;
            _tourRequests.Add(entity);
            Save();

            return entity;
        }

        public TourRequest Update(TourRequest entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TourRequest entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
