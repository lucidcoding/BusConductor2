using System;
using BusConductor.Domain.Common;

namespace BusConductor.Domain.Entities
{
    public class Voucher : Entity<Guid>
    {
        private string _code;
        private string _description;
        private decimal _discount;
        private DateTime? _expiryDate;

        public virtual string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public virtual decimal Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        public virtual DateTime? ExpiryDate
        {
            get { return _expiryDate; }
            set { _expiryDate = value; }
        }
    }
}
