using HendersonvilleTrafficTest.Services.Interfaces;
using HendersonvilleTrafficTest.Services.Models;

namespace HendersonvilleTrafficTest.Services
{
    public class DatabaseDataAccessService : IDataAccessService
    {
        public Task<TestSequenceStep[]> GetTestSequenceAsync(string sequenceId)
        {
            // TODO: Implement database access for test sequences
            throw new NotImplementedException("Database data access not yet implemented");
        }

        public Task<string[]> GetAllTestSequencesAsync()
        {
            // TODO: Implement database access for test sequence IDs
            throw new NotImplementedException("Database data access not yet implemented");
        }
    }
}