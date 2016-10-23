using System;

namespace API.Models
{
  public class SearchData
  {
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string IpAddress { get; set; }
    public string[] Roles { get; set; }
  }
}