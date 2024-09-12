using BestBrightnesss.BusinessLogic.LogicInterface;
using BestBrightnesss.BusinessLogic.StaticMappings;
using BestBrightnesss.DataLogic.Profile;
using BestBrightnesss.DataLogic.Sales;
using BestBrightnesss.Repositories.RepositoryInterfaces;
using BestBrightnesss.ViewLogic;

namespace BestBrightnesss.BusinessLogic.Managers.ProfileMananger
{
    public class RegisterMananger : IProfileLogic
    {
        private readonly iProfile _iProfile;

        public RegisterMananger(iProfile iProfile)
        {
            _iProfile = iProfile;
        }

        public async Task<bool> IsUserAdmin(string email)
        {
            // Corrected here to use the instance variable _iProfile
            var userRole = await _iProfile.GetUserRole(email);
            return userRole != null && userRole.Role == "Administrator";
        }

        public async Task<bool> IsUser(string email)
        {
            // Corrected here to use the instance variable _iProfile
            var userRole = await _iProfile.GetUser(email);
            if (userRole == null)
            {
                return false;
            }
            return userRole != null;
        }

        public async Task Register(RegisterView profile, CancellationToken token = default)
        {
            var DBModel = MappingConfig.Mapper.Map<Register>(profile);
            await _iProfile.Register(DBModel, token);
        }
    }
}
