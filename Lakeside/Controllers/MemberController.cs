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
                rvm.review = Review.GetReviewSingle(dbcon, mbrid, filmid);
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
            string cudAction = fc["cudAction"];
            review.MemberID = Convert.ToInt32(Session["memberid"]);
            review.ReviewDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    if (dbcon.State == ConnectionState.Closed) dbcon.Open();
                    if (cudAction == "create")
                    {
                        review.FilmID = Convert.ToInt32(fc["SelectedFilm"]);
                        filmid = review.FilmID;
                        intresult = Review.CUDReview(dbcon, "create", review);

                    } else if (cudAction == "update")
                    {
                        intresult = Review.CUDReview(dbcon, "update", review);

                    } else if (cudAction == "delete")
                    {
                        intresult = Review.CUDReview(dbcon, "delete", review);

                    } else
                    {
                        // throw exception
                    }

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