using BusConductor.Application.Requests.Enquiry;
using BusConductor.Domain.ParameterSets.Enquiry;
using BusConductor.Domain.RepositoryContracts;
using Lucidity.Utilities;

namespace BusConductor.Application.ParameterSetMappers.Enquiry
{
    public class MakeParameterSetMapper : BusConductor.Application.ParameterSetMappers.Enquiry.IMakeParameterSetMapper
    {
        private readonly IBusRepository _busRepository;
        private readonly IUserRepository _userRepository;

        public MakeParameterSetMapper(
            IBusRepository busRepository,
            IUserRepository userRepository)
        {
            _busRepository = busRepository;
            _userRepository = userRepository;
        }

        public MakeParameterSet Map(MakeRequest request)
        {
            var parameterSet = PropertyMapper.MapMatchingProperties<MakeRequest, MakeParameterSet>(request);
            parameterSet.Bus = _busRepository.GetById(request.BusId);
            parameterSet.CurrentUser = _userRepository.GetByUsername("Application");
            return parameterSet;
        }
    }
}
