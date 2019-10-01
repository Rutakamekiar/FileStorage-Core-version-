using FileStorage.Implementation.DataAccess.RepositoryInterfaces;

namespace FileStorage.Implementation.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StorageContext _context;

        public UserRepository(StorageContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}