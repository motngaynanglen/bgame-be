namespace BG_IMPACT.Jwt
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid id, string role);
    }
}
