using System;
using System.Runtime.Serialization;

namespace api.Domain.UseCase.UserServices
{
  [Serializable]
  public class UserEmptyId : Exception
  {
    public UserEmptyId(string message) : base(message) { }
  }
}