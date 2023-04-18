using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Model;
using SIMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Application.UseCases
{
    public class TourRequestService
    {
        private static TourRequestService _instance;
        private ITourRequestRepository _tourRequestRepository;

        private TourRequestService()
        {
            _tourRequestRepository = Injector.Injector.CreateInstance<ITourRequestRepository>();
        }

        public static TourRequestService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TourRequestService();
            }
            return _instance;
        }

        public TourRequest AddRequest(TourRequest tourRequest)
        {
            return _tourRequestRepository.Add(tourRequest);
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll().ToList();
        }
    }
}
