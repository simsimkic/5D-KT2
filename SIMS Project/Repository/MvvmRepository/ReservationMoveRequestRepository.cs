using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Model;
using SIMS_Project.Model.Enums;
using SIMS_Project.Repository.MvvmRepository.Interfaces;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository.MvvmRepository
{
    public class ReservationMoveRequestRepository : ICsvSerialize<ReservationMoveRequest>, IReservationMoveRequestRepository
    {
        private readonly string _filename = ResourcePath.DataPath + "reservation-move-request.csv";
        private readonly CsvSerializer _csvSerializer;
        private List<ReservationMoveRequest> _moveRequests;

        public ReservationMoveRequestRepository()
        {
            _csvSerializer = new CsvSerializer(_filename);
            _moveRequests = new List<ReservationMoveRequest>();
            Load();
        }

        public void Load()
        {
            IAccommodationReservationRepository reservationRepository = Injector.Injector.CreateInstance<IAccommodationReservationRepository>();
            foreach (string row in _csvSerializer.LoadCsv())
            {
                ReservationMoveRequest moveRequest = ObjectFromCsv(row);
                moveRequest.AccommodationReservation = reservationRepository.GetById(moveRequest.ReservationId);
                _moveRequests.Add(moveRequest);
            }
        }

        public void Save()
        {
            StringBuilder csvStringBuilder = new StringBuilder();
            foreach (ReservationMoveRequest moveRequest in _moveRequests)
            {
                csvStringBuilder.AppendLine(_csvSerializer.CreateCsvRow(ObjectToCsv(moveRequest)));
            }
            _csvSerializer.SaveCsv(csvStringBuilder);
        }

        public ReservationMoveRequest ObjectFromCsv(string row)
        {
            ReservationMoveRequest moveRequest = new ReservationMoveRequest();
            string[] values = _csvSerializer.ReadCsvRow(row);

            moveRequest.Id = int.Parse(values[0]);
            moveRequest.ReservationId = int.Parse(values[1]);
            moveRequest.Status = (ReservationMoveStatus)Enum.Parse(typeof(ReservationMoveStatus), values[2]);
            moveRequest.Changed = bool.Parse(values[3]);
            moveRequest.Handled = bool.Parse(values[4]);
            moveRequest.Comment = values[5];
            moveRequest.Start = DateOnly.Parse(values[6]);
            moveRequest.End = DateOnly.Parse(values[7]);

            return moveRequest;
        }

        public string[] ObjectToCsv(ReservationMoveRequest entity)
        {
            string[] csvValues =
            {
                entity.Id.ToString(),
                entity.ReservationId.ToString(),
                entity.Status.ToString(),
                entity.Changed.ToString(),
                entity.Handled.ToString(),
                entity.Comment.ToString(),
                entity.Start.ToString(),
                entity.End.ToString()
            };
            return csvValues;
        }

        public IEnumerable<ReservationMoveRequest> GetByGuestId(int id)
        {
            return _moveRequests.FindAll(r => r.AccommodationReservation.GuestId == id);
        }

        public IEnumerable<ReservationMoveRequest> GetByAccommodationId(int id)
        {
            return _moveRequests.FindAll(r => r.AccommodationReservation.AccomodationId == id);
        }

        public IEnumerable<ReservationMoveRequest> GetAllValid()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationMoveRequest> GetAll()
        {
            return _moveRequests;
        }

        public ReservationMoveRequest GetById(int id)
        {
            return _moveRequests.Find(r => r.Id == id);
        }

        private int NextId()
        {
            if (_moveRequests.Count != 0)
                return _moveRequests.Max(a => a.Id) + 1;
            else
                return 0;
        }

        public ReservationMoveRequest Add(ReservationMoveRequest entity)
        {
            entity.Id = NextId();
            _moveRequests.Add(entity);
            Save();
            return entity;
        }

        public ReservationMoveRequest Update(ReservationMoveRequest entity)
        {
            ReservationMoveRequest moveRequest = GetById(entity.Id);
            if (moveRequest != null)
            {
                moveRequest.AccommodationReservation = entity.AccommodationReservation;
                moveRequest.ReservationId = entity.ReservationId;
                moveRequest.Status = entity.Status;
                moveRequest.Changed = entity.Changed;
                moveRequest.Handled = entity.Handled;
                moveRequest.Comment = entity.Comment;
                moveRequest.Start = entity.Start;
                moveRequest.End = entity.End;
                Save();
                return moveRequest;
            }

            return null;
        }

        public void Delete(ReservationMoveRequest entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationMoveRequest> GetNotHandled()
        {
            return _moveRequests.FindAll(r => !r.Handled);
        }
    }
}
