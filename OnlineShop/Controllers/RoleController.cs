using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using API.Models;
using Microsoft.AspNet.Identity.EntityFramework;


namespace API.Controllers
{
  [AllowAnonymous]
  [RoutePrefix("api/role")]
  public class RoleController : ApiController
  {
    private ApplicationRoleManager _roleManager;

    public RoleController()
    {
    }
    public RoleController(ApplicationRoleManager roleManager)
    {
      this.RoleManager = roleManager;
    }
    
    public ApplicationRoleManager RoleManager
    {
      get { return this._roleManager ?? Request.GetOwinContext().Get<ApplicationRoleManager>(); }
      private set { this._roleManager = value; }
    }

    public IHttpActionResult GetAllRoles()
    {
      var roles = RoleManager.Roles;
      return Ok(roles);
    }

    [Route("create")]
    [HttpPost]
    public IHttpActionResult CreateRole(RoleDto role)
    {
      IdentityRole newRole = new IdentityRole
      {
        Id = role.Id,
        Name = role.Name
      };
      var roleResult = RoleManager.Create(newRole);
      return Ok(roleResult);
    }

    [Route("update")]
    public IHttpActionResult UpdateRole(RoleDto role)
    {
      IdentityRole newRole = new IdentityRole
      {
        Id = role.Id,
        Name = role.Name
      };
      var roleResult = RoleManager.Update(newRole);
      return Ok(roleResult);
    }

    [Route("delete")]
    public IHttpActionResult UpdateRole(string roleId)
    {
      var roletoDelete = RoleManager.FindById(roleId);
      var roleResult = RoleManager.Delete(roletoDelete);
      return Ok(roleResult);
    }

    #region Helpers

    #endregion
  }
}
