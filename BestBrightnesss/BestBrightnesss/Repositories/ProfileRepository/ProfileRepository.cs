using BestBrightnesss.DataLogic;
using BestBrightnesss.DataLogic.Profile;
using BestBrightnesss.Repositories.GenericRepository;
using BestBrightnesss.Repositories.RepositoryInterfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BestBrightnesss.Repositories.ProfileRepository
{
    public class ProfileRepository: GenericRepository<Register>, iProfile
    {
        private DefaultContext _context;
        public ProfileRepository(DefaultContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }

        public async Task Register(Register profile, CancellationToken token = default)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Title", profile.Title),
                new SqlParameter("@FirstName", profile.FirstName),
                new SqlParameter("@LastName",profile.LastName),
                new SqlParameter("@Email", profile.Email),
                new SqlParameter("@ContactNumber", profile.ContactNumber),
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC [Register] @Title, @FirstName, @LastName, @Email, @ContactNumber", parameters, token);
            await _context.SaveChangesAsync(token);
        }
        public async Task<UserRole?> GetUserRole(string email, CancellationToken token = default)
        {
            object[] parameters =
            {
                new SqlParameter("@email", email)
            };
            const string query = "EXEC [GetUserRole] @email";
            var queryResult = _context.Set<UserRole>().FromSqlRaw(query, parameters).AsAsyncEnumerable();
            return await queryResult.FirstOrDefaultAsync(token);
        }

        public async Task<UserRole?> GetUser(string email, CancellationToken token = default)
        {
            object[] parameters =
            {
                new SqlParameter("@email", email)
            };
            const string query = "EXEC [GetUser] @email";
            var queryResult = _context.Set<UserRole>().FromSqlRaw(query, parameters).AsAsyncEnumerable();
            return await queryResult.FirstOrDefaultAsync(token);
        }
    }
}
