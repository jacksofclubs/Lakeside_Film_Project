using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;


namespace Lakeside.Models
{
    public class CheckModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public static List<CheckModel> GetCheckModelList(SqlConnection dbcon, int id)
        {
            List<CheckModel> itemlist = new List<CheckModel>();
            string strsql = "select * from checkmodelview1 where filmid = " + id + " order by 2";
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                CheckModel obj = new CheckModel();
                obj.ID = Convert.ToInt32(myReader["Categoryid"].ToString());
                obj.Name = myReader["CategoryName"].ToString();
                if (myReader["Checked"].ToString() == "1") obj.Checked = true;
                else obj.Checked = false;
                itemlist.Add(obj);
            }
            myReader.Close();
            return itemlist;
        }
    }
}