using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Lakeside.Models
{
    public class FilmCategory
    {
        [Required,Key,Column(Order = 1)]
        public int FilmID { get; set; }
        [Required, Key,Column(Order = 2)]
        public int CategoryID { get; set; }

        public static FilmCategory GetFilmCategorySingle(SqlConnection dbcon, int id)
        {
            FilmCategory obj = new FilmCategory();
            string strsql = "select * from FilmCategories where FikmID = " + id;
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                obj.FilmID = Convert.ToInt32(myReader["FilmID"].ToString());
                obj.CategoryID = Convert.ToInt32(myReader["CategoryID"].ToString());
            }
            myReader.Close();
            return obj;
        }
        public static List<FilmCategory> GetFilmCategoryList(SqlConnection dbcon)
        {
            List<FilmCategory> itemlist = new List<FilmCategory>();
            string strsql = "select * from FilmCategories";
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                FilmCategory obj = new FilmCategory();
                obj.FilmID = Convert.ToInt32(myReader["FilmID"].ToString());
                obj.CategoryID = Convert.ToInt32(myReader["CategoryID"].ToString());
                itemlist.Add(obj);
            }
            myReader.Close();
            return itemlist;
        }
        public static int CUDFilmCategory(SqlConnection dbcon, string CUDAction, FilmCategory obj)
        {
            SqlCommand cmd = new SqlCommand();
            if (CUDAction == "create")
            {
                cmd.CommandText = "insert into FilmCategories " +
                "Values (@FilmID,@CategoryID)";
                cmd.Parameters.AddWithValue("@FilmID", SqlDbType.Int).Value = obj.FilmID;
                cmd.Parameters.AddWithValue("@CategoryID", SqlDbType.Int).Value = obj.CategoryID;
            }
            else if (CUDAction == "delete")
            {
                cmd.CommandText = "delete FilmCategories where FilmID = @FilmID";
                cmd.Parameters.AddWithValue("@FilmID", SqlDbType.Int).Value = obj.FilmID;
            }
            cmd.Connection = dbcon;
            int intResult = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return intResult;
        }

        public static void UpdateCategories(SqlConnection dbcon,FormCollection fc)
        {
            int x, intcnt, catid, filmid;
            filmid = Convert.ToInt32(fc["FilmID"]);
           
            string strsql = "delete from filmcategories where filmid = " + filmid;
            var cmd = new SqlCommand(strsql,dbcon);
            intcnt = cmd.ExecuteNonQuery();
            for (x = 0; x < fc.Count; x++)
            {
                if (fc.Keys[x].Substring(0, 4) == "cat-")
                {
                    catid = Convert.ToInt32(fc.Keys[x].Substring(4));
                    if (fc[x].StartsWith("true"))
                    {
                        strsql = "insert into filmcategories values(" + filmid + "," + catid + ");";
                        cmd.CommandText = strsql;
                        intcnt = cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}