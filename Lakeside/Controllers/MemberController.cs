using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using Lakeside.Models.ViewModels;
using System.Data;
using Lakeside.Models;
using System.IO;

namespace Lakeside.Controllers
{
    public class MemberController : Controller
    {
        SqlConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["lakesidedb"].
                                 ConnectionString.ToString());

        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyReview(int? id)
        {
            int mbrid = Convert.ToInt32(Session["memberid"].ToString());
            int filmid = 0;
            ReviewVM rvm = new ReviewVM();
            try
            {
                if (dbcon.State == ConnectionState.Closed) dbcon.Open();
                rvm.filmlist = Film.GetFilmDDList(dbcon, "1=1");
                if (id != null) filmid = Convert.ToInt32(id.ToString());
                else filmid = Convert.ToInt32(rvm.filmlist.ToList()[0].Value);
                rvm.SelectedFilm = filmid;
                rvm.review = Review.GetReviewSingle(dbcon, filmid, mbrid);
                dbcon.Close();
                return View(rvm);
            } catch (Exception ex)
            {
                @ViewBag.errormsg = ex.Message;
                if (dbcon != null &&
                dbcon.State == ConnectionState.Open) dbcon.Close();
                return View("error");
            }
        }

        [HttpPost]
        public ActionResult MyReview(Review review, FormCollection fc)
        {
            int filmid = review.FilmID;
            int intresult = 0;
            if (ModelState.IsValid)
            {
                review.MemberID = Convert.ToInt32(Session["memberid"]);
                review.ReviewDate = DateTime.Now;
                try
                {
                    if (dbcon.State == ConnectionState.Closed) dbcon.Open();
                    //if (fc["btnSave"] != null)
                    // update logic here
                    //else if (fc["btnCreate"] != null)
                    // create logic here
                    //else if (fc["btnDelete"] != null)
                    // delete logic here
                } catch (Exception ex)
                {
                    @ViewBag.errormsg = ex.Message;
                    if (dbcon != null && dbcon.State == ConnectionState.Open) dbcon.Close();
                    return View("error");
                }
            }
            return RedirectToAction("MyReview", "Member", new { id = filmid });
        }

    }
}