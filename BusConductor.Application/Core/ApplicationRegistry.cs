using BusConductor.Application.ParameterSetMappers.Booking;
using BusConductor.Application.Contracts;
using BusConductor.Application.Implementations;
using BusConductor.Application.ParameterSetMappers.Bus;
using BusConductor.Data.Core;
using StructureMap.Configuration.DSL;
using BusConductor.Application.ParameterSetMappers.Enquiry;

namespace BusConductor.Application.Core
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            Configure(x =>
            {
                For<IUserService>().Use<UserService>();
                For<ITaskService>().Use<TaskService>();
                For<IBookingService>().Use<BookingService>();
                For<IBusService>().Use<BusService>();
                For<IEnquiryService>().Use<EnquiryService>();
                For<ICustomerMakeParameterSetMapper>().Use<CustomerMakeParameterSetMapper>();
                For<IAdminMakeParameterSetMapper>().Use<AdminMakeParameterSetMapper>();
                For<IEditParameterSetMapper>().Use<EditParameterSetMapper>();
                For<IMakeParameterSetMapper>().Use<MakeParameterSetMapper>();
                //For<ILog>().Use(LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}
