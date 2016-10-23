using System;
using System.Linq;
using Model.Infrastructure;
using Model.Models;

namespace Model.Repository
{
  public class AspNetUserRepository : RepositoryBase<AspNetUser>, IAspNetUserRepository
  {
    public AspNetUserRepository(IDatabaseFactory databaseFactory)
        : base(databaseFactory)
    {
    }

    public IQueryable<AspNetUser> Table
    {
      get
      {
        throw new NotImplementedException();
      }
    }
  }
  public interface IAspNetUserRepository : IRepository<AspNetUser>
  {
  }
}
