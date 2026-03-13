namespace ThreadService.Application.Abstractions
{
    public interface ITokenProvider
    {
        Task<string> GetAccessTokenAsync();
    }
}