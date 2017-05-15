using System.Collections.Generic;
using System.Web.Mvc;

namespace Lakeside.Models.ViewModels
{
    public class FilmListVM
    {
        public int selectedcatid { get; set; }
        public List<Film> films { get; set; }
        public IEnumerable<SelectListItem> catlist { get; set; }
    }
}