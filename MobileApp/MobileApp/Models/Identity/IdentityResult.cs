namespace MobileApp.Models.Identity
{
    public class IdentityResult
    {
        public string ErrorDescription { get; set; }
        public bool Succeeded => string.IsNullOrEmpty(ErrorDescription);
        public bool Aborted { get; set; }
    }
}