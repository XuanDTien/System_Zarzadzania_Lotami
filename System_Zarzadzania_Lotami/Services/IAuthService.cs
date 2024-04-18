namespace System_Zarzadzania_Lotami.Services
{
    public interface IAuthService
    {
        Task<bool> ValidateCredentials(string username, string password);
    }
}
