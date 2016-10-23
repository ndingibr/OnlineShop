﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using API.Models;


namespace API.Controllers
{
  [AllowAnonymous]
  [RoutePrefix("api/Account")]

  public class AccountController : ApiController
  {
    private const string LocalLoginProvider = "Local";
    private ApplicationUserManager _userManager;
    private ApplicationRoleManager _roleManager;

    public AccountController()
    {
    }
  
    public AccountController(ApplicationUserManager userManager,
      ApplicationRoleManager roleManager,
      ISecureDataFormat<AuthenticationTicket> accessTokenFormat
      )
    {
      UserManager = userManager;
      this.RoleManager = roleManager;
      AccessTokenFormat = accessTokenFormat;
    }

    public ApplicationUserManager UserManager
    {
      get { return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
      private set { _userManager = value; }
    }

    public ApplicationRoleManager RoleManager
    {
      get { return this._roleManager ?? Request.GetOwinContext().Get<ApplicationRoleManager>(); }
      private set { this._roleManager = value; }
    }

    public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

    // POST api/Account/Logout
    [Route("Logout")]
    public IHttpActionResult Logout()
    {
      Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
      return Ok();
    }
    

    // POST api/Account/ChangePassword
    [Route("ChangePassword")]
    public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
        model.NewPassword);

      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      return Ok();
    }

    // POST api/Account/SetPassword
    [Route("SetPassword")]
    public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      return Ok();
    }

    [Route("Roles")]
    public IHttpActionResult GetAllRoles()
    {
      var roles = RoleManager.Roles;
      return Ok(roles);
    }

    // POST api/Account/RemoveLogin
    [Route("RemoveLogin")]
    public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IdentityResult result;

      if (model.LoginProvider == LocalLoginProvider)
      {
        result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
      }
      else
      {
        result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
          new UserLoginInfo(model.LoginProvider, model.ProviderKey));
      }

      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      return Ok();
    }

    // POST api/Account/Register
    [AllowAnonymous]
    [Route("Register")]
    public async Task<IHttpActionResult> Register(RegisterModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var user = new ApplicationUser()
      {
        UserName = model.Email,
        Email = model.Email,
        Firstname = model.FirstName,
        LastName = model.LastName,
        PhoneNumber = model.PhoneNumber,
        CreatedOn = DateTime.Now,
        LastActivity = DateTime.Now
      };

      IdentityResult result = await UserManager.CreateAsync(user, model.Password);

      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      return Ok();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && _userManager != null)
      {
        _userManager.Dispose();
        _userManager = null;
      }

      base.Dispose(disposing);
    }

    #region Helpers

    private IAuthenticationManager Authentication
    {
      get { return Request.GetOwinContext().Authentication; }
    }

    private IHttpActionResult GetErrorResult(IdentityResult result)
    {
      if (result == null)
      {
        return InternalServerError();
      }

      if (!result.Succeeded)
      {
        if (result.Errors != null)
        {
          foreach (string error in result.Errors)
          {
            ModelState.AddModelError("", error);
          }
        }

        if (ModelState.IsValid)
        {
          // No ModelState errors are available to send, so just return an empty BadRequest.
          return BadRequest();
        }

        return BadRequest(ModelState);
      }

      return null;
    }

    #endregion
  }
}
