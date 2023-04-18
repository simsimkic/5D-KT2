using SIMS_Project.Model.DAO;
using SIMS_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Controller
{
    public class VoucherController
    {
        private static VoucherController _instance;
        private readonly VoucherDAO _dao;

        private VoucherController()
        {
            _dao = VoucherDAO.GetInstance();

        }

        public static VoucherController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new VoucherController();
            }
            return _instance;
        }

        public List<Voucher> GetAll()
        {
            return _dao.GetAll();
        }

        public Voucher Add(Voucher voucher)
        {
            return _dao.Add(voucher);
        }
        public Voucher GetById(int id)
        {
            return _dao.GetById(id);
        }
        public void Save()
        {
            _dao.Save();
        }
    }
}
