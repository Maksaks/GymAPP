using Course_project_GYMAPP.Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Course_project_GYMAPP.Controllers
{
    public abstract class BaseController : Controller
    {
        public void Alert(string message, NotificationType notificationType)
        {
            var msg = "<script>swal('" + notificationType.ToString().ToUpper() + "', '" + message + "','" + notificationType + "');</script>;";
            TempData["notification"] = msg;
        }

        public void ErrorMessage(string message)
        {
            var msg = "<script>alert(" + message + ");</script>;";
            TempData["notification"] = msg;
        }
    }
}
