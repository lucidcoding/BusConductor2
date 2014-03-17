using System;
using BusConductor.Data.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.RepositoryContracts;

namespace BusConductor.Data.Repositories
{
    public class EnquiryRepository : Repository<Enquiry, Guid>, IEnquiryRepository
    {
        public EnquiryRepository(IContextProvider contextProvider) :
            base(contextProvider)
        {
        }
    }
}
