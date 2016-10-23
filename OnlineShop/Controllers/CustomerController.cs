using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using API.Models;
using Model.Models;
using Service;

namespace API.Controllers
{
  [EnableCors("*", "*", "*")]
  public class CustomerController : ApiController
  {
    private readonly IAspNetUserService _aspNetUserService;
    private readonly IAspNetRoleService _aspNetRoleService;

    public CustomerController(IAspNetUserService aspNetUserService, IAspNetRoleService aspNetRoleService)
    {
      this._aspNetUserService = aspNetUserService;
      this._aspNetRoleService = aspNetRoleService;
    }

    //get: api/customer
    [HttpGet]
    public IEnumerable<AspNetUser> GetCustomersBySearchCriteria(string email = null, string firstName = null,
      string lastName = null, DateTime? dateofBirth = default(DateTime?),
      string ipAddress = null)
    {
     
       
      return _aspNetUserService.GetAspNetUserBySearchCriteria(
        email,
        firstName,
        lastName,
        dateofBirth,
        ipAddress
    
        );
    }
  }
}