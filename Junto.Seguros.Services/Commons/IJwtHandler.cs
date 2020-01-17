using Junto.Seguros.Domain.Commons;

namespace Junto.Seguros.Services.Commons
{
    public interface IJwtHandler
    {
        JsonWebToken Create(string login, string name);
        JwtUser Decode(string token);
    }
}
