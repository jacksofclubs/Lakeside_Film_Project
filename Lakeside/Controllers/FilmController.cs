﻿using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using Lakeside.Models.ViewModels;
using System.Data;
using Lakeside.Models;

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

        public ActionResult ViewFilm(int id)
        {
            ViewFilmVM vm = new ViewFilmVM();
            try
            {
                dbcon.Open();
                vm.selectedfilm = Film.GetFilmSingle(dbcon, id);
                vm.reviewlist = FilmReview.GetFilmReviewList(dbcon, id);
                dbcon.Close();
            } catch (Exception ex)
            {
                @ViewBag.errormsg = ex.Message;
                if (dbcon != null && dbcon.State == ConnectionState.Open) dbcon.Close();
                return View("error");
            }
            return View(vm);
        }

    }
}