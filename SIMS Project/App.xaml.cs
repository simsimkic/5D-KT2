using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Model.DAO;
using SIMS_Project.Repository.MvvmRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {
            Injector.Injector.Implementations.Add(typeof(IUserRepository), new UserRepository());
            Injector.Injector.Implementations.Add(typeof(ILocationRepository), new LocationRepository());
            Injector.Injector.Implementations.Add(typeof(IAccommodationRepository), new AccommodationRepository());
            Injector.Injector.Implementations.Add(typeof(IAccommodationReservationRepository), new AccommodationReservationRepository());
            Injector.Injector.Implementations.Add(typeof(IAccommodationReviewRepository), new AccommodationReviewRepository());
            Injector.Injector.Implementations.Add(typeof(IReservationMoveRequestRepository), new ReservationMoveRequestRepository());
            Injector.Injector.Implementations.Add(typeof(ITourRequestRepository), new TourRequestRepository());

            //Remove ASAP
            AccommodationReservationDAO.GetInstance();
            AccommodationOwnerRateDAO.GetInstance().LoadAccommodationOwnerRates();
            ReservationMoveRequestDAO.GetInstance().LoadRequests();
            ReservationCancelNotificationDAO.GetInstance().LoadReservations();
        }
    }
}
