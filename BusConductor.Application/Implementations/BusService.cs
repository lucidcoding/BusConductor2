using BusConductor.Application.Contracts;
using BusConductor.Application.ParameterSetMappers.Bus;
using BusConductor.Application.Requests.Bus;
using BusConductor.Domain.Common;
using BusConductor.Domain.RepositoryContracts;
using Lucidity.Utilities.Contracts.Logging;

namespace BusConductor.Application.Implementations
{
    public class BusService : IBusService
    {
        private readonly IBusRepository _busRepository;
        private readonly IEditParameterSetMapper _editParameterSetMapper;
        private readonly ILog _log;

        public BusService(
            IBusRepository busRepository,
            IEditParameterSetMapper editParameterSetMapper,
            ILog log)
        {
            _busRepository = busRepository;
            _editParameterSetMapper = editParameterSetMapper;
            _log = log;
        }

        public ValidationMessageCollection ValidateEdit(EditRequest request)
        {
            _log.Add(request);
            var parameterSet = _editParameterSetMapper.Map(request);
            var bus = _busRepository.GetById(request.Id);
            var validationMessages = bus.ValidateEdit(parameterSet);
            return validationMessages;
        }

        public void Edit(EditRequest request)
        {
            _log.Add(request);
            var parameterSet = _editParameterSetMapper.Map(request);
            var bus = _busRepository.GetById(request.Id);
            bus.Edit(parameterSet);
            _busRepository.Update(bus);
        }
    }
}
