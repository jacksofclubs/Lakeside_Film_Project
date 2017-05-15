using System.Web.Mvc;

namespace Lakeside.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["memberid"] == null)
                Session["memberid"] = 1;
            ViewBag.memberid = Session["memberid"];
            return View();
        }

        [HttpPost]
        public ActionResult SetMemberid(int memberid)
        {
            Session["memberid"] = memberid;
            return RedirectToAction("index");
        }
    }
}