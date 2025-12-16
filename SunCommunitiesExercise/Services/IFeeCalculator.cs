using SunCommunitiesExercise.Models;

namespace SunCommunitiesExercise.Services
{
    public interface IFeeCalculator
    {
        FeeResult Calculate(decimal amount, bool preferredCustomer);
    }
}
