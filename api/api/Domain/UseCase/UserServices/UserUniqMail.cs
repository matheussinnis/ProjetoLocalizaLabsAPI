using System;
using System.Runtime.Serialization;

namespace api.Domain.UseCase.UserServices
{
  [Serializable]
  public class UserUniqMail : Exception
  {
    public UserUniqMail(string message) : base(message) { }
  }
}