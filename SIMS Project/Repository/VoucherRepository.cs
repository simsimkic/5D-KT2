using SIMS_Project.Model;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository
{
    public class VoucherRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "vouchers.csv";
        private readonly Serializer<Voucher> _serializer;


        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
        }

        public List<Voucher> Load()
        {
            return _serializer.FromCSV(_filePath);

        }

        public void Save(List<Voucher> vouchers)
        {
            _serializer.ToCSV(_filePath, vouchers);
        }
    }
}
