using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BestBrightnesss.DataLogic.Profile
{
    [Keyless]
    public class Register
    {
        public string Title { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
    }
}
