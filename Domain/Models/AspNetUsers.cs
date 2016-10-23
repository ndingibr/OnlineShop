using System;
using System.Collections.Generic;
using System.Net;

namespace Model.Models
{
  public class AspNetUser
  {
    public AspNetUser()
    {

      this.AspNetRoles = new List<AspNetRole>();
    }
    public string Id { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PasswordHash { get; set; }
    public string SecurityStamp { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? LockoutEndDateUtc { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    public string UserName { get; set; }
    public string Firstname { get; set; }
    public string LastName { get; set; }
    public string IpAddress { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? LastActivity { get; set; }
    public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
  }
}
