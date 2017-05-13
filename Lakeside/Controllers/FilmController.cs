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
    public class FilmController : Controller
    {
        SqlConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["lakesidedb"].
                                 ConnectionString.ToString());

        // GET: Film
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FilmList(int id = 0)
        {
            FilmListVM filmvm = new FilmListVM();
            try
            {
                dbcon.Open();
                filmvm.catlist = Category.GetCategoriesDDList(dbcon);
                if (id > 0) filmvm.selectedcatid = id;
                else filmvm.selectedcatid = Convert.ToInt32(filmvm.catlist.ToList()[0].Value);
                filmvm.films = Film.GetFilmListView1(dbcon, filmvm.selectedcatid);
                dbcon.Close();
            } catch (Exception ex)
            {
                @ViewBag.errormsg = ex.Message;
                if (dbcon != null && dbcon.State == ConnectionState.Open) dbcon.Close();
                return View("error");
            }
            return View(filmvm);
        }
        [HttpPost]
        public ActionResult FilmList(FormCollection fc)
        {
            return RedirectToAction("FilmList", "Film",
            new { id = Convert.ToInt32(fc["selectedcatid"]) });
        }

    }
}