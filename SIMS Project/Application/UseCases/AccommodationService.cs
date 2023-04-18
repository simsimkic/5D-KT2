using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Model;
using SIMS_Project.Resources;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Application.UseCases
{
    public class AccommodationService
    {
        private static AccommodationService _instance;
        private readonly IAccommodationRepository _repository;

        private AccommodationService()
        {
            _repository = Injector.Injector.CreateInstance<IAccommodationRepository>();
        }

        public static AccommodationService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AccommodationService();
            }

            return _instance;
        }

        public List<Accommodation> GetAllValid()
        {
            return _repository.GetAllValid().ToList();
        }

        public Accommodation GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<Accommodation> GetByOwnerId(int id)
        {
            return _repository.GetByOwnerId(id).ToList();
        }

        public Accommodation RegisterNew(Accommodation accommodation)
        {
            return _repository.Add(accommodation);
        }

        public IEnumerable<Accommodation> SearchAccomodations(string searchValue, string searchType)
        {
            if (searchValue == null)
            {
                return Enumerable.Empty<Accommodation>();
            }
            try
            {
                if (searchType == "Name" && searchValue != "")
                {
                    return _repository.QueryByContaingName(searchValue.Trim());
                }
                else if (searchType == "Location" && searchValue != "")
                {
                    return _repository.QueryByContaingLocation(searchValue.Trim());
                }
                else if (searchType == "Type" && searchValue != "")
                {
                    return _repository.QueryByContaingType(searchValue.Trim());
                }
                else if (searchType == "Number of guests")
                {
                    return _repository.QueryByNumberOfGuests(Convert.ToInt32(searchValue));
                }
                else if (searchType == "Number of days")
                {
                    return _repository.QueryByNumberOfDays(Convert.ToInt32(searchValue));
                }

            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Accommodation>();
            }

            return Enumerable.Empty<Accommodation>();
        }
    }
}
