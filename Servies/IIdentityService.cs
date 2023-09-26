namespace BEApi.Servies
{
    public interface IIdentityService
    {
        string GetUserNameIdentity();
        string GetUserIdIdentity();
        string GetUserIsAdminIdentity();
    }
}