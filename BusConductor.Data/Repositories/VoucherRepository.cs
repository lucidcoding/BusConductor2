using System;
using System.Linq;
using BusConductor.Data.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.RepositoryContracts;

namespace BusConductor.Data.Repositories
{
    public class VoucherRepository : Repository<Voucher, Guid>, IVoucherRepository
    {
        public VoucherRepository(IContextProvider contextProvider) :
            base(contextProvider)
        {
        }

        public Voucher GetByCode(string code)
        {
            var voucher = Context.Vouchers
                .Where(x => x.Code == code)
                .SingleOrDefault();

            return voucher;
        }
    }
}
