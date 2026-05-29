namespace PC12410062624100634.CORE.Core.Interfaces
{
    public interface IJWTService
    {
        // Genera un token usando datos básicos (puedes pasarle el correo del cliente o usuario)
        string GenerateToken(string email, string role);
    }
}