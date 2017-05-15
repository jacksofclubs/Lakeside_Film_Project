using System;
using System.Collections.Generic;
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

        public ActionResult FilmDelete(int id)
        {
            dbcon.Open();
            int intresult = Film.FilmDelete(dbcon, id);
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

        //member action methods
        public ActionResult MemberList()
        {
            dbcon.Open();
            List<Member> mbrlist = Member.GetMemberList(dbcon);
            dbcon.Close();
            return View(mbrlist);
        }

        public ActionResult MemberCreate()
        {
            Member mbr = new Member();
            mbr.Avatar = "noname.jpg";
            mbr.Email = "enter value";
            mbr.MemberName = "enter value";
            mbr.PWD = "P@ssword01";
            dbcon.Open();
            int intresult = Member.CUDMember(dbcon, "create", mbr);
            dbcon.Close();
            return RedirectToAction("MemberList", "Admin");
        }

        public ActionResult MemberDelete(int id)
        {
            try
            {
                Member mbr = new Member();
                mbr.MemberID = id;
                dbcon.Open();
                int intresult = Member.MemberDelete(dbcon, id);
                dbcon.Close();
                return RedirectToAction("Memberlist");
            } catch (Exception ex)
            {
                @ViewBag.errormsg = ex.Message;
                if (dbcon != null && dbcon.State == ConnectionState.Open) dbcon.Close();
                return View("error");
            }
        }

        public ActionResult MemberEdit(int id)
        {
            dbcon.Open();
            Member mbr = Member.GetMemberSingle(dbcon, id);
            dbcon.Close();
            return View(mbr);
        }

        [HttpPost]
        public ActionResult MemberEdit(Member mbr, HttpPostedFileBase UploadFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (UploadFile != null)
                    {
                        var fileName = Path.GetFileName(UploadFile.FileName);
                        var filePath = Server.MapPath("/Content/Images/Members");
                        string savedFileName = Path.Combine(filePath, fileName);
                        UploadFile.SaveAs(savedFileName);
                        mbr.Avatar = fileName;
                    }
                    dbcon.Open();
                    int intresult = Member.CUDMember(dbcon, "update", mbr);
                    dbcon.Close();
                    return RedirectToAction("Memberlist");
                }
                return View(mbr);
            } catch (Exception ex)
            {
                @ViewBag.errormsg = ex.Message;
                if (dbcon != null && dbcon.State == ConnectionState.Open) dbcon.Close();
                return View("error");
            }
        }

    }
}