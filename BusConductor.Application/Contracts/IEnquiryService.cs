using BusConductor.Application.Requests.Enquiry;
using BusConductor.Domain.Common;

namespace BusConductor.Application.Contracts
{
    public interface IEnquiryService
    {
        ValidationMessageCollection ValidateMake(MakeRequest request);
        void Make(MakeRequest request);
    }
}
