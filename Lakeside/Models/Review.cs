using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
        public string ReviewTitle { get; set; }
        [Required, MaxLength(1000)]
        public string FullReview { get; set; }

        // get review single

        // CUD review
    }
}