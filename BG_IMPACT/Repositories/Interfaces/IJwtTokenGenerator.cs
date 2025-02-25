namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid id, string role);
    }
}
