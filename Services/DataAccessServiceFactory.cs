using HendersonvilleTrafficTest.Configuration;
using HendersonvilleTrafficTest.Services.Interfaces;

namespace HendersonvilleTrafficTest.Services
{
    public class DataAccessServiceFactory
    {
        public IDataAccessService CreateDataAccessService()
        {
            return ConfigurationManager.Current.DataAccess.DataAccessMode switch
            {
                DataAccessMode.Local => new LocalDataAccessService(),
                DataAccessMode.Database => new DatabaseDataAccessService(),
                _ => new LocalDataAccessService()
            };
        }
    }
}