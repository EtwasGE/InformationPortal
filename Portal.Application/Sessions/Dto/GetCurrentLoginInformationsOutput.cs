namespace Portal.Application.Sessions.Dto
{
    public class GetCurrentLoginInformationsOutput
    {
        public UserLoginInfoDto User { get; set; }
        public TenantLoginInfoDto Tenant { get; set; }
    }
}