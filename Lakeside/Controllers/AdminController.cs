using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Lakeside.Models;
using Lakeside.Models.ViewModels;
using System.IO;

namespace Lakeside.Controllers
{
    public class AdminController : Controller
    {
        SqlConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["lakesidedb"].
                                 ConnectionString.ToString());
        public ActionResult FilmList()
        {
            dbcon.Open();
            List<Film> filmlist = Film.GetFilmList(dbcon);
            dbcon.Close();
            return View(filmlist);
        }

        public ActionResult FilmCreate()
        {
            Film film = new Film();
            film.Title = "a new film";
            film.YearMade = 0;
            film.Link = "xx";
            film.Imagefile = "xx";
            film.Resources = "zz";
            film.Synopsis = "xx";
            dbcon.Open();
            int intresult = Film.CUFilm(dbcon, "create", film);
            dbcon.Close();
            return RedirectToAction("FilmList", "Admin");
        }

        public ActionResult FilmEdit(int id)
        {
            FilmEditVM filmvm = new FilmEditVM();
            dbcon.Open();
            filmvm.film = Film.GetFilmSingle(dbcon, id);
            filmvm.FilmCatList = CheckModel.GetCheckModelList(dbcon, id);
            dbcon.Close();
            return View(filmvm);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FilmEdit(FormCollection fc, HttpPostedFileBase UploadFile)
        {
            Film film = new Models.Film();
            TryUpdateModel<Film>(film, fc);

            if (UploadFile != null)
            {
                var fileName = Path.GetFileName(UploadFile.FileName);
                var filePath = Server.MapPath("/Content/Images/Films");
                string savedFileName = Path.Combine(filePath, fileName);
                UploadFile.SaveAs(savedFileName);
                film.Imagefile = fileName;
            }
            if (ModelState.IsValid)
            {
                dbcon.Open();
                int intresult = Film.CUFilm(dbcon, "update", film);
                FilmCategory.UpdateCategories(dbcon, fc);
                dbcon.Close();
            }
            return RedirectToAction("FilmList", "Admin");
        }


        public ActionResult FilmDelete(int id)
        {
            dbcon.Open();
            int intresult = Film.FilmDelete(dbcon, id);
            dbcon.Close();
            return RedirectToAction("FilmList", "Admin");
        }

    }
}