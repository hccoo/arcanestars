namespace ArcaneStars.AuthServiceHost.Proxies.Dtos
{
    public class VerificationDto
    {
        public string Code { get; set; }

        public string To { get; set; }

        public BizCode BizCode { get; set; }
    }

    public enum BizCode
    {
        Register = 1,
        ForgetPassword = 2,
        Login = 3
    }
}
