using System.Threading.Tasks;

namespace MobileApp.Services.Credentials
{
    public interface ICredentialsService
    {
        string Firstname { get; set; }

        string Lastname { get; set; }

        string Fullname { get; set; }

        string Email { get; set; }

        bool EmailConfirmed { get; set; }

        string Role { get; set; }

        bool IsAdmin { get; set; }

        bool IsAuthenticated { get; set; }

        string ExternalProvider { get; set; }

        bool IsExternal { get; set; }



        string AccessToken { get; set; }

        string IdentityToken { get; set; }

        string RefreshToken { get; set; }

        string AccessTokenExpiration { get; set; }



        Task SetCredentialsFromSecureStorage();
        void ClearProfileCredentials();
    }
}
