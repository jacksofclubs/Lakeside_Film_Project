using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Lakeside.Models
{
    public class Member
    {
        [Required, Key]
        public int MemberID { get; set; }
        [Required, MaxLength(50)]
        public string Email { get; set; }
        [Required, MaxLength(20)]
        public string PWD { get; set; }
        [Required, MaxLength(30)]
        public string MemberName { get; set; }
        [Required, MaxLength(30)]
        public string Avatar { get; set; }
        [Required]
        public int Admin { get; set; }

        public static Member GetMemberSingle(SqlConnection dbcon, int id)
        {
            Member obj = new Member();
            string strsql = "select * from Members where MemberID = " + id;
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                obj.MemberID = Convert.ToInt32(myReader["MemberID"].ToString());
                obj.Email = myReader["Email"].ToString();
                obj.PWD = myReader["PWD"].ToString();
                obj.MemberName = myReader["MemberName"].ToString();
                obj.Avatar = myReader["Avatar"].ToString();
                obj.Admin = Convert.ToInt32(myReader["Admin"].ToString());
            }
            myReader.Close();
            return obj;
        }
        public static List<Member> GetMemberList(SqlConnection dbcon)
        {
            List<Member> itemlist = new List<Member>();
            string strsql = "select * from Members";
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                Member obj = new Member();
                obj.MemberID = Convert.ToInt32(myReader["MemberID"].ToString());
                obj.Email = myReader["Email"].ToString();
                obj.PWD = myReader["PWD"].ToString();
                obj.MemberName = myReader["MemberName"].ToString();
                obj.Avatar = myReader["Avatar"].ToString();
                obj.Admin = Convert.ToInt32(myReader["Admin"].ToString());
                itemlist.Add(obj);
            }
            myReader.Close();
            return itemlist;
        }
        public static int CUDMember(SqlConnection dbcon, string CUDAction, Member obj)
        {
            SqlCommand cmd = new SqlCommand();
            if (CUDAction == "create")
            {
                cmd.CommandText = "insert into Members " +
                "(Email,PWD,MemberName,Avatar,Admin) " +
                "Values (@Email,@PWD,@MemberName,@Avatar,@Admin)";
                cmd.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = obj.Email;
                cmd.Parameters.AddWithValue("@PWD", SqlDbType.NVarChar).Value = obj.PWD;
                cmd.Parameters.AddWithValue("@MemberName", SqlDbType.VarChar).Value = obj.MemberName;
                cmd.Parameters.AddWithValue("@Avatar", SqlDbType.VarChar).Value = obj.Avatar;
                cmd.Parameters.AddWithValue("@Admin", SqlDbType.Int).Value = obj.Admin;
            } else if (CUDAction == "update")
            {
                cmd.CommandText = "update Members set Email = @Email,PWD = @PWD, " +
                "MemberName = @MemberName,Avatar = @Avatar,Admin = @Admin " +
                "Where MemberID = @MemberID";
                cmd.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = obj.Email;
                cmd.Parameters.AddWithValue("@PWD", SqlDbType.NVarChar).Value = obj.PWD;
                cmd.Parameters.AddWithValue("@MemberName", SqlDbType.VarChar).Value = obj.MemberName;
                cmd.Parameters.AddWithValue("@Avatar", SqlDbType.VarChar).Value = obj.Avatar;
                cmd.Parameters.AddWithValue("@Admin", SqlDbType.Int).Value = obj.Admin;
                cmd.Parameters.AddWithValue("@MemberID", SqlDbType.Int).Value = obj.MemberID;
            }
            cmd.Connection = dbcon;
            int intResult = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return intResult;
        }

        public static int MemberDelete(SqlConnection dbcon, int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete reviews where memberid = @Memberid";
            cmd.Parameters.AddWithValue("@Memberid", SqlDbType.Int).Value = id;

            cmd.Connection = dbcon;
            int intResult = cmd.ExecuteNonQuery();

            cmd.CommandText = "delete members where memberid = @Memberid";
            intResult = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return 1;
        }
    }
}