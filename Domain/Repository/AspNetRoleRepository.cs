using System.Linq;
using Model.Infrastructure;
using Model.Models;

namespace Model.Repository
{
    public class AspNetRoleRepository : RepositoryBase<AspNetRole>, IAspNetRoleRepository
    {
        public AspNetRoleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

      public IQueryable<AspNetRole> Table { get; }
    }
    public interface IAspNetRoleRepository : IRepository<AspNetRole>
    {
    }
}
