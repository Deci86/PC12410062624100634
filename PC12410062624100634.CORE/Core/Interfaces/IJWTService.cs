using PC12410062624100634.CORE.Core.Entities;
using PC12410062624100634.CORE.Core.Settings;

namespace PC12410062624100634.CORE.Core.Interfaces
{
    public interface IJWTService
    {
        JWTSettings _settings { get; }

        string GenerateJWToken(User user);
    }
}