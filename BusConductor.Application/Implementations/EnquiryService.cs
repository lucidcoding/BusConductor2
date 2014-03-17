using BusConductor.Application.Contracts;
using BusConductor.Application.ParameterSetMappers.Enquiry;
using BusConductor.Application.Requests.Enquiry;
using BusConductor.Domain.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.RepositoryContracts;
using Lucidity.Utilities.Contracts.Logging;

namespace BusConductor.Application.Implementations
{
    public class EnquiryService : IEnquiryService
    {
        private readonly IEnquiryRepository _enquiryRepository;
        private readonly IMakeParameterSetMapper _makeParameterSetMapper;
        private readonly ILog _log;

        public EnquiryService(
            IEnquiryRepository enquiryRepository,
            IMakeParameterSetMapper makeParameterSetMapper,
            ILog log)
        {
            _enquiryRepository = enquiryRepository;
            _makeParameterSetMapper = makeParameterSetMapper;
            _log = log;
        }

        public ValidationMessageCollection ValidateMake(MakeRequest request)
        {
            _log.Add(request);
            var parameterSet = _makeParameterSetMapper.Map(request);
            var validationMessages = Enquiry.ValidateMake(parameterSet);
            return validationMessages;
        }

        public void Make(MakeRequest request)
        {
            _log.Add(request);
            var parameterSet = _makeParameterSetMapper.Map(request);
            var enquiry = Enquiry.Make(parameterSet);
            _enquiryRepository.Save(enquiry);
        }
    }
}
