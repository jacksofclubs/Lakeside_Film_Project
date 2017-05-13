using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lakeside.Models;
namespace Lakeside.Models.ViewModels
{
    public class ViewFilmVM
    {
        public Film selectedfilm { get; set; }
        public List<FilmReview> reviewlist { get; set; }
    }
}