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

namespace SIMS_Project.Repository.MvvmRepository
{
    public class AccommodationReviewRepository : ICsvSerialize<AccommodationReview>, IAccommodationReviewRepository
    {
        private readonly string _filename = ResourcePath.DataPath + "accommodation-reviews.csv";
        private readonly FileManager _fileManager;
        private readonly CsvSerializer _csvSerializer;
        private List<AccommodationReview> _reviews;

        public AccommodationReviewRepository()
        {
            _csvSerializer = new CsvSerializer(_filename);
            _fileManager = new FileManager();
            _reviews = new List<AccommodationReview>();

            Load();
        }

        public void Load()
        {
            IUserRepository userRepository = Injector.Injector.CreateInstance<IUserRepository>();
            IAccommodationReservationRepository reservationRepository = Injector.Injector.CreateInstance<IAccommodationReservationRepository>();
            foreach (string row in _csvSerializer.LoadCsv())
            {
                AccommodationReview review = ObjectFromCsv(row);
                review.Guest = userRepository.GetById(review.GuestId);
                review.Reservation = reservationRepository.GetById(review.ReservationId);
                _reviews.Add(review);
            }
        }

        public void Save()
        {
            StringBuilder csvStringBuilder = new StringBuilder();
            foreach (AccommodationReview review in _reviews)
            {
                csvStringBuilder.AppendLine(_csvSerializer.CreateCsvRow(ObjectToCsv(review)));
            }
            _csvSerializer.SaveCsv(csvStringBuilder);
        }

        public AccommodationReview ObjectFromCsv(string row)
        {
            AccommodationReview review = new AccommodationReview();
            string[] values = _csvSerializer.ReadCsvRow(row);

            review.Id = int.Parse(values[0]);
            review.GuestId = int.Parse(values[1]);
            review.ReservationId = int.Parse(values[2]);
            review.Clean = int.Parse(values[3]);
            review.Correct = int.Parse(values[4]);
            review.Comfort = int.Parse(values[5]);
            review.Location = int.Parse(values[6]);
            review.Comment = values[7];
            review.Images.AddRange(values[8].Split(","));

            return review;
        }

        public string[] ObjectToCsv(AccommodationReview entity)
        {
            string[] csvValues =
            {
                entity.Id.ToString(),
                entity.GuestId.ToString(),
                entity.ReservationId.ToString(),
                entity.Clean.ToString(),
                entity.Correct.ToString(),
                entity.Comfort.ToString(),
                entity.Location.ToString(),
                entity.Comment,
                entity.ConcatImages()
            };

            return csvValues;
        }

        public IEnumerable<AccommodationReview> GetAllValid()
        {
            return _reviews;
        }

        public IEnumerable<AccommodationReview> GetAll()
        {
            return _reviews;
        }

        public AccommodationReview GetById(int id)
        {
            return _reviews.Find(r => r.Id == id);
        }

        public AccommodationReview GetByReservationId(int id)
        {
            return _reviews.Find(r => r.ReservationId == id);
        }

        public IEnumerable<AccommodationReview> GetByAccommodationId(int id)
        {
            return _reviews.FindAll(r => r.Reservation.AccomodationId == id);
        }

        private int NextId()
        {
            if (_reviews.Count != 0)
                return _reviews.Max(a => a.Id) + 1;
            else
                return 0;
        }

        public AccommodationReview Add(AccommodationReview entity)
        {
            entity.Id = NextId();
            entity.Images = _fileManager.UploadImages(entity.Images, ResourcePath.AccommodationRate, entity.Id);
            _reviews.Add(entity);
            Save();
            return entity;
        }

        public AccommodationReview Update(AccommodationReview entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(AccommodationReview entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
