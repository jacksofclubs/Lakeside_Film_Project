using System.Collections.Generic;

namespace Lakeside.Models.ViewModels
{
    public class FilmEditVM
    {
        public Film film { get; set; }
        public List<CheckModel> FilmCatList { get; set; }
    }
}