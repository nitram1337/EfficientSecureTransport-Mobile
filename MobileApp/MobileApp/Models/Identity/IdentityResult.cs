namespace MobileApp.Models.Identity
{
    public class IdentityResult
    {
        public string ErrorDescription { get; set; }
        public bool Succeeded { get; set; }
        public bool Aborted { get; set; }
    }
}