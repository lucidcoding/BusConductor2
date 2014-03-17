using System.Collections.Generic;
using BusConductor.Application.Requests.Bus;
using BusConductor.Domain.ParameterSets.Bus;
using BusConductor.Domain.RepositoryContracts;

namespace BusConductor.Application.ParameterSetMappers.Bus
{
    public class EditParameterSetMapper : IEditParameterSetMapper
    {
        private readonly IUserRepository _userRepository;

        public EditParameterSetMapper(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public EditParameterSet Map(EditRequest request)
        {
            var parameterSet = new EditParameterSet();
            parameterSet.Name = request.Name;
            parameterSet.Description = request.Description;
            parameterSet.Overview = request.Overview;
            parameterSet.Details = request.Details;
            parameterSet.DriveSide = request.DriveSide;
            parameterSet.Berth = request.Berth;
            parameterSet.Year = request.Year;
            parameterSet.PricingPeriods = new List<EditPricingPeriodParameterSet>();
            parameterSet.CurrentUser = _userRepository.GetByUsername("Application");

            foreach(var editPricingPeriodRequest in request.PricingPeriods)
            {
                var editPricingPeriodParameterSet = new EditPricingPeriodParameterSet();
                editPricingPeriodParameterSet.Id = editPricingPeriodRequest.Id;
                editPricingPeriodParameterSet.StartMonth = editPricingPeriodRequest.StartMonth;
                editPricingPeriodParameterSet.StartDay = editPricingPeriodRequest.StartDay;
                editPricingPeriodParameterSet.EndMonth = editPricingPeriodRequest.EndMonth;
                editPricingPeriodParameterSet.EndDay = editPricingPeriodRequest.EndDay;
                editPricingPeriodParameterSet.FridayToFridayRate = editPricingPeriodRequest.FridayToFridayRate;
                editPricingPeriodParameterSet.FridayToMondayRate = editPricingPeriodRequest.FridayToMondayRate;
                editPricingPeriodParameterSet.MondayToFridayRate = editPricingPeriodRequest.MondayToFridayRate;
                editPricingPeriodParameterSet.CurrentUser = _userRepository.GetByUsername("Application");
                parameterSet.PricingPeriods.Add(editPricingPeriodParameterSet);
            }

            return parameterSet;
        }
    }
}
