using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lakeside.Models;

namespace Lakeside.Models.ViewModels
{
    public class FilmEditVM
    {
        public Film film { get; set; }
        public List<CheckModel> FilmCatList { get; set; }
    }
}