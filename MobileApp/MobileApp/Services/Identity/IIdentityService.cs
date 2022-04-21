using System.Threading.Tasks;

using MobileApp.Models.Identity;

namespace MobileApp.Services.Identity
{
    public interface IIdentityService
    {
        Task<IdentityResult> LoginAsync();

        Task<IdentityResult> LogoutAsync();

        Task<IdentityResult> RefreshTokenAsync();

        Task<IdentityResult> GetUserInfoUpdatePreferenceAsync();
    }
}