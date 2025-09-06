using HendersonvilleTrafficTest.Services.Models;

namespace HendersonvilleTrafficTest.Services.Interfaces
{
    public interface IDataAccessService
    {
        Task<TestSequenceStep[]> GetTestSequenceAsync(string sequenceId);
        Task<string[]> GetAllTestSequencesAsync();
    }
}