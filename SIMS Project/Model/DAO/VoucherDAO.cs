using SIMS_Project.Controller;
using SIMS_Project.Repository;
using SIMS_Project.Resources;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model.DAO
{
    internal class VoucherDAO
    {
        private static VoucherDAO _instance;
        private List<Voucher> _vouchers;
        private readonly VoucherRepository _voucherRepository;
        private readonly UserController _userController;

        private VoucherDAO()
        {
            _voucherRepository = new VoucherRepository();
            _userController = UserController.GetInstance();

            _vouchers = _voucherRepository.Load();

            LoadGuests();

        }
        private void LoadGuests()
        {
            
            foreach (Voucher voucher in _vouchers) {
                User userFromId = _userController.GetById(voucher.Guest2Id);
                if (userFromId != null)
                {
                    voucher.Guest = userFromId; 
                }
            }
        }

        public static VoucherDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new VoucherDAO();
            }
            return _instance;

        }
        public int NextId()
        {
            if (_vouchers.Count != 0)
                return _vouchers.Max(t => t.Id) + 1;
            else
                return 0;
        }

        public List<Voucher> GetAll()
        {
            return _vouchers;
        }

        public Voucher GetById(int id)
        {
            return _vouchers.Find(v => v.Id == id);
        }

        public Voucher Add(Voucher voucher)
        {
            voucher.Id = NextId();
            voucher.Guest2Id = voucher.Guest.Id;
            _vouchers.Add(voucher);
            _voucherRepository.Save(_vouchers);

            return voucher;
        }
        public void Save()
        {
            _voucherRepository.Save(_vouchers);
        }
    }
}
