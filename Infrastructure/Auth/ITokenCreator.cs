using Core.Entities;

namespace Infrastructure.Auth
{
    public interface ITokenCreator
    {
        string Create(User user);
    }
}
