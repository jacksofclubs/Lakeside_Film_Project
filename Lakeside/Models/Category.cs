using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lakeside.Models
{
    public class Category
    {
        [Required,Key]
       
        public int Categoryid { get; set; }
        [Required, MaxLength(20)]
        public string CategoryName { get; set; }

        public static Category GetCategorySingle(SqlConnection dbcon, int id)
        {
            Category obj = new Category();
            string strsql = "select * from Categories where Categoryid = " + id;
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                obj.Categoryid = Convert.ToInt32(myReader["Categoryid"].ToString());
                obj.CategoryName = myReader["CategoryName"].ToString();
            }
            myReader.Close();
            return obj;
        }
        public static List<Category> GetCategoryList(SqlConnection dbcon)
        {
            List<Category> itemlist = new List<Category>();
            string strsql = "select * from Categories";
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                Category obj = new Category();
                obj.Categoryid = Convert.ToInt32(myReader["Categoryid"].ToString());
                obj.CategoryName = myReader["CategoryName"].ToString();
                itemlist.Add(obj);
            }
            myReader.Close();
            return itemlist;
        }
        public static int CUCategory(SqlConnection dbcon, string CUDAction, Category obj)
        {
            SqlCommand cmd = new SqlCommand();
            if (CUDAction == "create")
            {
                cmd.CommandText = "insert into Categories (CategoryName) " +
                "Values (@CategoryName)";
                cmd.Parameters.AddWithValue("@CategoryName", SqlDbType.VarChar).Value = obj.CategoryName;
            }
            else if (CUDAction == "update")
            {
                cmd.CommandText = "update Categories set CategoryName = @CategoryName " +
                "Where Categoryid = @Categoryid";
                cmd.Parameters.AddWithValue("@CategoryName", SqlDbType.VarChar).Value = obj.CategoryName;
                cmd.Parameters.AddWithValue("@Categoryid", SqlDbType.Int).Value = obj.Categoryid;
            }
            cmd.Connection = dbcon;
            int intResult = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return intResult;
        }

        public static int CategoryDelete(SqlConnection dbcon, int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete filmcategories where categoryid = " + id;
            cmd.Connection = dbcon;
            int intResult = cmd.ExecuteNonQuery();
            
            cmd.CommandText = "delete categories where categoryid = " + id;
            intResult = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return 1;
        }
        public static IEnumerable<SelectListItem> GetCategoriesDDList(SqlConnection dbcon)
        {
            IList<SelectListItem> ddlist = new List<SelectListItem>();
            string strsql = "select * from categories";
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                ddlist.Add(new SelectListItem()
                {
                    Text = myReader["CategoryName"].ToString(),
                    Value = myReader["Categoryid"].ToString()
                });
            }
            myReader.Close();
            return ddlist;
        }
    }
}