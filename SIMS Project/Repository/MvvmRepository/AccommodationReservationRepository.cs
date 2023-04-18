using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Model;
using SIMS_Project.Repository.MvvmRepository.Interfaces;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository.MvvmRepository
{
    public class AccommodationReservationRepository : ICsvSerialize<AccommodationReservation>, IAccommodationReservationRepository
    {
        private readonly string _filename = ResourcePath.DataPath + "accommodation-reservations.csv";
        private readonly CsvSerializer _csvSerializer;
        private List<AccommodationReservation> _reservations;

        public AccommodationReservationRepository()
        {
            _csvSerializer = new CsvSerializer(_filename);
            _reservations = new List<AccommodationReservation>();

            Load();
        }

        public void Load()
        {
            IUserRepository userRepository = Injector.Injector.CreateInstance<IUserRepository>();
            IAccommodationRepository accommodationRepository = Injector.Injector.CreateInstance<IAccommodationRepository>();
            foreach (string row in _csvSerializer.LoadCsv())
            {
                AccommodationReservation reservation = ObjectFromCsv(row);
                reservation.Accommodation = accommodationRepository.GetById(reservation.AccomodationId);
                reservation.Guest = userRepository.GetById(reservation.GuestId);
                _reservations.Add(reservation);
            }
        }

        public void Save()
        {
            StringBuilder csvStringBuilder = new StringBuilder();
            foreach (AccommodationReservation reservation in _reservations)
            {
                csvStringBuilder.AppendLine(_csvSerializer.CreateCsvRow(ObjectToCsv(reservation)));
            }
            _csvSerializer.SaveCsv(csvStringBuilder);
        }

        public AccommodationReservation ObjectFromCsv(string row)
        {
            AccommodationReservation reservation = new AccommodationReservation();
            string[] values = _csvSerializer.ReadCsvRow(row);

            reservation.Id = int.Parse(values[0]);
            reservation.AccomodationId = int.Parse(values[1]);
            reservation.Start = DateOnly.Parse(values[2]);
            reservation.End = DateOnly.Parse(values[3]);
            reservation.NumberOfGuests = int.Parse(values[4]);
            reservation.Cancelled = Boolean.Parse(values[5]);
            reservation.GuestId = int.Parse(values[6]);

            return reservation;
        }

        public string[] ObjectToCsv(AccommodationReservation entity)
        {
            string[] csvValues = {
                entity.Id.ToString(),
                entity.AccomodationId.ToString(),
                entity.Start.ToString(),
                entity.End.ToString(),
                entity.NumberOfGuests.ToString(),
                entity.Cancelled.ToString(),
                entity.GuestId.ToString()
            };

            return csvValues;
        }

        public IEnumerable<AccommodationReservation> GetByAccommodationId(int id)
        {
            return _reservations.Where(r => r.AccomodationId == id && !r.Cancelled);
        }

        public IEnumerable<AccommodationReservation> GetAllValid()
        {
            return _reservations.Where(r => !r.Cancelled);
        }

        public IEnumerable<AccommodationReservation> GetAll()
        {
            return _reservations;
        }

        public AccommodationReservation GetById(int id)
        {
            return _reservations.Find(r => r.Id == id);
        }

        private int NextId()
        {
            if (_reservations.Count != 0)
                return _reservations.Max(r => r.Id) + 1;
            else
                return 0;
        }

        public AccommodationReservation Add(AccommodationReservation entity)
        {
            entity.Id = NextId();
            _reservations.Add(entity);
            Save();
            return entity;
        }

        public AccommodationReservation Update(AccommodationReservation entity)
        {
            AccommodationReservation reservation = GetById(entity.Id);
            if (reservation != null)
            {
                reservation.AccomodationId = entity.AccomodationId;
                reservation.Accommodation = entity.Accommodation;
                reservation.Start = entity.Start;
                reservation.End = entity.End;
                reservation.Cancelled = entity.Cancelled;
                reservation.NumberOfGuests = entity.NumberOfGuests;
                reservation.GuestId = entity.GuestId;
                reservation.Guest = entity.Guest;

                Save();
                return reservation;
            }

            return null;
        }

        public void BulkUpdate(IEnumerable<AccommodationReservation> entities) 
        {
            foreach (AccommodationReservation entity in entities)
            {
                AccommodationReservation reservation = GetById(entity.Id);
                if (reservation != null)
                {
                    reservation.AccomodationId = entity.AccomodationId;
                    reservation.Accommodation = entity.Accommodation;
                    reservation.Start = entity.Start;
                    reservation.End = entity.End;
                    reservation.Cancelled = entity.Cancelled;
                    reservation.NumberOfGuests = entity.NumberOfGuests;
                    reservation.GuestId = entity.GuestId;
                    reservation.Guest = entity.Guest;
                }
            }
            Save();
        }

        public void Delete(AccommodationReservation entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
