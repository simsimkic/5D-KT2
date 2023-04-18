using SIMS_Project.Controller;
using SIMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model.DAO
{
    internal class KeyPointDAO
    {
        private static KeyPointDAO _instance;
        private List<KeyPoint> _keypoints;
        private readonly KeyPointRepository _keyPointRepository; 
        private readonly TourController _tourController;
        private readonly TourReservationController _tourReservationController;


        private KeyPointDAO() { 
            _keyPointRepository = new KeyPointRepository();
            _tourController = TourController.GetInstance();
            _tourReservationController= TourReservationController.GetInstance(); 
            _keypoints = _keyPointRepository.Load();

            LoadTour();
           
        
        }

        private void LoadTour() { 
            foreach(KeyPoint keypoint in _keypoints)
            {
                Tour tourFromId = _tourController.GetById(keypoint.TourID);
                if (tourFromId != null) 
                {
                    keypoint.Tour = tourFromId;
                    tourFromId.KeyPoints.Add(keypoint); 
                }

            }
        }


        
        public static KeyPointDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new KeyPointDAO();
            }
            return _instance;

        }
        public int NextId()
        {
            if (_keypoints.Count != 0)
                return _keypoints.Max(k => k.Id) + 1;
            else
                return 0;
        }

        public List<KeyPoint> GetAll()
        {
            return _keypoints;
        }

        public KeyPoint GetById(int id)
        {
            return _keypoints.Find(k => k.Id == id);
        }

        public KeyPoint Add(KeyPoint keypoint)
        {
            keypoint.Id = NextId();
           
            _keypoints.Add(keypoint);
            _keyPointRepository.Save(_keypoints);

            return keypoint;
        }
        public void Save() 
        {
            _keyPointRepository.Save(_keypoints); 
        }
    }
}
