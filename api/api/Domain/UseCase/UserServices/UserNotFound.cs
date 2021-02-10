using System;
using System.Runtime.Serialization;

namespace api.Domain.UseCase.UserServices
{
  [Serializable]
  public class UserNotFound : Exception
  {
    public UserNotFound(string message) : base(message) { }
  }
}