namespace BEApi.Servies
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }

        public virtual string GetUserNameIdentity()
        {
            return (_context.HttpContext?.User).FindFirst("UserName")?.Value;
        }

        public virtual string GetUserIdIdentity()
        {
            return (_context.HttpContext?.User).FindFirst("Id")?.Value;
        }

        public virtual string GetUserIsAdminIdentity()
        {
            return (_context.HttpContext?.User).FindFirst("IsAdmin")?.Value;
        }
    }
}