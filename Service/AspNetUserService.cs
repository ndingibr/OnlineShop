using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Model.Infrastructure;
using Model.Models;
using Model.Repository;

namespace Service
{
  public interface IAspNetUserService
  {
    IEnumerable<AspNetUser> GetAspNetUsers();

    IEnumerable<AspNetUser> GetAspNetUserBySearchCriteria(
      string email = null, string firstName = null,
      string lastName = null, DateTime? dateofBirth = default(DateTime?),
      string ipAddress = null);

    AspNetUser GetAspNetUser(string id);
    AspNetUser CreateAspNetUser(AspNetUser aspNetUser);
    void UpdateAspNetUser(AspNetUser aspNetUser);
    void DeleteAspNetUser(string id);
  }

  public class AspNetUserService : IAspNetUserService
  {
    private readonly IAspNetUserRepository _iAspNetUserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AspNetUserService(IAspNetUserRepository iAspNetUserRepository, IUnitOfWork unitOfWork)
    {
      this._iAspNetUserRepository = iAspNetUserRepository;
      this._unitOfWork = unitOfWork;
    }

    #region IAspNetUserService Members

    public IEnumerable<AspNetUser> GetAspNetUsers()
    {
      var aspNetUsers = _iAspNetUserRepository.GetAll();
      return aspNetUsers;
    }

    public AspNetUser GetAspNetUser(string id)
    {
      var aspNetUser = _iAspNetUserRepository.Get(q => q.Id == id);
      return aspNetUser;
    }

    public IEnumerable<AspNetUser> GetAspNetUserBySearchCriteria(
      string email = null, string firstName = null,
      string lastName = null, DateTime? dateofBirth = default(DateTime?),
      string ipAddress = null)
    {
      var query = _iAspNetUserRepository.GetAll();
      if (!string.IsNullOrEmpty(email))
        query = query.Where(x => x.Email == email);

      if (!string.IsNullOrWhiteSpace(firstName))
        query = query.Where(x => x.Firstname == firstName);

      if (!string.IsNullOrWhiteSpace(lastName))
        query = query.Where(x => x.LastName == lastName);

      if (!string.IsNullOrWhiteSpace(ipAddress))
        query = query.Where(x => Equals(x.IpAddress, ipAddress));

      //if (roles != null && roles.Length > 0)
      //  query = query.Where(x => x.AspNetRoles.Select(y => y.Id).Intersect(roles).Any());
      return query;
    }

    public AspNetUser CreateAspNetUser(AspNetUser aspNetUser)
    {
      _iAspNetUserRepository.Add(aspNetUser);
      SaveAspNetUser();
      return aspNetUser;
    }

    public void UpdateAspNetUser(AspNetUser aspNetUser)
    {
      _iAspNetUserRepository.Update(aspNetUser);
      SaveAspNetUser();
    }

    public void DeleteAspNetUser(string id)
    {
      var aspNetUser = _iAspNetUserRepository.GetById(id);
      _iAspNetUserRepository.Delete(aspNetUser);
      _iAspNetUserRepository.Delete(q => q.Id == id);
      SaveAspNetUser();
    }

    public void SaveAspNetUser()
    {
      _unitOfWork.Commit();
    }

    #endregion
  }
}