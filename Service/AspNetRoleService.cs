using System.Collections.Generic;
using Model.Infrastructure;
using Model.Models;
using Model.Repository;

namespace Service
{
  public interface IAspNetRoleService
  {
    IEnumerable<AspNetRole> GetAspNetRoles();
    AspNetRole GetAspNetRole(string id);
    AspNetRole CreateAspNetRole(AspNetRole aspNetRole);
    void UpdateAspNetRole(AspNetRole aspNetRole);
    void DeleteAspNetRole(string id);
  }

  public class AspNetRoleService : IAspNetRoleService
  {
    private readonly IAspNetRoleRepository _iAspNetRoleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AspNetRoleService(IAspNetRoleRepository iAspNetRoleRepository, IUnitOfWork unitOfWork)
    {
      this._iAspNetRoleRepository = iAspNetRoleRepository;
      this._unitOfWork = unitOfWork;
    }

    #region IAspNetRoleService Members

    public IEnumerable<AspNetRole> GetAspNetRoles()
    {
      var aspNetRoles = _iAspNetRoleRepository.GetAll();
      return aspNetRoles;
    }

    public AspNetRole GetAspNetRole(string id)
    {
      var aspNetRole = _iAspNetRoleRepository.Get(q => q.Id == id);
      return aspNetRole;
    }

    public AspNetRole CreateAspNetRole(AspNetRole aspNetRole)
    {
      _iAspNetRoleRepository.Add(aspNetRole);
      SaveAspNetRole();
      return aspNetRole;
    }

    public void UpdateAspNetRole(AspNetRole aspNetRole)
    {
      _iAspNetRoleRepository.Update(aspNetRole);
      SaveAspNetRole();
    }

    public void DeleteAspNetRole(string id)
    {
      var aspNetRole = _iAspNetRoleRepository.GetById(id);
      _iAspNetRoleRepository.Delete(aspNetRole);
      _iAspNetRoleRepository.Delete(q => q.Id == id);
      SaveAspNetRole();
    }

    public void SaveAspNetRole()
    {
      _unitOfWork.Commit();
    }

    #endregion
  }
}