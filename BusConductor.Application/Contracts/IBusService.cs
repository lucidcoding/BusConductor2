using BusConductor.Application.Requests.Bus;
using BusConductor.Domain.Common;

namespace BusConductor.Application.Contracts
{
    public interface IBusService
    {
        ValidationMessageCollection ValidateEdit(EditRequest request);
        void Edit(EditRequest request);
    }
}
