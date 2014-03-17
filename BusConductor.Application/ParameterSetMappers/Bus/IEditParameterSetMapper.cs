using BusConductor.Application.Requests.Bus;
using BusConductor.Domain.ParameterSets.Bus;

namespace BusConductor.Application.ParameterSetMappers.Bus
{
    public interface IEditParameterSetMapper
    {
        EditParameterSet Map(EditRequest request);
    }
}
