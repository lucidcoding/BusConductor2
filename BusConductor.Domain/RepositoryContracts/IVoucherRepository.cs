using System;
using BusConductor.Domain.Common;
using BusConductor.Domain.Entities;

namespace BusConductor.Domain.RepositoryContracts
{
    public interface IVoucherRepository : IRepository<Voucher, Guid>
    {
        Voucher GetByCode(string code);
    }
}
