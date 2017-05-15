using Lakeside.Models;
using System;using System.Collections.Generic;
using System.Web.Mvc;
namespace Lakeside.Models.ViewModels
{
    public class ReviewVM
    {
        public Review review { get; set; }
        public IEnumerable<SelectListItem> filmlist { get; set; }
        public int SelectedFilm { get; set; }
    }
}