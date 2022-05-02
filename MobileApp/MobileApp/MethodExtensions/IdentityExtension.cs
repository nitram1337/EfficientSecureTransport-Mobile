using System;
using System.Linq;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Results;

using MobileApp.Models.Identity;
using MobileApp.Services.Credentials;

namespace MobileApp.MethodExtensions
{
    public static class IdentityExtension
    {
        public static IdentityResult SetProfileCredentials(this LoginResult loginResult, ICredentialsService credentialsService)
        {
            credentialsService.Firstname = loginResult.User.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value;
            credentialsService.Lastname = loginResult.User.Claims.FirstOrDefault(c => c.Type == "family_name")?.Value;
            credentialsService.Fullname = loginResult.User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            credentialsService.Email = loginResult.User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            credentialsService.EmailConfirmed = Convert.ToBoolean(loginResult.User.Claims.FirstOrDefault(c => c.Type == "email_verified")?.Value);
            credentialsService.Role = loginResult.User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            credentialsService.IsAdmin = credentialsService.Role.Contains("Administrator") ? true : false;
            credentialsService.IsAuthenticated = loginResult.User.Identity.IsAuthenticated;
            credentialsService.ExternalProvider = loginResult.User.Claims.FirstOrDefault(c => c.Type == "external_provider")?.Value;
            credentialsService.IsExternal = !string.IsNullOrEmpty(credentialsService.ExternalProvider) ? true : false;

            return new IdentityResult { Succeeded = true };
        }

        public static IdentityResult SetProfileCredentials(this UserInfoResult userInfoResult, ICredentialsService credentialsService)
        {
            credentialsService.Firstname = userInfoResult.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value;
            credentialsService.Lastname = userInfoResult.Claims.FirstOrDefault(c => c.Type == "family_name")?.Value;
            credentialsService.Fullname = userInfoResult.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            credentialsService.Email = userInfoResult.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            credentialsService.Role = userInfoResult.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            credentialsService.IsAdmin = credentialsService.Role.Contains("Administrator") ? true : false;
            credentialsService.EmailConfirmed = Convert.ToBoolean(userInfoResult.Claims.FirstOrDefault(c => c.Type == "email_verified")?.Value);

            return new IdentityResult { Succeeded = true };
        }
    }
}