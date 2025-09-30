namespace BG_IMPACT.Repository.Repositories.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid id, string role);
    }
}
