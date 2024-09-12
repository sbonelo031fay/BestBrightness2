using Microsoft.EntityFrameworkCore;

namespace BestBrightnesss.DataLogic.Profile
{
    [Keyless]
    public class UserRole
    {
        public string Role { get; set; } = string.Empty;
    }
}
