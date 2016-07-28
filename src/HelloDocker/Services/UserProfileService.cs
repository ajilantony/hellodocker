using System;
using System.Collections.Generic;
using HelloDocker.Models;

namespace HelloDocker.Services
{
  public class UserProfileService
  {
    public List<UserProfile> UserProfiles { get; } = new List<UserProfile>()
    {
      new UserProfile() { Id = 1, Guid = Guid.NewGuid(), Username = "akalathil", FirstName = "Ajil", LastName = "Kalathil", Email = "akalathil@usga.org" }
    };
  }
}
