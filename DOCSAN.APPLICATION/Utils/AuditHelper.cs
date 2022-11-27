using DOCSAN.CORE.Entities;
using Microsoft.AspNetCore.Http;

namespace DOCSAN.APPLICATION.Utils
{
    public static class AuditHelper
    {
        public static Func<IHttpContextAccessor> _context { get; set; }

        public static string GetRequestOwner()
        {
            var context = _context.Invoke();
            var user = (context.HttpContext.Items["User"] as User)!;
            if(user == null)
                return String.Empty;

            return user.Mail;
        }
    }
}
