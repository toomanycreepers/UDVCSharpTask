using UssJuniorTest.Core;
using UssJuniorTest.Core.Models;
using UssJuniorTest.Infrastructure.Store;

namespace UssJuniorTest.Infrastructure.Repositories
{
    public class DriveLogRepository : IRepository<DriveLog>
    {
        private readonly IStore _store;

        public DriveLogRepository(IStore store)
        {
            _store = store;
        }

        /// <inheritdoc />
        public DriveLog? Get(long id)
        {
            return _store.GetAllDriveLogs().FirstOrDefault(x => x.Id == id);
        }

        /// <inheritdoc />
        public IEnumerable<DriveLog> GetAll()
        {
            return _store.GetAllDriveLogs();
        }
    }
}
