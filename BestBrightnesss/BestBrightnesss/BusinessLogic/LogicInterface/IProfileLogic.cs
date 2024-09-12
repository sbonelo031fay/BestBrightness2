using BestBrightnesss.DataLogic.Profile;
using BestBrightnesss.ViewLogic;

namespace BestBrightnesss.BusinessLogic.LogicInterface
{
    public interface IProfileLogic
    {
        Task Register(RegisterView profile, CancellationToken token = default);
        Task<bool> IsUserAdmin(string email);
        Task<bool> IsUser(string email);
    }
}
