using BestBrightnesss.DataLogic.Profile;
using BestBrightnesss.DataLogic.Sales;

namespace BestBrightnesss.Repositories.RepositoryInterfaces
{
    public interface iProfile
    {
        Task Register(Register profile, CancellationToken token = default);
        Task<UserRole?> GetUserRole(string email, CancellationToken token = default);
        Task<UserRole?> GetUser(string email, CancellationToken token = default);
    }
}
