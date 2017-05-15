using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lakeside.Models
{
    public class Film
    {
        [Required,Key]
        public int FilmID { get; set; }
        [Required,MaxLength(100)]
        public string Title { get; set; }
        [Required, MaxLength(100)]
        public string Link { get; set; }
        [Required]
        public int YearMade { get; set; }
        [Required, MaxLength(30)]
        public string Imagefile { get; set; }
        [Required, MaxLength(1500)]
        public string Synopsis { get; set; }
        [AllowHtml]
        [Required, MaxLength(1000)]
        public string Resources { get; set; }

        public static Film GetFilmSingle(SqlConnection dbcon, int id)
        {
            Film obj = new Film();
            string strsql = "select * from Films where FilmID = " + id;
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                obj.FilmID = Convert.ToInt32(myReader["FilmID"].ToString());
                obj.Title = myReader["Title"].ToString();
                obj.Link = myReader["Link"].ToString();
                obj.YearMade = Convert.ToInt32(myReader["YearMade"].ToString());
                obj.Imagefile = myReader["Imagefile"].ToString();
                obj.Synopsis = myReader["Synopsis"].ToString();
                obj.Resources = myReader["Resources"].ToString();
            }
            myReader.Close();
            return obj;
        }
        public static List<Film> GetFilmList(SqlConnection dbcon)
        {
            List<Film> itemlist = new List<Film>();
            string strsql = "select * from Films";
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                Film obj = new Film();
                obj.FilmID = Convert.ToInt32(myReader["FilmID"].ToString());
                obj.Title = myReader["Title"].ToString();
                obj.Link = myReader["Link"].ToString();
                obj.YearMade = Convert.ToInt32(myReader["YearMade"].ToString());
                obj.Imagefile = myReader["Imagefile"].ToString();
                obj.Synopsis = myReader["Synopsis"].ToString();
                obj.Resources = myReader["Resources"].ToString();
                itemlist.Add(obj);
            }
            myReader.Close();
            return itemlist;
        }
        public static int CUFilm(SqlConnection dbcon, string CUDAction, Film obj)
        {
            SqlCommand cmd = new SqlCommand();
            if (CUDAction == "create")
            {
                cmd.CommandText = "insert into Films " +
                "(Title,Link,YearMade,Imagefile,Synopsis,Resources) " +
                "Values (@Title,@Link,@YearMade,@Imagefile,@Synopsis,@Resources)";
                cmd.Parameters.AddWithValue("@Title", SqlDbType.VarChar).Value = obj.Title;
                cmd.Parameters.AddWithValue("@Link", SqlDbType.VarChar).Value = obj.Link;
                cmd.Parameters.AddWithValue("@YearMade", SqlDbType.Int).Value = obj.YearMade;
                cmd.Parameters.AddWithValue("@Imagefile", SqlDbType.VarChar).Value = obj.Imagefile;
                cmd.Parameters.AddWithValue("@Synopsis", SqlDbType.VarChar).Value = obj.Synopsis;
                cmd.Parameters.AddWithValue("@Resources", SqlDbType.VarChar).Value = obj.Resources;
            }
            else if (CUDAction == "update")
            {
                cmd.CommandText = "update Films set Title = @Title, Link = @Link, "+
                                  "YearMade = @YearMade, Imagefile = @Imagefile, " +
                                  "Synopsis = @Synopsis, Resources = @Resources " +
                                  "Where FilmID = @FilmID";
                cmd.Parameters.AddWithValue("@Title", SqlDbType.VarChar).Value = obj.Title;
                cmd.Parameters.AddWithValue("@Link", SqlDbType.VarChar).Value = obj.Link;
                cmd.Parameters.AddWithValue("@YearMade", SqlDbType.Int).Value = obj.YearMade;
                cmd.Parameters.AddWithValue("@Imagefile", SqlDbType.VarChar).Value = obj.Imagefile;
                cmd.Parameters.AddWithValue("@Synopsis", SqlDbType.VarChar).Value = obj.Synopsis;
                cmd.Parameters.AddWithValue("@Resources", SqlDbType.VarChar).Value = obj.Resources;
                cmd.Parameters.AddWithValue("@FilmID", SqlDbType.Int).Value = obj.FilmID;
            }
            cmd.Connection = dbcon;
            int intResult = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return intResult;
        }

        public static int FilmDelete(SqlConnection dbcon, int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete reviews where FilmID = @FilmID";
            cmd.Parameters.AddWithValue("@FilmID", SqlDbType.Int).Value = id;
            cmd.Connection = dbcon;
            int intResult = cmd.ExecuteNonQuery();

            cmd.CommandText = "delete filmcategories where FilmID = @FilmID";
            intResult = cmd.ExecuteNonQuery();

            cmd.CommandText = "delete films where FilmID = @FilmID";
            intResult = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return 1;
        }

        public static List<Film> GetFilmListView1(SqlConnection dbcon,int id)
        {
            List<Film> itemlist = new List<Film>();
            string strsql = "select * from filmlistview1 where categoryid = " + id;
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                Film obj = new Film();
                obj.FilmID = Convert.ToInt32(myReader["FilmID"].ToString());
                obj.Title = myReader["Title"].ToString();
               // obj.Link = myReader["Link"].ToString();
               //obj.YearMade = Convert.ToInt32(myReader["YearMade"].ToString());
                obj.Imagefile = myReader["Imagefile"].ToString();
                obj.Synopsis = myReader["Synopsis"].ToString();
                //obj.Resources = myReader["Resources"].ToString();
                itemlist.Add(obj);
            }
            myReader.Close();
            return itemlist;
        }
        public static IEnumerable<SelectListItem> GetFilmDDList(SqlConnection dbcon,string whereclause)
        {
            IList<SelectListItem> ddlist = new List<SelectListItem>();
            string strsql = "select filmid,title from Films where " + whereclause + " Order by title";
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                ddlist.Add(new SelectListItem()
                {
                    Text = myReader["Title"].ToString(),
                    Value = myReader["FilmID"].ToString()
                });
            }
            myReader.Close();
            return ddlist;
        }
    }
}