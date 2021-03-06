﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Lakeside.Models
{
    public class Review
    {
        [Required, Key]
        public int MemberID { get; set; }
        [Required, Key]
        public int FilmID { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required, MaxLength(100)]
        [RegularExpression("^[^<>]{2,100}$", ErrorMessage = "Review Title is not valid")]
        public string ReviewTitle { get; set; }
        [Required, MaxLength(1000)]
        [RegularExpression("^[^<>]{2,1000}$", ErrorMessage = "Full review is not valid")]
        public string FullReview { get; set; }

        public static Review GetReviewSingle(SqlConnection dbcon, int memberid, int filmid)
        {
            Review obj = new Review();
            string strsql = "select * from Reviews where MemberID = " + memberid + "and FilmID = " + filmid;
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                // set retrieved objects to this object
                obj.MemberID = Convert.ToInt32(myReader["MemberID"].ToString());
                obj.FilmID = Convert.ToInt32(myReader["FilmID"].ToString());
                obj.ReviewDate = Convert.ToDateTime(myReader["ReviewDate"].ToString());
                obj.Rating = Convert.ToInt32(myReader["Rating"].ToString());
                obj.ReviewTitle = myReader["ReviewTitle"].ToString();
                obj.FullReview = myReader["FullReview"].ToString();
            }
            myReader.Close();
            return obj;
        }

        public static int CUDReview(SqlConnection dbcon, string CUDAction, Review obj)
        {
            SqlCommand cmd = new SqlCommand();

            if (CUDAction == "create")
            {
                cmd.CommandText = "INSERT INTO Reviews (MemberID, FilmID, ReviewDate, Rating, ReviewTitle, FullReview) " +
                    "VALUES (@MemberID, @FilmID, @ReviewDate, @Rating, @ReviewTitle, @FullReview)";
                cmd.Parameters.AddWithValue("@MemberID", SqlDbType.Int).Value = obj.MemberID;
                cmd.Parameters.AddWithValue("@FilmID", SqlDbType.Int).Value = obj.FilmID;
                cmd.Parameters.AddWithValue("@ReviewDate", SqlDbType.DateTime).Value = obj.ReviewDate;
                cmd.Parameters.AddWithValue("@Rating", SqlDbType.Int).Value = obj.Rating;
                cmd.Parameters.AddWithValue("@ReviewTitle", SqlDbType.VarChar).Value = obj.ReviewTitle;
                cmd.Parameters.AddWithValue("@FullReview", SqlDbType.VarChar).Value = obj.FullReview;
            } 
            else if (CUDAction == "update")
            {
                cmd.CommandText = "UPDATE REVIEWS SET " +
                    "ReviewDate = @ReviewDate, " +
                    "Rating = @Rating, " +
                    "ReviewTitle = @ReviewTitle, " +
                    "FullReview = @FullReview " +
                    "WHERE MemberID = @MemberID AND FilmID = @FilmID";
                cmd.Parameters.AddWithValue("@MemberID", SqlDbType.Int).Value = obj.MemberID;
                cmd.Parameters.AddWithValue("@FilmID", SqlDbType.Int).Value = obj.FilmID;
                cmd.Parameters.AddWithValue("@ReviewDate", SqlDbType.DateTime).Value = obj.ReviewDate;
                cmd.Parameters.AddWithValue("@Rating", SqlDbType.Int).Value = obj.Rating;
                cmd.Parameters.AddWithValue("@ReviewTitle", SqlDbType.VarChar).Value = obj.ReviewTitle;
                cmd.Parameters.AddWithValue("@FullReview", SqlDbType.VarChar).Value = obj.FullReview;
            }
            else if (CUDAction == "delete")
            {
                cmd.CommandText = "DELETE FROM Reviews " +
                    "WHERE MemberID = @MemberID AND FilmID = @FilmID";
                cmd.Parameters.AddWithValue("@MemberID", SqlDbType.Int).Value = obj.MemberID;
                cmd.Parameters.AddWithValue("@FilmID", SqlDbType.Int).Value = obj.FilmID;
            }

            cmd.Connection = dbcon;
            int intResult = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return intResult;
        }
    }
}