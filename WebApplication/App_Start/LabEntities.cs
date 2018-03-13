using System.Data.Entity;


namespace WebApplication.App_Start
{
    public class LabEntities : DbContext
    {
        public LabEntities()
        {
            Configuration.ProxyCreationEnabled = false;
        }
    }
}