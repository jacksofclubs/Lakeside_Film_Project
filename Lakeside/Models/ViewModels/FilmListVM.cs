using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lakeside.Models;
namespace Lakeside.Models.ViewModels
{
    public class FilmListVM
    {
        public int selectedcatid { get; set; }
        public List<Film> films { get; set; }
        public IEnumerable<SelectListItem> catlist { get; set; }
    }
}