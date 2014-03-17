using System;

namespace BusConductor.Application.ParameterSetMappers.Enquiry
{
    public interface IMakeParameterSetMapper
    {
        BusConductor.Domain.ParameterSets.Enquiry.MakeParameterSet Map(BusConductor.Application.Requests.Enquiry.MakeRequest request);
    }
}
