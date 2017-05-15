using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Lakeside.Models.ViewModels
{
    public class FilmReview
    {
        public int FilmID { get; set; }
        public string Avatar { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Rating { get; set; }
        public string ReviewTitle { get; set; }
        public string FullReview { get; set; }
        public string MemberName { get; set; }

        public static List<FilmReview> GetFilmReviewList(SqlConnection dbcon,int filmid)
        {
            List<FilmReview> itemlist = new List<FilmReview>();
            string strsql = "select * from filmreviews where filmid = " + filmid;
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                FilmReview obj = new FilmReview();
                obj.FilmID = Convert.ToInt32(myReader["FilmID"].ToString());
                obj.Avatar = myReader["Avatar"].ToString();
                obj.ReviewDate = Convert.ToDateTime(myReader["ReviewDate"].ToString());
                obj.Rating = Convert.ToInt32(myReader["Rating"].ToString());
                obj.ReviewTitle = myReader["ReviewTitle"].ToString();
                obj.FullReview = myReader["FullReview"].ToString();
                obj.MemberName = myReader["MemberName"].ToString();
                itemlist.Add(obj);
            }
            myReader.Close();
            return itemlist;
        }
    }
}